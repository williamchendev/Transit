using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableBehavior : MonoBehaviour
{

	public bool oxy;

	public void interact()
	{
		if (oxy)
		{
			GameObject player = GameObject.FindGameObjectWithTag("Player");
			player.GetComponent<PlayerBehavior>().addOxy();
			Destroy(gameObject);
		}
		else
		{
			int type = GetComponent<PieceScript>().type;
			GameObject player = GameObject.FindGameObjectWithTag("Player");
			player.GetComponent<PlayerBehavior>().addImage(type);
			Destroy(gameObject);
		}
	}
}
