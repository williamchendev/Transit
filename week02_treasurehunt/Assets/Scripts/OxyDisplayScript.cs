using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OxyDisplayScript : MonoBehaviour {

	//Settings
	private RectTransform rect;
	private Text text;
	private Vector3 pos;
	
	// Use this for initialization
	void Start ()
	{
		rect = GetComponent<RectTransform>();
		text = GetComponent<Text>();
		pos = rect.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
		rect.localPosition = Vector3.Lerp(rect.localPosition, pos, Time.deltaTime * 5f);
		text.color = new Color(text.color.r, text.color.g, text.color.b, Mathf.Lerp(text.color.a, 1, Time.deltaTime * 3f));
	}

	public void shakeText(int shake)
	{
		rect.localPosition = new Vector3(pos.x + Random.Range(-shake, shake), pos.y + Random.Range(-shake, shake), pos.z);
		text.color = new Color(text.color.r, text.color.g, text.color.b, 0.7f);
	}
}
