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
	
	
	public void setSkip(int skip)
	{
		//Skip events
	}

	public void setText(string text)
	{
		TextScript textbox = Instantiate(Resources.Load<GameObject>("Text/Textbox")).GetComponent<TextScript>();
		textbox.text = text;
	}
	
}
