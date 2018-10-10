using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
	private GameObject player;
	
	// Use this for initialization
	void Start ()
	{
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update ()
	{
		float height = 6f;
		transform.position = Vector3.Lerp(transform.position, player.transform.position + new Vector3(0, 5f, -17f), Time.deltaTime * 5f);
		transform.position = new Vector3(transform.position.x, height, transform.position.z);
	}
}
