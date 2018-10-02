using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{


	private int timer;
	
	// Use this for initialization
	void Start ()
	{
		timer = 600;
	}
	
	// Update is called once per frame
	void Update ()
	{
		timer--;
		if (timer <= 0)
		{
			SceneManager.LoadScene("Intro", LoadSceneMode.Single);
		}
		else if (timer == 60)
		{
			addHint("Please come back for us");
		}
		else if ((timer > 300) && (timer < 360))
		{
			if (timer % 6 == 0)
			{
				addText();
			}
		}
		else if (timer < 440)
		{
			if (timer % 10 == 0)
			{
				addText();
			}
		}
		else if (timer < 500)
		{
			if (timer % 24 == 0)
			{
				addText();
			}
		}
	}

	private void addText()
	{
		int choice = Random.Range(0, 5);
		string choice_text;
		if (choice == 0)
		{
			choice_text = "why?";
		}
		else if (choice == 1)
		{
			choice_text = "we're looming";
		}
		else if (choice == 2)
		{
			choice_text = "where are we?";
		}
		else if (choice == 3)
		{
			choice_text = "come back";
		}
		else
		{
			choice_text = "please";
		}
		addHint(choice_text);
	}
	
	public void addHint(string hint)
	{
		GameObject hint_obj = Instantiate(Resources.Load<GameObject>("pHintText"));
		hint_obj.transform.SetParent(Camera.main.gameObject.transform.GetChild(0));
		hint_obj.transform.localPosition = new Vector2(0, 0);
		hint_obj.transform.localScale = new Vector2(1, 1);
		hint_obj.GetComponent<HintTextScript>().changeText(hint);
	}
}
