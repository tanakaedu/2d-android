using UnityEngine;
using System.Collections;

public class fallblock : MonoBehaviour {

    void OnCollisionEnter2D(Collision2D collision2D)
    {
        if(collision2D.gameObject.CompareTag("UnityChan"))
        {
            Invoke("Fall", 0.5f);
        }
    }

    void Fall()
    {
        GetComponent<Rigidbody2D>().isKinematic = false;
    }
}
