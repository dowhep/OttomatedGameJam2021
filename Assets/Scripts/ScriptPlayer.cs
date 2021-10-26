using System.Collections.Generic;
using UnityEngine;

public class ScriptPlayer : MonoBehaviour
{
    const float I80OverPI = 180f / Mathf.PI;
    const float PIOver180 = Mathf.PI / 180f;

    private Queue<Vector2> nextAction = new Queue<Vector2>();
    private float timer = 0.0f;
    [SerializeField] private bool onGround;
    private Rigidbody2D rig;
    private ContactPoint2D[] contactPoints = new ContactPoint2D[10];
    private Vector2 mouseBegin = Vector2.zero;
    //private Vector2 mouseEnd = Vector2.zero;
    private Vector2 vecFinal;
    private bool isAiming = false;
    private float aimTime = 0.0f;

    private float offsetAngle = 0f;
    private float offsetAngleSub = 0f;

    private AudioSource audioAiming;
    private AudioSource audioCollision;
    private AudioSource audioShooting;

    private float maxCollisionMagSoundInv;


    #region Public Fields
    public Camera curCam;
    public GameObject Aimer;

    public float lengthConstant = 20.0f;
    public float strengthConstant = 0.5f;
    public float rateConstant = 1.0f;
    public float angleConstant = 1.0f;
    [Range(0f, 1f)] public float onGroundMin = 0.7f;
    [Range(0f, 1f)] public float moveDamp = 0.3f;
    [Range(0f, 1f)] public float collisionDamp = 0.8f;
    [Range(0f, 1f)] public float rollDamp = 0.9f;
    public float velocityMin = 0.1f;
    public float bufferTime = 2.0f;
    public float aimUnstableTime = 1.0f;
    public float aimUnstableMultiplier = 5.0f;
    public float minAimShake = 0.1f;

    public float minAimingSound = 0.2f;
    public float aimingSoundMult = 5f;

    public float maxCollisionMagSound = 30f;
    #endregion

    #region Unity Methods

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        audioAiming = AudioManager.GetSound("AimingSound").source;
        audioCollision = AudioManager.GetSound("CollisionSound").source;
        audioShooting = AudioManager.GetSound("ShootingSound").source;
        maxCollisionMagSoundInv = (maxCollisionMagSound == 0) ? 0f : 1f / maxCollisionMagSound;
    }
 
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Reset();

            aimTime = 0.0f;

            mouseBegin = Input.mousePosition;
            isAiming = true;

            audioAiming.Play();
        } 
        if (Input.GetMouseButtonUp (0))
        {
            //mouseEnd = Input.mousePosition;
            AddQueue(vecFinal);
            isAiming = false;

            audioAiming.Stop();
        }

        Aimer.SetActive(false);

        if (isAiming)
        {
            Aimer.SetActive(true);

            Vector2 mouseCur = Input.mousePosition;
            Vector3 vecDif = curCam.ScreenToWorldPoint(mouseBegin)
                - curCam.ScreenToWorldPoint(mouseCur);
            float rot = vecDif.x == 0 ? 90 : Mathf.Atan2(vecDif.y, vecDif.x) * I80OverPI;
            //rot = vecDif.y > 0 ? rot : rot + 180;
            float limitedMagnitude = Cap01(vecDif.magnitude / curCam.orthographicSize);

            audioAiming.pitch = minAimingSound + limitedMagnitude * aimingSoundMult;

            // shaking effect
            aimTime += Time.deltaTime;
            float editedMultiplier = aimTime > aimUnstableTime ? 
                1.0f : (aimUnstableTime - aimTime) * aimUnstableMultiplier + 1.0f;

            float deltaAngle = Time.deltaTime * vecDif.magnitude * rateConstant;
            offsetAngle += deltaAngle;
            offsetAngleSub += deltaAngle * (1.0f + limitedMagnitude * 2.7f);

            rot += angleConstant * (limitedMagnitude + minAimShake) * 
                (Mathf.Sin(offsetAngle) - Mathf.Sin(offsetAngleSub)) * editedMultiplier;

            vecFinal = new Vector2(Mathf.Cos(rot * PIOver180), Mathf.Sin(rot * PIOver180)) * limitedMagnitude;

            // limit strength
            Vector3 processedDif = lengthConstant * vecFinal;
            Aimer.transform.position = processedDif * 0.5f + transform.position;
            Aimer.transform.rotation = Quaternion.Euler(0f, 0f, rot);
            Aimer.transform.localScale = new Vector3(processedDif.magnitude, 0.5f, 1f);

            // debug
            //Debug.Log(offsetAngle + ", " + offsetAngleSub);
        }

        if (timer > 0.0f)
        {
            Aimer.SetActive(true);
            Aimer.transform.position = nextAction.Peek() * lengthConstant * 0.5f;
            Aimer.transform.position += transform.position;

            timer -= Time.deltaTime;

            if (timer <= 0.0f)
            {
                nextAction.Clear();
                Aimer.SetActive(false);
            } 
            else
            {
                Color aimerColor = Aimer.GetComponent<SpriteRenderer>().color;
                aimerColor.a = timer / bufferTime;
                Aimer.GetComponent<SpriteRenderer>().color = aimerColor;
            }
        }
    }

    private void Reset()
    {
        offsetAngle = 0f;
        offsetAngleSub = 0f;
        timer = 0.0f;

        Color aimerColor = Aimer.GetComponent<SpriteRenderer>().color;
        aimerColor.a = 1f;
        Aimer.GetComponent<SpriteRenderer>().color = aimerColor;
    }

    void FixedUpdate()
    {
        if (onGround && nextAction.Count > 0)
        {
            Vector2 jumpVec = nextAction.Dequeue();
            rig.velocity *= moveDamp;
            rig.velocity += jumpVec * strengthConstant;
            timer = 0.0f;
            Aimer.SetActive(false);
            audioShooting.Play();
        }
        onGround = false;
    }

    #endregion

    #region Private Methods

    void AddQueue(Vector2 vecJump)
    {
        nextAction.Clear();
        nextAction.Enqueue(vecJump);
        timer = bufferTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        rig.velocity *= collisionDamp;
        //CollisionOverlap(collision);
        float magOfCol = (rig.velocity * collision.GetContact(0).normal).magnitude;
        audioCollision.volume = Cap01(magOfCol / maxCollisionMagSound);
        audioCollision.Play();
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        rig.velocity *= rollDamp;
        CollisionOverlap(collision);
    }

    private void CollisionOverlap(Collision2D collision)
    {
        onGround = false;

        //collision.GetContacts(contactPoints);
        //foreach (ContactPoint2D point in contactPoints)
        //{
        //    if (point.normal.y > onGroundMin) onGround = true;
        //}

        if (rig.velocity.magnitude < velocityMin)
        {
            ContactPoint2D pt = collision.GetContact(0);
            rig.velocity = Vector2.zero;
            transform.position = pt.point + pt.normal * transform.localScale * 0.49f;

            onGround = true;
        }
    }

    private float Cap01(float input)
    {
        if (input < 0f) return 0f;
        if (input > 1f) return 1f;
        float cubing = 1f - input;
        return 1f - cubing * cubing * cubing;
    }
    #endregion
}
