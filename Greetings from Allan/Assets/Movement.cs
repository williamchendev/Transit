using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

	private Rigidbody rb;
	
	// Use this for initialization
	void Start ()
	{
		rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.W))
		{
			rb.AddForce(new Vector3(2,7,2));
		}
	}
}
