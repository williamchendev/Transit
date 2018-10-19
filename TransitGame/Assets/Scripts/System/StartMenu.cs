using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu : MonoBehaviour
{
	private bool start;
	
	// Update is called once per frame
	void Update () {
		if (!start)
		{
			if (Input.GetMouseButtonDown(0))
			{
				start = true;
				Transition trans = Instantiate(Resources.Load<GameObject>("Transition").GetComponent<Transition>());
				trans.trans_in = true;
				trans.scene_change = "Morning";
			}
		}
	}
}
