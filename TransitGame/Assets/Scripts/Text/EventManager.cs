using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {

	//Settings
	private bool event_active;
	private int event_num;
	private List<ArrayList> event_data;

	private Vector2 text_bubble_pos;
	private float text_bubble_radius;
	private TextEventScript text_bubble;
	
	//Variables
	private bool choice_made;
	private int choice_num;
	
	//Start Event
	void Start ()
	{
		event_num = 0;
		event_data = TextData.getData(0);
		
		//startEvent();
	}
	
	//Update Event
	void Update () {
		//Event Management
		if (event_active)
		{
			//Event Parsing & Instantiation
			if (GameObject.FindGameObjectWithTag("Text") == null)
			{
				if (choice_made)
				{
					event_num--;
					event_num += choice_num;
					choice_made = false;
				}
				
				if (event_num < event_data.Count)
				{
					ArrayList event_command = event_data[event_num];
					int event_type = (int) event_command[0];

					if (event_type == 0)
					{
						string text = (string) event_command[1];
						int character_active = (int) event_command[2];
						bool start_text = false;
						bool end_text = false;
						int first_index = -1;
						int last_index = -1;
						for (int i = 0; i < event_data.Count; i++)
						{
							ArrayList temp_event = event_data[i];
							if (((int) temp_event[0]) == 0)
							{
								if (first_index == -1)
								{
									first_index = i;
								}
								last_index = i;
							}
						}

						if (first_index == event_num)
						{
							start_text = true;
						}
						if (last_index == event_num)
						{
							end_text = true;
						}
						instText(text, start_text, end_text);
						
						GameObject[] characters = GameObject.FindGameObjectsWithTag("Character");
						foreach (GameObject character in characters)
						{
							character.GetComponent<CharacterScript>().show = false;
							if (character.GetComponent<CharacterScript>().id == character_active)
							{
								character.GetComponent<CharacterScript>().show = true;
							}
						}
					}
					else if (event_type == 1)
					{
						string text = (string) event_command[1];
						float time = (float) event_command[2];
						string choice1 = (string) event_command[3];
						string choice2 = (string) event_command[4];
						int skip1 = (int) event_command[5];
						int skip2 = (int) event_command[6];
						int skip_time = (int) event_command[7];
						instChoice(text, time, choice1, choice2, skip1, skip2, skip_time);

						GameObject[] characters = GameObject.FindGameObjectsWithTag("Character");
						foreach (GameObject character in characters)
						{
							character.GetComponent<CharacterScript>().show = false;
						}
					}
					else if (event_type == 2)
					{
						string text = (string) event_command[1];
						float time = (float) event_command[2];
						string choice1 = (string) event_command[3];
						string choice2 = (string) event_command[4];
						string choice3 = (string) event_command[5];
						int skip1 = (int) event_command[6];
						int skip2 = (int) event_command[7];
						int skip3 = (int) event_command[8];
						int skip_time = (int) event_command[9];
						instChoice(text, time, choice1, choice2, choice3, skip1, skip2, skip3, skip_time);
						
						GameObject[] characters = GameObject.FindGameObjectsWithTag("Character");
						foreach (GameObject character in characters)
						{
							character.GetComponent<CharacterScript>().show = false;
						}
					}
					else if (event_type == 3)
					{
						int skip_num = (int) event_command[1];
						event_num += skip_num - 1;
					}
					else if (event_type == 4)
					{
						string character_name = (string) event_command[1];
						instCharacter(character_name);
					}
					else if (event_type == 5)
					{
						GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraBehavior>().compass_show = true;
					}
					else if (event_type == 6)
					{
						Transition trans = Instantiate(Resources.Load<GameObject>("Transition").GetComponent<Transition>());
						trans.trans_in = true;
						trans.scene_change = "StartMenu";
					}

					event_num++;
				}
				else
				{
					endEvent();
				}
			}
			
			//Text Bubble Management
			if (bubble_exists)
			{
				text_bubble.destroy_obj = true;
			}
		}
		
		//Text Bubble Distance Condition
		if (bubble_exists)
		{
			Vector3 player_position = GameObject.FindGameObjectWithTag("Player").transform.position;
			if (Vector2.Distance(new Vector2(player_position.x, player_position.z), text_bubble_pos) > text_bubble_radius)
			{
				text_bubble.destroy_obj = true;
			}
		}
	}

	//Event Methods
	public void startEvent()
	{
		event_num = 0;
		BoatBehavior player = GameObject.FindGameObjectWithTag("Player").GetComponent<BoatBehavior>();
		player.canmove = false;
		event_active = true;
	}
	
	public void startEvent(List<ArrayList> new_data)
	{
		if (new_data != null)
		{
			event_data = new_data;
		}
		startEvent();
	}

	public void endEvent()
	{
		BoatBehavior player = GameObject.FindGameObjectWithTag("Player").GetComponent<BoatBehavior>();
		player.canmove = true;
		event_active = false;
	}

	public void refresh()
	{
		Update();
	}

	//Instantiate Dialogue Event Object Methods
	private void instCharacter(string character)
	{
		CharacterScript character_obj = Instantiate(Resources.Load<GameObject>("Characters/" + character)).GetComponent<CharacterScript>();
		character_obj.transform.SetParent(transform);
		character_obj.GetComponent<RectTransform>().localPosition = Resources.Load<GameObject>("Characters/" + character).GetComponent<RectTransform>().localPosition;
		character_obj.GetComponent<RectTransform>().localEulerAngles = Resources.Load<GameObject>("Characters/" + character).GetComponent<RectTransform>().localEulerAngles;
		character_obj.GetComponent<RectTransform>().localScale = Resources.Load<GameObject>("Characters/" + character).GetComponent<RectTransform>().localScale;
	}

	private void instText(string text, bool start_text, bool end_text)
	{
		TextScript textbox = Instantiate(Resources.Load<GameObject>("Text/Textbox")).GetComponent<TextScript>();
		textbox.em = this;
		textbox.text = text;
		textbox.text_begin = start_text;
		textbox.text_end = end_text;
		textbox.transform.SetParent(transform);
		textbox.GetComponent<RectTransform>().localPosition = new Vector3(0f, -575f, 0f);
		textbox.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
		textbox.gameObject.tag = "Text";
	}
	
	private void instChoice(string text, float time, string choice1, string choice2, int skip1, int skip2, int skip_time)
	{
		string[] choice_array = {choice1, choice2};
		int[] skip_array = {skip1, skip2, skip_time};
		ChoiceScript textbox = Instantiate(Resources.Load<GameObject>("Text/Choicebox")).GetComponent<ChoiceScript>();
		textbox.em = this;
		textbox.text = text;
		textbox.choice_time = time;
		textbox.transform.SetParent(transform);
		textbox.GetComponent<RectTransform>().localPosition = new Vector3(0f, -575f, 0f);
		textbox.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
		textbox.addChoiceText(choice_array);
		textbox.skips = skip_array;
		textbox.gameObject.tag = "Text";
	}

	private void instChoice(string text, float time, string choice1, string choice2, string choice3, int skip1, int skip2, int skip3, int skip_time)
	{
		string[] choice_array = {choice1, choice2, choice3};
		int[] skip_array = {skip1, skip2, skip3, skip_time};
		ChoiceScript textbox = Instantiate(Resources.Load<GameObject>("Text/Choicebox")).GetComponent<ChoiceScript>();
		textbox.em = this;
		textbox.text = text;
		textbox.choice_time = time;
		textbox.transform.SetParent(transform);
		textbox.GetComponent<RectTransform>().localPosition = new Vector3(0f, -575f, 0f);
		textbox.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
		textbox.addChoiceText(choice_array);
		textbox.skips = skip_array;
		textbox.gameObject.tag = "Text";
	}
	
	//Public Methods
	public void createTextBubble(Vector2 bubble_position, float bubble_radius, List<ArrayList> bubble_data)
	{
		text_bubble_pos = bubble_position;
		text_bubble_radius = bubble_radius;
		text_bubble = Instantiate(Resources.Load<GameObject>("Text/TextBubble")).GetComponent<TextEventScript>();
		text_bubble.event_data = bubble_data;
		text_bubble.GetComponent<RectTransform>().SetParent(transform);
		text_bubble.GetComponent<RectTransform>().localScale = new Vector3(2, 2, 2);
	}
	
	//Public Methods
	public void createTextBubble(Vector2 bubble_position, float bubble_radius, List<ArrayList> bubble_data, DialogueTrigger bubble_trigger)
	{
		createTextBubble(bubble_position, bubble_radius, bubble_data);
		text_bubble.trigger = bubble_trigger;
	}

	//Getter & Setter Methods
	public int choice
	{
		set
		{
			choice_made = true;
			choice_num = value;
		}
	}

	public bool bubble_exists
	{
		get
		{
			if (event_active)
			{
				return true;
			}
			if (text_bubble != null)
			{
				return true;
			}
			return false;
		}
	}
	
}

public static class EventData
{
	
	public static ArrayList addTextData(string text, int charcter_active)
	{
		ArrayList array = new ArrayList();
		array.Add(0);
		array.Add(text);
		array.Add(charcter_active);
		return array;
	}
	
	public static ArrayList addChoiceData(string text, float time, string choice1, string choice2, int skip1, int skip2, int skip_time)
	{
		ArrayList array = new ArrayList();
		array.Add(1);
		array.Add(text);
		array.Add(time);
		array.Add(choice1);
		array.Add(choice2);
		array.Add(skip1);
		array.Add(skip2);
		array.Add(skip_time);
		return array;
	}
	
	public static ArrayList addChoiceData(string text, float time, string choice1, string choice2, string choice3, int skip1, int skip2, int skip3, int skip_time)
	{
		ArrayList array = new ArrayList();
		array.Add(2);
		array.Add(text);
		array.Add(time);
		array.Add(choice1);
		array.Add(choice2);
		array.Add(choice3);
		array.Add(skip1);
		array.Add(skip2);
		array.Add(skip3);
		array.Add(skip_time);
		return array;
	}
	
	public static ArrayList addCharacterData(int character_num)
	{
		ArrayList array = new ArrayList();
		array.Add(4);
		string character_name = "DebugCharacter";
		if (character_num == 1)
		{
			character_name = "Will";
		}
		else if (character_num == 2)
		{
			character_name = "JohnMichael";
		}
		array.Add(character_name);
		return array;
	}
	
	public static ArrayList addSkipData(int skip_num)
	{
		ArrayList array = new ArrayList();
		array.Add(3);
		array.Add(skip_num);
		return array;
	}
	
	public static ArrayList addCompass()
	{
		ArrayList array = new ArrayList();
		array.Add(5);
		return array;
	}
	
	public static ArrayList addEndGame()
	{
		ArrayList array = new ArrayList();
		array.Add(6);
		return array;
	}
	
}
