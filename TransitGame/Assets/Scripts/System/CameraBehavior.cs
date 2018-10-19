using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
	//Components
	private GameObject player;
	
	//Settings
	public bool compass_show { set; private get; }

	//Variables
	private float compass_lerp;
	private float angle_offset;

	private Vector3 compass_start;
	private Vector3 compass_end;
	
	//Init
	void Start ()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		angle_offset = transform.eulerAngles.y;
		compass_show = false;
		compass_start = transform.GetChild(0).transform.localPosition + new Vector3(0, -0.4f, 0);
		compass_end = transform.GetChild(0).transform.localPosition;
		transform.GetChild(0).transform.localPosition = compass_start;
	}

	void FixedUpdate ()
	{
		//Set Camera Position
		float offset = -17f;
		float player_angle = player.transform.eulerAngles.y * Mathf.Deg2Rad;
		Vector3 camera_offset = new Vector3(Mathf.Sin(player_angle) * offset, 5f, Mathf.Cos(player_angle) * offset);
		Vector2 new_pos = new Vector2(Mathf.Lerp(transform.position.x, player.transform.position.x + camera_offset.x, Time.deltaTime * 2.5f), Mathf.Lerp(transform.position.z, player.transform.position.z + camera_offset.z, Time.deltaTime * 2.5f));
		transform.position = new Vector3(new_pos.x, transform.position.y, new_pos.y);
		
		//Set Mouse Rotation
		float x_axis = Mathf.Clamp(((Input.mousePosition.x / Screen.width) - 0.5f) * 2f, -1f, 1f);
		float y_axis = -1 * Mathf.Clamp(((((Input.mousePosition.y / Screen.height) - 0.5f) * 2f) - 0.5f) * 0.9f, -0.6f, 0.2f);
		
		//Set Camera Angle
		angle_offset = Mathf.LerpAngle(angle_offset, player.transform.eulerAngles.y, Time.deltaTime * 0.85f);
		transform.eulerAngles = new Vector3(Mathf.LerpAngle(transform.eulerAngles.x, (y_axis * 25f), Time.deltaTime * 3.5f), Mathf.LerpAngle(transform.eulerAngles.y, angle_offset + (x_axis * 25f), Time.deltaTime * 3.5f), transform.eulerAngles.z);
		
		//Compass Lerp
		if (compass_show)
		{
			if (GameObject.FindGameObjectWithTag("Text") == null) {
				transform.GetChild(0).transform.localPosition = Vector3.Lerp(transform.GetChild(0).transform.localPosition, compass_end, Time.deltaTime * 3.5f);
			}
			else
			{
				transform.GetChild(0).transform.localPosition = Vector3.Lerp(transform.GetChild(0).transform.localPosition, compass_start, Time.deltaTime * 3.5f);
			}
		}
		else
		{
			transform.GetChild(0).transform.localPosition = Vector3.Lerp(transform.GetChild(0).transform.localPosition, compass_start, Time.deltaTime * 3.5f);
		}
		
		//Compass Angle
		GameObject[] offshore_checks = GameObject.FindGameObjectsWithTag("Offshore");
		if (offshore_checks.Length > 0)
		{
			Vector2 player_position = new Vector2(player.transform.position.x, player.transform.position.z);
			Vector2 offshore_position = new Vector2(offshore_checks[0].transform.position.x, offshore_checks[0].transform.position.z);

			for (int i = 0; i < offshore_checks.Length; i++)
			{
				Vector2 new_check = new Vector2(offshore_checks[i].transform.position.x, offshore_checks[i].transform.position.z);
				if (Vector2.Distance(player_position, new_check) < Vector2.Distance(player_position, offshore_position))
				{
					offshore_position = new_check;
				}
			}
			//Set Distance
			float compass_dis = Vector2.Distance(player_position, offshore_position) / 100f;
			transform.GetChild(0).transform.GetChild(2).gameObject.GetComponent<TextMesh>().text = string.Format("{0:F1}", compass_dis);
			
			//Set Angle
			float compass_angle = pointAngle(player_position, offshore_position);
			transform.GetChild(0).transform.GetChild(0).transform.localEulerAngles = new Vector3(0, compass_angle - transform.eulerAngles.y, 0);
		}
	}
	
	private float pointAngle(Vector2 pointA, Vector2 pointB)
	{
		return -((Mathf.Atan2(pointB.y - pointA.y, pointB.x - pointA.x) * 180) / Mathf.PI) + 90;
	}

}
