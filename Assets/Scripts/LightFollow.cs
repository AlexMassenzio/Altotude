using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFollow : MonoBehaviour {

	private GameObject player;
	private Transform focus;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectsWithTag("Player")[0];
		focus = player.transform;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += (focus.position - transform.position) * Time.deltaTime * 5;
		Vector3 temp = transform.position;
		temp.y = 12.47f;
		transform.position = temp;
	}
}
