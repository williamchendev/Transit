using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HintTextScript : MonoBehaviour
{

	//Variables
	private string text;
	private Text back;
	private Text front;
	private Vector2 pos;

	private int timer;
	private int big_timer;
	
	//Start
	void Start () {
		pos = new Vector2(0, 0);
		pos += new Vector2(Random.Range(-640, 640), Random.Range(-360, 360));
		back = transform.GetChild(0).gameObject.GetComponent<Text>();
		front = transform.GetChild(1).gameObject.GetComponent<Text>();
		back.text = text;
		front.text = text;

		float size_change = Random.Range(0.8f, 1.6f);
		back.transform.localScale = new Vector2(back.transform.localScale.x * size_change, back.transform.localScale.y * size_change);
		front.transform.localScale = new Vector2(front.transform.localScale.x * size_change, front.transform.localScale.y * size_change);
		
		timer = 0;
		big_timer = 80;
		
		Update();
	}
	
	// Update is called once per frame
	void Update ()
	{
		timer--;
		if (timer <= 0)
		{
			float offset = 7f;
			back.transform.localPosition = new Vector3(pos.x + Random.Range(-offset, offset), pos.y + Random.Range(-offset, offset), back.transform.localPosition.z);
			front.transform.localPosition = new Vector3(pos.x + Random.Range(-offset, offset), pos.y + Random.Range(-offset, offset), front.transform.localPosition.z);
			timer = Random.Range(2, 5);
		}

		big_timer--;
		if (big_timer <= 0)
		{
			Destroy(gameObject);
		}
	}

	public void changeText(string new_text)
	{
		text = new_text;
	}
}
