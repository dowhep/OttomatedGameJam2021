using UnityEngine;

public class scoringRegionScr : MonoBehaviour
{

    #region Public Fields
    public BasketScr basketScr;
    #endregion

    #region Unity Methods
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            basketScr.Scored(collision.attachedRigidbody.velocity);
        }
    }
    #endregion

    #region Private Methods
    #endregion
}
