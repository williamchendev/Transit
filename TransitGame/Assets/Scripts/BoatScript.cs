using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatScript : MonoBehaviour
{

	private Rigidbody rb;
	public InnoWaveBehavior wave;

	[SerializeField] private float spd;

	private Vector2 pos;
	
	// Use this for initialization
	void Start ()
	{
		rb = GetComponent<Rigidbody>();
		pos = new Vector2(transform.position.x, transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
		wave.setPosition(new Vector2(transform.position.x, transform.position.z));
		if (Input.GetMouseButtonDown(0))
		{
			RaycastHit hit;
			Ray click_ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(click_ray, out hit))
			{
				if (hit.collider.gameObject.CompareTag("Wave"))
				{
					pos = new Vector2(hit.point.x, hit.point.z);
				}
			}
		}
		
		rb.MovePosition(new Vector3(pos.x, transform.position.y, pos.y));
	
		/*
		if (Input.GetKey(KeyCode.W))
		{
			rb.AddForce(0, 0, spd);
		}
		else if (Input.GetKey(KeyCode.S))
		{
			rb.AddForce(0, 0, -spd);
		}
		
		if (Input.GetKey(KeyCode.A))
		{
			rb.AddForce(-spd, 0, 0);
		}
		else if (Input.GetKey(KeyCode.D))
		{
			rb.AddForce(spd, 0, 0);
		}
		*/
		
		//rb.MovePosition(new Vector3(transform.position.x, 1, transform.position.z));
	}
}
