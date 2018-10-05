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
			if (Vector2.Distance(new Vector2(transform.position.x, transform.position.z), move_position) < 1f)
			{
				moving = false;
			}
			
			//Velocity
			//rb.velocity = new Vector3(rb.velocity.x * friction, rb.velocity.y, rb.velocity.z * friction);
		}
		else
		{
			
		}
		
		//Set Wave Position
		wave_behave.setPosition(new Vector2(transform.position.x, transform.position.z));
		float boat_height = (wave_behave.getPointAt(InnoWaveBehavior.size / 2, InnoWaveBehavior.size / 2).y - 1.15f);
		transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, boat_height, Time.deltaTime * 2), transform.position.z);
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
