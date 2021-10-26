using UnityEngine;

public class SwitchButtonScr : AbstSwitchReactor
{
    private SpriteRenderer sprRd;
<<<<<<< HEAD
    private AudioSource source;
=======
>>>>>>> origin/main

    #region Public Fields

    #endregion

    #region Unity Methods
    protected override void Start()
    {
        base.Start();
        sprRd = GetComponent<SpriteRenderer>();
    }
    #endregion
    protected override void React()
    {
        sprRd.color = globalIsRed ? new Color(1f, 0.3f, 0.3f, 0.6f) : new Color(0.3f, 0.6f, 1f, 0.6f);
<<<<<<< HEAD
        source = AudioManager.GetSound("ButtonSound").source;
=======
>>>>>>> origin/main
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
<<<<<<< HEAD
            source.Play();
=======
>>>>>>> origin/main
            Switch();
        }
    }

    #region Private Methods
    #endregion
}
