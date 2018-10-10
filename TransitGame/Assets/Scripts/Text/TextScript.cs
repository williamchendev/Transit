using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextScript : MonoBehaviour {

	//Components
	private Text tx;
	
	//Settings
	public string text;
	[SerializeField] private float text_spd = 0.17f;
	
	//Variables
	private float timer;
	
	//Awake Event
	void Awake ()
	{
		timer = 0;
		tx = gameObject.transform.GetChild(0).gameObject.GetComponent<Text>();
		tx.text = "";
	}
	
	//Update Event
	void Update () {
		if (timer < text.Length)
		{
			timer += text_spd;
		}

		tx.text = text.Substring(0, Mathf.Clamp(Mathf.RoundToInt(timer), 0, text.Length - 1));
	}
}
