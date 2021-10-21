using UnityEngine;

public class ScriptCam : MonoBehaviour
{
    Vector3 offset;
    Vector3 velocity = new Vector3();

    #region Public Fields

    public Transform playerTrans;
    public float SmoothTime = 1.0f;

    #endregion
 
    #region Unity Methods
 
    void Start()
    {
        offset = transform.position - playerTrans.position;
    }
 
    void LateUpdate()
    {
        Vector3 targetPos = playerTrans.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, SmoothTime);

    }
 
    #endregion
 
    #region Private Methods
    #endregion
}
