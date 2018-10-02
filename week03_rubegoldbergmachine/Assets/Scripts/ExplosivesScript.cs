using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosivesScript : MonoBehaviour
{

	public GameObject cord;
	private bool switchy;
	
	// Use this for initialization
	void Start () {
		switchy = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (switchy)
		{
			if (cord == null)
			{
				switchy = false;
				gameObject.transform.GetChild(0).gameObject.SetActive(true);
			}
		}
	}
}
