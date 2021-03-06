﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ChoiceScript : MonoBehaviour {

	//Components
	private Text tx;
	public EventManager em { private get; set; }

	//Settings
	public string text { set; private get; }
	public int[] skips { set; private get; }
	public float choice_time { set; private get; }
	public bool text_finished { private set; get; }
	[SerializeField] private float text_spd = 0.17f;
	
	//Variables
	private int trip;
	private bool clicked;
	private float choice_end_timer;
	private int skip_select;
	private float timer;
	private float choice_timer;
	private List<ChoicesBehavior> choices;
	private Material m_material;
	private Shader shader;
	
	//Awake Event
	void Awake ()
	{
		trip = 5;
		timer = 0;
		text = "Hi";
		tx = gameObject.transform.GetChild(0).gameObject.GetComponent<Text>();
		tx.text = "";

		clicked = false;
		text_finished = false;
		m_material = material;
		transform.GetChild(1).transform.gameObject.GetComponent<Image>().material = m_material;
		choice_end_timer = 1f;
		choice_timer = 0;
		skip_select = 0;
		
		choices = new List<ChoicesBehavior>();
	}
	
	//Update Event
	void Update () {
		//Update Text
		if (timer < text.Length)
		{
			timer += text_spd;
		}

		tx.text = text.Substring(0, Mathf.Clamp(Mathf.RoundToInt(timer), 0, text.Length));

		//Text Click
		if (Input.GetMouseButtonDown(0))
		{
			if (timer < text.Length)
			{
				timer = text.Length;
			}
			else
			{
				text_finished = true;
			}
		}
		
		//Choice Time
		if (clicked)
		{
			m_material.SetFloat("_Threshold", 1);
			choice_end_timer = Mathf.Lerp(choice_end_timer, 0f, Time.deltaTime * 3.5f);
			if (choice_end_timer <= 0.1f)
			{
				em.choice = skip_select;
				gameObject.tag = "Untagged";
				Destroy(gameObject);
				em.refresh();
			}
		}
		else
		{
			choice_timer += Time.deltaTime * 1f;
			float choice_show = Mathf.Clamp(choice_timer / choice_time, 0, 1);
			m_material.SetFloat("_Threshold", choice_show);

			if (choice_timer >= choice_time)
			{
				trip--;
				if (trip <= 0)
				{
					clicked = true;
					skip_select = skips[skips.Length - 1];
					for (int i = 0; i < choices.Count; i++)
					{
						choices[i].selected = true;
					}
				}
			}
		}
	}
	
	public void addChoiceText(string[] new_text)
	{
		for (int i = 0; i < new_text.Length; i++)
		{
			ChoicesBehavior choice = transform.GetChild(2).transform.GetChild(i).gameObject.AddComponent<ChoicesBehavior>();
			choice.choice_script = this;
			choice.text = new_text[i];
			choice.skip = i;
			choices.Add(choice);
		}

		if (choices.Count == 2)
		{
			choices[0].move_pos = new Vector2(-390f, 0f);
			choices[1].move_pos = new Vector2(390f, 0f);
			Destroy(transform.GetChild(2).transform.GetChild(2).gameObject);
		}
		else
		{
			choices[0].move_pos = new Vector2(-390f, -110f);
			choices[1].move_pos = new Vector2(0f, 140f);
			choices[2].move_pos = new Vector2(390f, -110f);
		}
	}

	public void setChoice(int skip_num)
	{
		if (!clicked)
		{
			clicked = true;
			skip_select = skips[skip_num];
			for (int i = 0; i < choices.Count; i++)
			{
				choices[i].selected = true;
			}
		}
	}
	
	private Material material {
		get {
			if (m_material == null) {
				shader = Shader.Find("Custom/TextTimerSpriteShader");
				m_material = new Material(shader) {hideFlags = HideFlags.DontSave};
				m_material.SetFloat("_Threshold", 0f);
				m_material.SetFloat("_TexSize", Screen.width / 2f);
				Update();
			}

			return m_material;
		}
	}
	
}

public class ChoicesBehavior : MonoBehaviour {
	
	//Components
	private Text tx;
	private RectTransform trans;
	private Collider2D col;
	public ChoiceScript choice_script { set; private get; }

	//Settings
	public bool selected { set; private get; }
	private int skip_num;
	public Vector2 move_pos { set; private get; }
	private float[] random_offset;
	private float displace_length;
	private float sin_val;
	private float sin_val2;
	private float mouse_hover;
	
	void Awake()
	{
		//Components
		tx = gameObject.GetComponent<Text>();
		tx.text = "";
		tx.color = new Color(tx.color.r, tx.color.g, tx.color.b, 0f);

		trans = gameObject.GetComponent<RectTransform>();

		//Settings
		skip_num = -1;
		sin_val = 0f;
		sin_val2 = 0f;
		displace_length = 25f;
		random_offset = new float[]{Random.Range(0f, Mathf.PI * 2f), Random.Range(0f, Mathf.PI * 2f)};
		mouse_hover = 0f;
	}

	void Start()
	{
		GameObject col_obj = new GameObject("TextMesh", typeof(RectTransform));
		col_obj.AddComponent<TextMesh>().text = resolveTextSize(tx.text, 30);
		col_obj.GetComponent<TextMesh>().characterSize = 1.25f;
		col_obj.GetComponent<TextMesh>().lineSpacing = 1.05f;
		col_obj.GetComponent<TextMesh>().anchor = TextAnchor.MiddleCenter;
		col_obj.GetComponent<TextMesh>().alignment = TextAlignment.Center;
		col_obj.GetComponent<TextMesh>().fontSize = 220;
		col_obj.transform.SetParent(transform);
		col_obj.GetComponent<RectTransform>().localPosition = new Vector3(0f, 0f, 0f);
		col_obj.GetComponent<RectTransform>().localScale = new Vector3(0.9f, 1f, 1f) * 4.5f;
		col = col_obj.gameObject.AddComponent<BoxCollider2D>();
		Destroy(col_obj.GetComponent<TextMesh>());
	}
	
	void Update()
	{
		//Check Clicked
		Vector3 v3 = Input.mousePosition;
		v3.z = 0;
		if (col.bounds.Contains(v3))
		{
			mouse_hover = Mathf.Lerp(mouse_hover, 1, Time.deltaTime * 2.5f);
			if (Input.GetMouseButtonDown(0))
			{
				choice_script.setChoice(skip_num);
			}
		}
		else
		{
			mouse_hover = Mathf.Lerp(mouse_hover, 0, Time.deltaTime * 2.5f);
		}
		
		//Visual Calculations
		sin_val += 0.0009f;
		if (sin_val >= 1)
		{
			sin_val = 0f;
		}
		sin_val2 += 0.007f;
		if (sin_val2 >= 1)
		{
			sin_val2 = 0f;
		}
		float draw_val = (Mathf.Sin((sin_val2 * Mathf.PI * 2) + random_offset[0]));
		float scale_displace = (((Mathf.Sin((sin_val2 * Mathf.PI * 2) + random_offset[1]) + 1) / 2) * 0.037f) + (mouse_hover * 0.12f);
		float temp_displace = draw_val * displace_length;

		Vector2 temp_move_pos = move_pos + (temp_displace * new Vector2(Mathf.Cos((sin_val * Mathf.PI * 2) + random_offset[1]), Mathf.Sin((sin_val * Mathf.PI * 2) + random_offset[1])));
		if (selected)
		{
			temp_move_pos = temp_displace * new Vector2(Mathf.Cos((sin_val * Mathf.PI * 2) + random_offset[1]), Mathf.Sin((sin_val * Mathf.PI * 2) + random_offset[1]));
			trans.localPosition = Vector3.Lerp(trans.localPosition, new Vector3(temp_move_pos.x, temp_move_pos.y, 0f), Time.deltaTime * 1.5f);
			tx.color = new Color(tx.color.r, tx.color.g, tx.color.b, Mathf.Lerp(tx.color.a, 0f, Time.deltaTime * 4.5f));
		}
		else
		{
			trans.localPosition = Vector3.Lerp(trans.localPosition, new Vector3(temp_move_pos.x, temp_move_pos.y, 0f), Time.deltaTime * 2.5f);
			tx.color = new Color(tx.color.r, tx.color.g, tx.color.b, Mathf.Lerp(tx.color.a, 1f, Time.deltaTime * 2f));
		}
		
		trans.localScale = new Vector3(0.5f + scale_displace, 0.5f + scale_displace, 0.5f);
	}

	public string text
	{
		set
		{
			tx.text = value;
		} 
	}

	public int skip
	{
		set
		{
			skip_num = value; 
		}
	}
	
	private string resolveTextSize(string input, int lineLength){
 
		// Split string by char " "         
		string[] words = input.Split(" "[0]);
		string result = "";
		string line = "";      
		
		foreach(string s in words){
			string temp = line + " " + s;
			if(temp.Length > lineLength){
				result += line + "\n";
				line = s;
			}
			else {
				line = temp;
			}
		}
   
		result += line;
		return result.Substring(1,result.Length-1);
	}
	
}
