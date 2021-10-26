using UnityEngine;

public class VictoryScr : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D rig = collision.GetComponent<Rigidbody2D>();
            rig.isKinematic = true;
            UIScr.Victory();
        }
    }
}
