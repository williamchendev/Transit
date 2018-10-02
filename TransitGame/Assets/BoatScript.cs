using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatScript : MonoBehaviour
{

	private Rigidbody rb;

	[SerializeField] private float spd;
	
	// Use this for initialization
	void Start ()
	{
		rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
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
		
		//rb.MovePosition(new Vector3(transform.position.x, 1, transform.position.z));
	}
}
