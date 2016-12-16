using UnityEngine;
using System.Collections;

public class itemscript : MonoBehaviour {

    public int healPoint = 20;
    private lifescript lifeScript;

    void Start()
    {
        lifeScript = GameObject.FindGameObjectWithTag("HP").GetComponent<lifescript>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "UnityChan")
        {
            lifeScript.LifeUp(healPoint);
            Destroy(gameObject);
        }
    }
}
