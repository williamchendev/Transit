using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondBall : MonoBehaviour
{

	public GameObject candle;
	private Rigidbody rb;
	
	// Use this for initialization
	void Start ()
	{
		rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		if (rb.constraints == RigidbodyConstraints.None)
		{
			transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Clamp(transform.position.z, 4.25f, 5));
		}
	}
}
