using UnityEngine;
using UnityEditor;

public class SlapScr : MonoBehaviour
{
    private float timer = 0f;
    private bool slapped = false;

    private SpriteRenderer renderor;

    #region Public Fields

    public Sprite notActivated;
    public Sprite activated;

    // relative aim
    public Vector2 aim;

    public float strength;
    public bool reslapable;
    public float reslapTime;
    public bool horizonal = true;
    #endregion
 
    #region Unity Methods
 
    void Start()
    {
        renderor = gameObject.GetComponent<SpriteRenderer>();
    }
 
    void Update()
    {
        if (timer > 0.0f)
        {
            timer -= Time.deltaTime;
            if (timer <= 0.0f)
            {
                slapped = false;
                renderor.sprite = notActivated;
            }
        }
    }

    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !slapped)
        {
            Rigidbody2D rig = collision.attachedRigidbody;
            rig.velocity = findVector(aim - rig.position + new Vector2(transform.position.x, transform.position.y),
                rig.gravityScale * Physics2D.gravity.y, strength, horizonal);
            
            if (reslapable) timer = reslapTime;
            renderor.sprite = activated;
            slapped = true;
        }
    }

    #region Private Methods
    
    /// <summary>
    /// Find the velocity of the ball needed to aim at exactly a spot with given gravity and speed.
    /// </summary>
    /// <param name="offset">The Aim</param>
    /// <param name="gravity">The y-acceleration (usually negative)</param>
    /// <param name="speed">The magitude of velocity provided</param>
    /// <param name="fasterRoot">True to find a more horizontal solution (may not exist)</param>
    /// <returns></returns>
    private Vector2 findVector(Vector2 offset, float gravity, float speed, bool fasterRoot)
    {
        float dist = offset.x;
        float height = offset.y;
        float speed2d = fastSquare(speed);
        float delta = speed2d * (speed2d + 2 * gravity * height) - fastSquare(gravity * dist);
        if (delta < 0) return Vector2.zero;
        else
        {
            float delta2 = (speed2d + gravity * height + (fasterRoot ? -Mathf.Sqrt(delta) : Mathf.Sqrt(delta))) * 2;
            if (delta2 <= 0) return Vector2.zero;

            float timeInv = Mathf.Abs(gravity) / Mathf.Sqrt(delta2);

            float Vx = dist * timeInv;
            float Vy = Mathf.Sqrt(speed2d - fastSquare(Vx));

            if (gravity > 0f)
            {
                Vy = -Vy;
            }

            if (fasterRoot)
            {
                float time = 1f / timeInv;
                if ((0.5 * time * time * gravity > height) ^ (gravity > 0f)) Vy = -Vy;
            }

            return new Vector2(Vx, Vy);
        }
    }

    private float fastSquare(float x)
    {
        return x * x;
    }
    #endregion
}







[CustomEditor(typeof(SlapScr))]
public class SlapScrEditor : Editor
{
    private SlapScr slapper;

    public void OnSceneGUI()
    {
        slapper = this.target as SlapScr;
        Handles.color = Color.green;
        Vector3 pos = slapper.aim;
        Handles.DrawWireDisc(slapper.transform.position + pos, Vector3.forward, 0.5f);
    }
}