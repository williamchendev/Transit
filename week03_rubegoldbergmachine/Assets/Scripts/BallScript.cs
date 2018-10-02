using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{

	public GameObject cord;
	private ConstantForce cf;

	private int timer;
	private bool first;
	
	// Use this for initialization
	void Start ()
	{
		cf = GetComponent<ConstantForce>();
		timer = 120;

		first = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!first)
		{
			if (cord == null)
			{
				cf.enabled = true;
				timer--;
				first = true;
			}
		}

		if (timer < 120 && timer > 0)
		{
			timer--;
			if (timer <= 0)
			{
				cf.enabled = false;
			} 
		}
	}
}
