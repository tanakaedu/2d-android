using UnityEngine;
using System.Collections;

public class bullet : MonoBehaviour{

	private GameObject player;
	private int speed = 10;

	void Start () {
		//ユニティちゃんオブジェクトを取得
		player = GameObject.FindWithTag ("UnityChan");
		//rigidbody2Dコンポーネントを取得
		Rigidbody2D rigidbody2D = GetComponent<Rigidbody2D> ();
		//ユニティちゃんの向いている向きに弾を飛ばす
		rigidbody2D.velocity = new Vector2 (speed * player.transform.localScale.x, rigidbody2D.velocity.y);
		//画像の向きをユニティちゃんに合わせる
		Vector2 temp = transform.localScale;
		temp.x = player.transform.localScale.x;
		transform.localScale = temp;
		//5秒後に消滅
		Destroy (gameObject, 2);
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }
}
