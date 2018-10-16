using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyScript : MonoBehaviour
{

	public int timer;
	
	// Update is called once per frame
	void Update ()
	{
		timer--;
		if (timer == 0)
		{
			Destroy(gameObject);
		}
	}
}
