using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

	//Singleton
	public static PlayerManager instance { get; private set; }
	
	//Settings
	public bool canmove { get; private set; }

	//Variables

	// Use this for initialization
	void Awake () {
		//Create Singleton
		if (GameObject.FindGameObjectWithTag("Player") != null)
		{
			Destroy(gameObject);
		}
		else
		{
			gameObject.tag = "Player";
			instance = gameObject.GetComponent<PlayerManager>();
		}
		
		//Settings
		canmove = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public bool cursorclick
	{
		get
		{
			if (Input.GetMouseButtonDown(0))
			{
				return true;
			}
			return false;
		}
	}
	
	public Vector3 cursorpos
	{
		get
		{
			Vector3 v3 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			v3.z = 0;
			return v3;
		}
	}

	public void setSkip(int skip)
	{
		//Skip events
	}

	public void continueEvent()
	{
		
	}
	
}
