using TMPro;
using UnityEngine;

public class BasketScr : MonoBehaviour
{
    private int score = 0;
    private float minMagnitudeSqrd;

    #region Public Fields
    public TextMeshPro scoreText;

    public float minMagnitude;
    #endregion

    #region Unity Methods
    private void Start()
    {
        minMagnitudeSqrd = minMagnitude * minMagnitude;
    }

    #endregion

    public void Scored (Vector2 velocity)
    {
        if (velocity.y < 0)
        {
            if (velocity.sqrMagnitude > minMagnitudeSqrd)
            {
                score += 3;
                // fancy celebration effect
            } 
            else
            {
                score += 2;
            }
            if (score > 99) score = 99;
            scoreText.text = score.ToString("D2");
        }
    }

    #region Private Methods
    #endregion
}
