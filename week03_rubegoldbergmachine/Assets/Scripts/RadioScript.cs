using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioScript : MonoBehaviour
{

	public GameObject ball;
	private bool shake;
	private Vector3 pos;

	private GameObject[] firepart;
	
	// Use this for initialization
	void Start ()
	{
		shake = false;
		pos = transform.position;
		
		firepart = GameObject.FindGameObjectsWithTag("particles");
		foreach (GameObject part in firepart)
		{
			part.SetActive(false);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (shake)
		{
			float xn = 0.05f;
			transform.position = new Vector3(pos.x + Random.Range(-xn, xn), pos.y + Random.Range(-xn, xn), pos.z + Random.Range(-xn, xn));
			xn = 5f;
			transform.eulerAngles = new Vector3(Random.Range(-xn, xn), Random.Range(-xn, xn), Random.Range(-xn, xn));
		}
		else
		{
			if (Vector3.Distance(transform.position, ball.transform.position) < 0.8f)
			{
				shake = true;
				GetComponent<CameraManager>().changeCamMain();
				foreach (GameObject part in firepart)
				{
					part.SetActive(true);
				}
			}
		}
	}
}
