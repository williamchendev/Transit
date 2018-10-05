using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyScript : MonoBehaviour
{

	private int timer;
	
	// Use this for initialization
	void Start ()
	{
		timer = 200;
	}
	
	// Update is called once per frame
	void Update ()
	{
		timer--;
		if (timer <= 0)
		{
			Destroy(gameObject);
		}
	}
}
