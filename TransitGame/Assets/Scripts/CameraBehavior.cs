using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
	//Settings
	private GameObject player;
	
	//Init
	void Start ()
	{
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	//Update
	void FixedUpdate ()
	{
		//Set Camera Position
		float offset = -17f;
		float player_angle = player.transform.eulerAngles.y * Mathf.Deg2Rad;
		Vector3 camera_offset = new Vector3(Mathf.Sin(player_angle) * offset, 5f, Mathf.Cos(player_angle) * offset);
		Vector2 new_pos = new Vector2(Mathf.Lerp(transform.position.x, player.transform.position.x + camera_offset.x, Time.deltaTime * 2.5f), Mathf.Lerp(transform.position.z, player.transform.position.z + camera_offset.z, Time.deltaTime * 2.5f));
		transform.position = new Vector3(new_pos.x, transform.position.y, new_pos.y);
		
		//Set Camera Angle
		transform.eulerAngles = new Vector3(transform.eulerAngles.x, Mathf.LerpAngle(transform.eulerAngles.y, player.transform.eulerAngles.y, Time.deltaTime * 1f), transform.eulerAngles.z);
	}
}
