using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterScript : MonoBehaviour {

	//Components
	private Image img;
	private RectTransform rect;
	
	//Settings
	public bool show { set; private get; }
	public bool destroy_obj { set; private get; }
	
	[SerializeField] private int character_id;
	[SerializeField] private bool right_side;

	//Variables
	private float sin_val;
	private float show_low;
	private float show_lerp;

	private Vector2 start_position;
	private Vector2 end_position;

	private int rotate_index;
	private float rotate_timer;
	private float[] x_rotate;
	private float[] z_rotate;
	
	//Init
	void Start ()
	{
		//Components
		img = GetComponent<Image>();
		rect = GetComponent<RectTransform>();
		
		//Settings
		show = false;
		destroy_obj = false;
		
		//Variables
		sin_val = 0f;
		show_low = 0.7f;
		show_lerp = 0f;

		float offset = 150;
		if (right_side)
		{
			offset = -150;
		}
		start_position = new Vector2(rect.localPosition.x - offset, rect.localPosition.y - Mathf.Abs(offset));
		end_position = new Vector2(rect.localPosition.x, rect.localPosition.y);

		rotate_index = Random.Range(0, 4);
		rotate_timer = 0f;
		z_rotate = new float[] { 0.5f, -1, 1, 0, -0.5f };
		x_rotate = new float[] { 2, -0.5f, 1.75f, 0.5f, 1f };

		gameObject.tag = "Character";
		Update();
	}
	
	//Update
	void Update ()
	{
		//Sin Val
		sin_val += Time.deltaTime * 0.27f;
		if (sin_val >= 1)
		{
			sin_val = 0;
		}
		float draw_sin = (Mathf.Sin(sin_val * 2 * Mathf.PI) + 1) / 2;
		
		//Color & Alpha
		if (!destroy_obj)
		{
			if (show)
			{
				show_lerp = Mathf.Lerp(show_lerp, 1f, Time.deltaTime * 2.5f);
			}
			else
			{
				show_lerp = Mathf.Lerp(show_lerp, show_low, Time.deltaTime * 2.5f);
			}
		}
		else
		{
			show_lerp = Mathf.Lerp(show_lerp, 0f, Time.deltaTime * 2f);
			if (show_lerp < 0.05f)
			{
				Destroy(gameObject);
			}
		}
		float greyscale_val = Mathf.Clamp(show_lerp, show_low, 1f);
		Color sprite_color = new Color(greyscale_val, greyscale_val, greyscale_val, show_lerp);
		img.color = sprite_color;
		
		//Position
		Vector2 new_pos = Vector2.Lerp(start_position, end_position, show_lerp);
		rect.transform.localPosition = new Vector3(new_pos.x, new_pos.y + (draw_sin * 30), 0f);
		
		//Rotation
		rotate_timer += Time.deltaTime;
		if (rotate_timer >= 1)
		{
			rotate_timer = 0;
			rotate_index++;
			if (rotate_index >= z_rotate.Length)
			{
				rotate_index = 0;
			}
		}
		rect.transform.localEulerAngles = new Vector3(x_rotate[rotate_index] * 3, x_rotate[rotate_index], z_rotate[rotate_index]);
	}
	
	//Getter & Setters
	public int id
	{
		get
		{
			return character_id;
		}
	}
}
