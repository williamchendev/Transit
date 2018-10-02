using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceScript : MonoBehaviour
{

	public int type;
	public string text;
	private int timer;
	
	// Use this for initialization
	void Start ()
	{
		timer = 0;
	}
	
	// Update is called once per frame
	void Update () {
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		if (Vector3.Distance(player.transform.position, transform.position) < 12f)
		{
			timer--;
			if (timer <= 0)
			{
				if (Vector3.Distance(player.transform.position, transform.position) < 3f)
				{
					timer = 8;
					for (int i = 0; i < 2; i++)
					{
						player.GetComponent<PlayerBehavior>().addHint(text);
					}
				}
				else if (Vector3.Distance(player.transform.position, transform.position) < 7f)
				{
					timer = 12;
					player.GetComponent<PlayerBehavior>().addHint(text);
				}
				else
				{
					timer = 24;
					player.GetComponent<PlayerBehavior>().addHint(text);
				}
			}
		}
	}
}
