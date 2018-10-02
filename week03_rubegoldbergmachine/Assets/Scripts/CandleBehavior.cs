using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleBehavior : MonoBehaviour
{

	public GameObject gm;
	public GameObject special;
	private Rigidbody rb;
	private bool col;
	private bool fire;
	
	// Use this for initialization
	void Start ()
	{
		rb = GetComponent<Rigidbody>();
		col = false;
		fire = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector3.Distance(transform.position, gm.transform.position) < 0.55f)
		{
			if (!col)
			{
				col = true;
				rb.constraints = RigidbodyConstraints.None;
				Destroy(gm.gameObject.GetComponent<ConstantForce>());
				Destroy(GetComponent<BoxCollider>());
			}
		}

		if (col)
		{
			if (!fire)
			{
				if (special != null)
				{
					if (Vector3.Distance(transform.position, special.transform.position) < 0.42f)
					{
						special.GetComponent<FireRopeBehavior>().addFire();
						GameObject.FindWithTag("radio").GetComponent<CameraManager>().changeCamC();
					}
				}
			}
		}
	}

}
