using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {

	//Settings
	private List<ArrayList> event_data;
	
	//Start Event
	void Start () {
		//instText("What's up yo!!!!", true);
		instChoice("What's up yo!!!!", 5f, "Heyyyy yeah and I just want to make games 24/7 and it's just fun", "Ohhh yeah", "Hiyo", 0, 0, 0);
	}
	
	//Update Event
	void Update () {
		
	}

	private void instCharacter(string character, bool show)
	{
		
	}

	private void instText(string text, bool start_text)
	{
		TextScript textbox = Instantiate(Resources.Load<GameObject>("Text/Textbox")).GetComponent<TextScript>();
		textbox.text = text;
		textbox.text_begin = start_text;
		textbox.transform.SetParent(transform);
		textbox.GetComponent<RectTransform>().localPosition = new Vector3(0f, -575f, 0f);
		textbox.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
	}
	
	private void instChoice(string text, float time, string choice1, string choice2, int skip1, int skip2)
	{
		string[] choice_array = {choice1, choice2};
		ChoiceScript textbox = Instantiate(Resources.Load<GameObject>("Text/Choicebox")).GetComponent<ChoiceScript>();
		textbox.text = text;
		textbox.choice_time = time;
		textbox.transform.SetParent(transform);
		textbox.GetComponent<RectTransform>().localPosition = new Vector3(0f, -575f, 0f);
		textbox.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
		textbox.addChoiceText(choice_array);
	}

	private void instChoice(string text, float time, string choice1, string choice2, string choice3, int skip1, int skip2, int skip3)
	{
		string[] choice_array = {choice1, choice2, choice3};
		ChoiceScript textbox = Instantiate(Resources.Load<GameObject>("Text/Choicebox")).GetComponent<ChoiceScript>();
		textbox.text = text;
		textbox.choice_time = time;
		textbox.transform.SetParent(transform);
		textbox.GetComponent<RectTransform>().localPosition = new Vector3(0f, -575f, 0f);
		textbox.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
		textbox.addChoiceText(choice_array);
	}

	private List<ArrayList> createData()
	{
		List<ArrayList> data = new List<ArrayList>();
		return data;
	}
	
}
