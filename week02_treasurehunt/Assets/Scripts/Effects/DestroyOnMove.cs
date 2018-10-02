using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnMove : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehavior>().can_move())
		{
			Destroy(gameObject);
		}
	}
}
