using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class player : MonoBehaviour {

	public float speed = 4f;
	public float jumpPower = 700;
	public LayerMask groundLayer;
	public GameObject bullet;
    public lifescript lifeScript;
	public AudioClip audioClip;
	private Rigidbody2D rigidbody2D;
	private Animator anim;
	private bool isGrounded;
    private Renderer renderer;
    private bool gameClear = false;
    public Text clearText;
	AudioSource audioSource;
	private SpriteRenderer spriteRenderer;
	// ジャンプをキャンセルする秒数
	public const float JUMP_CANCEL_TIME = 0.5f;
	// ジャンプが有効になる時間
	private float jumpIgnoreTime = 0f;

	// Use this for initialization
	void Start () {
		audioSource = gameObject.GetComponent < AudioSource> ();
		audioSource.clip = audioClip;
		anim = GetComponent<Animator> ();
		rigidbody2D = GetComponent<Rigidbody2D> ();
        renderer = GetComponent<Renderer>();

		spriteRenderer = GetComponent<SpriteRenderer> ();	
	}

	void Update(){
		//Linecastでユニティちゃんの足元に地面があるか判定
		isGrounded = Physics2D.Linecast (
			transform.position + transform.up * 1,
			transform.position - transform.up * 0.05f,
			groundLayer);
        if(!gameClear)
        {
            //スペースキーを押し、
			if (	(Time.time >= jumpIgnoreTime)
				&& (Input.GetKeyDown("space") || CrossPlatformInputManager.GetButtonDown("Jump")))
            {
                //着地していた時、
				if (isGrounded && rigidbody2D.velocity.y <= 0f)
                {
                    //Dashアニメーションを止めて、Jumpアニメーションを実行
                    anim.SetBool("dash", false);
                    anim.SetTrigger("Jump");
                    //着地判定をfalse
                    isGrounded = false;
                    //AddForceにて上方向へ力を加える
                    rigidbody2D.AddForce(Vector2.up * jumpPower);
					// ジャンプを一定時間キャンセル
					jumpIgnoreTime = Time.time+JUMP_CANCEL_TIME;
                }
                audioSource.Play();
            }
		}
		float velY = rigidbody2D.velocity.y;
		bool isJumping = velY > 0.1f ? true : false;
		bool isFalling = velY < -0.1f ? true : false;
		anim.SetBool ("isJumping", isJumping);
		anim.SetBool ("isFalling", isFalling);

        if(!gameClear)
        {
		if (Input.GetKeyDown ("f")) {
			anim.SetTrigger ("Shot");
			Instantiate (bullet, transform.position + new Vector3 (0f, 1.2f, 0f), transform.rotation);
		}
            if (gameObject.transform.position.y < -8)
            {
                lifeScript.GameOver();
            }
        }
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (!gameClear)
        {
			float x = Input.GetAxisRaw("Horizontal") + CrossPlatformInputManager.GetAxis("Horizontal");
			x = Mathf.Clamp (x, -1f, 1f);
            if (x != 0)
            {
                rigidbody2D.velocity = new Vector2(x * speed, rigidbody2D.velocity.y);
                /*
				Vector2 temp = transform.localScale;
                temp.x = x;
                transform.localScale = temp;
                */
                anim.SetBool("dash", true);

				// Xがマイナスの時、trueにして、左を向く
				spriteRenderer.flipX = x < 0;
            }
            else
            {
                rigidbody2D.velocity = new Vector2(0, rigidbody2D.velocity.y);
                anim.SetBool("dash", false);
            }
        }
        else
        {
            clearText.enabled = true;
            anim.SetBool("dash", true);
            rigidbody2D.velocity = new Vector2(speed, rigidbody2D.velocity.y);
            Invoke("CallTitle",5);
        }
	}
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            StartCoroutine("Damage");
        }
    }
    IEnumerator Damage()
    {
        gameObject.layer = LayerMask.NameToLayer("PlayerDamage");
        int count = 10;
        while (count > 0)
        {
            renderer.material.color = new Color(1, 1, 1, 0);
            yield return new WaitForSeconds(0.05f);
            renderer.material.color = new Color(1, 1, 1, 1);
            yield return new WaitForSeconds(0.05f);
            count--;
        }
        gameObject.layer = LayerMask.NameToLayer("Player");
    }
    void OnTriggerEnter2D (Collider2D col)
    {
        if(col.tag == "ClearZone")
        {
            gameClear = true;
        }
    }
    void CallTitle()
    {
        Application.LoadLevel("start");
    }
}
