using UnityEngine;
using System.Collections;

public class enemy1script : MonoBehaviour
{

    Rigidbody2D rigidbody2D;
    public int speed = -3;

    public GameObject explosion;
    public GameObject item;


    public int attackPoint = 10;
    private lifescript lifeScript;

    private const string MAIN_CAMERA_TAG_NAME = "MainCamera";
    private bool _isRendered = false;
    


    // Use this for initialization
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        lifeScript = GameObject.FindGameObjectWithTag("HP").GetComponent<lifescript>();

    }

    // Update is called once per frame
    void Update()
    {
        if (_isRendered)
        {
            rigidbody2D.velocity = new Vector3(speed, rigidbody2D.velocity.y);
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Bullet")
        {
            Destroy(gameObject);
            Instantiate(explosion, transform.position, transform.rotation);
            if(Random.Range(0,4)==0)
            {
                Instantiate(item, transform.position, transform.rotation);
            }
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "UnityChan")
        {
            lifeScript.LifeDown(attackPoint);
        }
    }
    void OnWillRenderObject()
    {
        if(Camera.current.tag == MAIN_CAMERA_TAG_NAME)
        {
            _isRendered = true;
        }
    }
}
