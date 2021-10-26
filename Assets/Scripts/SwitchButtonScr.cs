using UnityEngine;

public class SwitchButtonScr : AbstSwitchReactor
{
    private SpriteRenderer sprRd;

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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Switch();
        }
    }

    #region Private Methods
    #endregion
}
