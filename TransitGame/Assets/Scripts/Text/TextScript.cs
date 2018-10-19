using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextScript : MonoBehaviour {

	//Components
	private Text tx;
	public EventManager em { private get; set; }
	
	//Settings
	public string text { set; private get; }
	public bool text_begin { set; private get; }
	public bool text_end { set; private get; }
	private bool text_stable;
	private float text_begin_alpha;
	private float text_end_alpha;
	public bool text_finished { private set; get; }
	[SerializeField] private float text_spd = 0.17f;
	[SerializeField] private Material default_mat;
	
	//Variables
	private bool clicked;
	private float timer;
	private Material m_material;
	private Shader shader;
	
	//Awake Event
	void Awake ()
	{
		timer = 0;
		text = "Hi";
		tx = gameObject.transform.GetChild(0).gameObject.GetComponent<Text>();
		tx.text = "";

		text_stable = false;
		text_finished = false;
		m_material = material;
		text_begin_alpha = 0;
		text_end_alpha = 1;
		GetComponent<Image>().material = m_material;
		clicked = false;
	}

	void Start()
	{
		if (!text_begin)
		{
			m_material.SetFloat("_Threshold", 0f);
		}
	}
	
	//Update Event
	void Update () {
		if (text_begin)
		{
			if (!text_stable)
			{
				text_stable = true;
				text_begin_alpha = 1;
			}
			
			if (text_begin_alpha > 0.01f)
			{
				text_begin_alpha = Mathf.Lerp(text_begin_alpha, 0, Time.deltaTime * 5f);
			}
			else
			{
				text_begin = false;
			}

			m_material.SetFloat("_Threshold", Mathf.Clamp(text_begin_alpha, 0, 1));
		}
		else
		{
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
					if (!text_end)
					{
						text_finished = true;
						gameObject.tag = "Untagged";
						Destroy(gameObject);
						em.refresh();
					}
					else
					{
						if (!clicked)
						{
							GameObject[] characters = GameObject.FindGameObjectsWithTag("Character");
							foreach (GameObject character in characters)
							{
								character.GetComponent<CharacterScript>().destroy_obj = true;
							}

							GetComponent<Image>().material = default_mat;
						}
					}

					clicked = true;
				}
			}
		}

		if (clicked)
		{
			if (text_end)
			{
				if (text_end_alpha > 0.02f)
				{
					text_end_alpha = Mathf.Lerp(text_end_alpha, 0, Time.deltaTime * 2.5f);
				}
				else
				{
					text_finished = true;
					gameObject.tag = "Untagged";
					Destroy(gameObject);
				}

				Color tmp = tx.color;
				tmp.a = text_end_alpha;
				tx.color = tmp;
				tmp = GetComponent<Image>().color;
				tmp.a = text_end_alpha;
				GetComponent<Image>().color = tmp;
				
				GetComponent<RectTransform>().localPosition = Vector3.Lerp(GetComponent<RectTransform>().localPosition, new Vector3(0f, -775, 0), Time.deltaTime * 2.5f);
			}
		}
	}
	
	private Material material {
		get {
			if (m_material == null) {
				shader = Shader.Find("Inno/DissolveSpriteShader");
				m_material = new Material(shader) {hideFlags = HideFlags.DontSave};
				m_material.SetFloat("_Threshold", 1f);
				Update();
			}

			return m_material;
		}
	}
}
