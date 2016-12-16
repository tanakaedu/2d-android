using UnityEngine;
using System.Collections;

public class fallground : MonoBehaviour {

    void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.CompareTag("UnityChan"))
        {
            Invoke("Fall", 0.3f);
        }
    }

    void Fall()
    {
        GetComponent<Rigidbody2D>().isKinematic = false;
    }
}
