using UnityEngine;

public class SwitchPlatformScr : AbstSwitchReactor
{
    #region Public Fields
    public GameObject platform;
    public bool isRed;
    #endregion

    #region Unity Methods
    #endregion

    #region Private Methods
    #endregion
    protected override void React()
    {
        platform.SetActive(isRed == globalIsRed);
    }
}
