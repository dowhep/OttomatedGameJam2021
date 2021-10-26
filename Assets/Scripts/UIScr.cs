using UnityEngine;

public class UIScr : MonoBehaviour
{
    private bool isUIOpen = false;
    #region Public Fields
    public GameObject GOOptions;
    #endregion

    #region Unity Methods
    private void Start()
    {
        GOOptions.SetActive(isUIOpen);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            isUIOpen = !isUIOpen;
            GOOptions.SetActive(isUIOpen);
        }
    }
 
    #endregion
 
    #region Private Methods
    #endregion
}
