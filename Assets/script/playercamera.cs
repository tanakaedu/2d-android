using UnityEngine;
using System.Collections;

public class playercamera : MonoBehaviour {
	private Transform target;

	// Use this for initialization
	void Start () {
		target = GameObject.Find ("UnityChan").transform;
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3 (target.position.x, transform.position.y, transform.position.z);
	
	}
}
