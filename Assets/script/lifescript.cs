using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class lifescript : MonoBehaviour {

    RectTransform rt;

    public GameObject unityChan;
    public GameObject explotion;
    public Text gameOverText;
    private bool gameOver = false;

	void Start () {
        rt = GetComponent<RectTransform>();
	}

    void Update()
    {
        if(rt.sizeDelta.y <= 0)
        {
            if(gameOver == false)
            {
                Instantiate(explotion, unityChan.transform.position + new Vector3(0, 1, 0), unityChan.transform.rotation);
            }
            GameOver();
        }
        if (gameOver)
        {
            gameOverText.enabled = true;
            if (Input.GetMouseButton(0))
            {
                Application.LoadLevel("start");
            }
        }
    }
	
	// Update is called once per frame
	public void LifeDown(int ap) {
        rt.sizeDelta -= new Vector2(0, ap);
	}
    public void LifeUp(int hp)
    {
        rt.sizeDelta += new Vector2(0, hp);
        if(rt.sizeDelta.y > 240f)
        {
            rt.sizeDelta = new Vector2(51f, 240f);
        }
    }
    public void GameOver()
    {
        gameOver = true;
        Destroy(unityChan);
    }
}
