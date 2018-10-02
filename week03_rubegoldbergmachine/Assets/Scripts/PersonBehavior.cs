using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonBehavior : MonoBehaviour
{

	private Animator anim;
	public bool big_oof;
	
	// Use this for initialization
	void Start ()
	{
		anim = GetComponent<Animator>();
		big_oof = false;
	}
	
	// Update is called once per frame
	void Update () {
		/*
		if (Input.GetKeyDown(KeyCode.Alpha0))
		{
			anim.Play("person_button");
		}
		else if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			anim.Play("person_stand");
		}
		else if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			anim.Play("person_look");
		}
		else if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			anim.Play("person_sad");
		}
		*/
		if (big_oof)
		{
			anim.Play("person_sad");
			transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z + 5f);
		}
		else
		{
			if (Input.GetKey(KeyCode.Space))
			{
				anim.Play("person_button");
			}
			else
			{
				anim.Play("person_stand");
			}
		}
	}
}
