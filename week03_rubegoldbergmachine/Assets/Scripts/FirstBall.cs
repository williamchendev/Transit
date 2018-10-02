using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstBall : MonoBehaviour
{

	private bool begin;
	
	// Use this for initialization
	void Start ()
	{
		begin = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!begin)
		{
			if (Input.GetKeyUp(KeyCode.Space))
			{
				begin = true;
				GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
				GameObject.FindWithTag("radio").GetComponent<CameraManager>().changeCamA();
			}
		}
	}
}
