using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatBehavior : MonoBehaviour {

	//Components
	private Rigidbody rb;
	private InnoWaveBehavior wave_behave;
	
	//Settings
	[SerializeField] private float spd;
	
	//Variables
	private bool can_move;
	private bool moving;
	private float angle_lerp;
	private float spd_lerp;
	private Vector2 move_position;
	
	//Start Event
	void Start () {
		//Components
		rb = GetComponent<Rigidbody>();
		wave_behave = GameObject.FindGameObjectWithTag("Wave").GetComponent<InnoWaveBehavior>();
		
		//Variables
		can_move = true;
		moving = false;
		angle_lerp = 0f;
		spd_lerp = 0f;
		move_position = new Vector2(transform.position.x, transform.position.z);
	}
	
	//Update Event
	void Update () {
		//Movement
		if (can_move)
		{
			//Click on position to move towards
			if (click())
			{
				moving = false;
				
				RaycastHit hit;
				Ray click_ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				if (Physics.Raycast(click_ray, out hit))
				{
					if (hit.collider.gameObject.CompareTag("Wave"))
					{
						move_position = new Vector2(hit.point.x, hit.point.z);
						Instantiate(Resources.Load<GameObject>("Sphere"), new Vector3(move_position.x, 0, move_position.y), new Quaternion());
						angle_lerp = 0f;
						spd_lerp = 0f;
						moving = true;
					}
				}
			}
		}

		if (moving)
		{
			//Set angle towards move position
			float move_angle = pointAngle(new Vector2(transform.position.x, transform.position.z), move_position);
			angle_lerp += Time.deltaTime * 0.037f;
			angle_lerp = Mathf.Clamp(angle_lerp, 0, 1);
			transform.eulerAngles = new Vector3(0f, Mathf.LerpAngle(transform.eulerAngles.y, move_angle, angle_lerp), 0f);
			
			//Set Rigidbody velocity
			spd_lerp = Mathf.Lerp(spd_lerp, 1, Time.deltaTime * 5f);
			Vector3 spd_vector = new Vector3(transform.forward.x, 0f, transform.forward.z) * (spd * spd_lerp);
			rb.AddForce(spd_vector);
			
			//Meets target
			Vector2 boat_tip_pos = new Vector2(transform.position.x, transform.position.z) + (new Vector2(transform.forward.x, transform.forward.z) * 0.3f);
			if (Vector2.Distance(boat_tip_pos, move_position) < 1.5f)
			{
				moving = false;
			}
		}
		else
		{
			
		}
		
		//Set Wave Position
		wave_behave.setPosition(new Vector2(transform.position.x, transform.position.z));
		float boat_sin_height = (Mathf.Sin(Time.time * 0.37f)) * 0.05f;
		float boat_height = ((wave_behave.getPointAt(InnoWaveBehavior.size / 2, InnoWaveBehavior.size / 2).y - 0.85f) / 2) - 0.45f;
		transform.position = new Vector3(transform.position.x, boat_height + boat_sin_height, transform.position.z);
		
		//Set Wave Tangent
		float x_tangent = Mathf.Sin(Mathf.PerlinNoise(0, Time.time * 0.37f) * 2 * Mathf.PI);
		float z_tangent = Mathf.Sin(Mathf.PerlinNoise(Time.time * 0.37f, 0) * 2 * Mathf.PI);
		transform.eulerAngles = new Vector3(x_tangent * 6f, transform.eulerAngles.y, z_tangent * 10f);
	}

	//Misc Methods
	private float pointAngle(Vector2 pointA, Vector2 pointB)
	{
		return -((Mathf.Atan2(pointB.y - pointA.y, pointB.x - pointA.x) * 180) / Mathf.PI) + 90;
	}
	
	private bool click()
	{
		return (Input.GetMouseButtonDown(0));
	}
}
