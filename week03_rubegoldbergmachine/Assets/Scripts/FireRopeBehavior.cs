using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRopeBehavior : MonoBehaviour
{

	private bool fire;
	private int timer;

	private SpringJoint sp;
	
	// Use this for initialization
	void Start ()
	{
		fire = false;
		timer = 180;
		sp = GetComponent<SpringJoint>();
	}
	
	// Update is called once per frame
	void Update () {
		if (fire)
		{
			timer--;
			if (timer <= 0)
			{
				Destroy(gameObject);
			}
			else if (timer <= 140)
			{
				if (sp.connectedBody.GetComponent<FireRopeBehavior>() != null)
				{
					sp.connectedBody.GetComponent<FireRopeBehavior>().addFire();
				}
			}
		}
	}

	public void addFire()
	{
		if (!fire)
		{
			fire = true;
			GameObject fire_part = Instantiate(Resources.Load<GameObject>("fireparticle"));
			fire_part.transform.parent = gameObject.transform;
			fire_part.transform.localPosition = new Vector3(0f, 0f, 0f);
			fire_part.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
		}
	}

}
