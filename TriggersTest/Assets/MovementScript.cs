using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{

	public float spd;
	private Rigidbody rb;
	
	// Use this for initialization
	void Start ()
	{
		rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 vel = Vector3.zero;
		if (Input.GetKey(KeyCode.W))
		{
			vel.z = spd;
		}
		else if (Input.GetKey(KeyCode.S))
		{
			vel.z = -spd;
		}
		
		if (Input.GetKey(KeyCode.A))
		{
			vel.x = -spd;
		}
		else if (Input.GetKey(KeyCode.D))
		{
			vel.x = spd;
		}
		rb.AddForce(vel);
	}

	private void OnTriggerEnter(Collider other)
	{
		Destroy(other.gameObject);
	}
}