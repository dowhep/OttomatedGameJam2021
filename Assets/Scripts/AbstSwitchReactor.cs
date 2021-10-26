using System.Collections.Generic;
using UnityEngine;

public abstract class AbstSwitchReactor : MonoBehaviour
{

    #region Public Fields
    public static bool globalIsRed = true;
    public static List<AbstSwitchReactor> reactors = new List<AbstSwitchReactor>();
    #endregion
 
    protected virtual void Start()
    {
        reactors.Add(this);
    }

    protected virtual void OnDestroy()
    {
        reactors.Remove(this);
    }

    public static void Switch()
    {
        globalIsRed = !globalIsRed;
        foreach (AbstSwitchReactor reactor in reactors)
        {
            reactor.React();
        }
    }

    #region Private Methods
    protected abstract void React();
    #endregion
}
