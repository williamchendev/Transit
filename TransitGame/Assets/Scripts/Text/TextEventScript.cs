using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextEventScript : MonoBehaviour {

	//Components
	private RectTransform rect;
	private Collider2D col;
	private Image img;

	public List<ArrayList> event_data { set; private get; }
	public bool destroy_obj { set; private get; }
	public DialogueTrigger trigger { set; private get; }

	//Variables
	private bool clicked;
	private float pos_lerp;
	private Vector2 pos_start;
	private Vector2 pos_end;

	private float[] tilt;
	private int tilt_index;
	private float tilt_timer;
	
	// Use this for initialization
	void Awake ()
	{
		//Components
		rect = GetComponent<RectTransform>();
		rect.localPosition = new Vector3(840, -550, 0);
		col = GetComponent<Collider2D>();
		img = GetComponent<Image>();
		img.color = new Color(img.color.r, img.color.g, img.color.b, 0);
		
		//Variables
		clicked = false;
		pos_lerp = 0;
		pos_start = new Vector2(840, -550);
		pos_end = new Vector2(840, -450);
		destroy_obj = false;

		tilt = new float[]{5, -5, 0, 3, -4, 3, 1, -2, 0};
		tilt_index = 0;
		tilt_timer = 0.5f;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		//Check Clicked
		if (!destroy_obj)
		{
			if (clicked)
			{
				pos_lerp = Mathf.Lerp(pos_lerp, 0, Time.deltaTime * 3.5f);
				if (pos_lerp <= 0.15f)
				{
					if (trigger != null)
					{
						trigger.triggerDialogue();
					}
					transform.parent.gameObject.GetComponent<EventManager>().startEvent(event_data);
					Destroy(gameObject);
				}
			}
			else
			{
				Vector3 v3 = Input.mousePosition;
				v3.z = 0;

				if (col.bounds.Contains(v3))
				{
					if (Input.GetMouseButtonDown(0))
					{
						BoatBehavior player = GameObject.FindGameObjectWithTag("Player").GetComponent<BoatBehavior>();
						player.canmove = false;
						clicked = true;
						if (GameObject.FindGameObjectWithTag("Anchor") != null)
						{
							Destroy(GameObject.FindGameObjectWithTag("Anchor").gameObject);
						}
					}
				}

				pos_lerp = Mathf.Lerp(pos_lerp, 1, Time.deltaTime * 3.5f);
			}
		}
		else
		{
			pos_lerp = Mathf.Lerp(pos_lerp, 0, Time.deltaTime * 3.5f);
			if (pos_lerp <= 0.15f)
			{
				Destroy(gameObject);
			}
		}
		
		//Tilt
		tilt_timer -= Time.deltaTime;
		if (tilt_timer <= 0)
		{
			tilt_timer = 0.5f;
			tilt_index++;
			if (tilt_index >= tilt.Length)
			{
				tilt_index = 0;
			}
			rect.localEulerAngles = new Vector3(0f, 0f, tilt[tilt_index]);
		}
		
		//Update Position
		img.color = new Color(img.color.r, img.color.g, img.color.b, Mathf.Clamp(pos_lerp, 0, 1));
		Vector2 new_pos = Vector2.Lerp(pos_start, pos_end, pos_lerp);
		rect.localPosition = new Vector3(new_pos.x, new_pos.y, 0f);
	}
}
