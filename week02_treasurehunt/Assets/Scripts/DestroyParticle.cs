using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticle : MonoBehaviour
{

	private int timer;
 
	void Start ()
	{
		timer = 120;
	}
 
	void  Update ()
	{
		timer--;
		if(timer < 0) {
			Destroy(gameObject);
		}
	}
}
