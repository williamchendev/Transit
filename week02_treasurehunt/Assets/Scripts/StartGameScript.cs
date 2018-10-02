using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameScript : MonoBehaviour
{

	private bool begin;
	// Use this for initialization
	void Start ()
	{
		begin = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0))
		{
			if (!begin)
			{
				GameObject black_obj = Instantiate(Resources.Load<GameObject>("BlackScreenTrans"));
				black_obj.transform.SetParent(Camera.main.gameObject.transform.GetChild(0));
				black_obj.transform.localPosition = new Vector2(0, 0);
				begin = true;
			}
		}
	}
}
