using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScissorBehavior : MonoBehaviour
{

	public GameObject the_one;
	public GameObject ball;
	public GameObject otherball;

	private bool fall;
	private bool hit;
	private Rigidbody rb;
	
	// Use this for initialization
	void Start ()
	{
		fall = false;
		hit = false;
		rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		if (fall)
		{
			if (!hit)
			{
				if (Mathf.Abs(transform.position.y - the_one.transform.position.y) < 0.2f)
				{
					hit = true;
					Destroy(the_one.GetComponent<SpringJoint>());
					otherball.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
				}
			}
		}
		else
		{
			if (Vector3.Distance(transform.position, ball.transform.position) < 0.87f)
			{
				rb.AddForce(new Vector3(0, 0, -1.3f), ForceMode.Impulse);
				fall = true;
				GameObject.FindWithTag("radio").GetComponent<CameraManager>().changeCamB();
			}
		}
	}
}
