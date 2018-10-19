using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchorScript : MonoBehaviour
{

	private GameObject player;
	private GameObject cam;
	private float sin_val;
	private float pos;
	
	// Use this for initialization
	void Start ()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		cam = GameObject.FindGameObjectWithTag("MainCamera");
		pos = transform.position.y;
		sin_val = 0;
	}
	
	// Update is called once per frame
	void Update ()
	{
		sin_val += Time.deltaTime * 0.75f;
		if (sin_val >= 1)
		{
			sin_val = 0;
		}
		float draw_sin = (Mathf.Sin(sin_val * 2 * Mathf.PI) + 1) / 2f;
		
		transform.position = new Vector3(transform.position.x, pos + (draw_sin * 1.5f), transform.position.z);
		transform.eulerAngles = new Vector3(0f, cam.transform.eulerAngles.y, 0f);
		
		Vector2 boat_tip_pos = new Vector2(player.transform.position.x, player.transform.position.z);
		Vector2 move_position = new Vector2(transform.position.x, transform.position.z);

		float size = (Mathf.Clamp(Mathf.Clamp(Vector2.Distance(boat_tip_pos, move_position) - 350f, 0, 450f) / 450f, 0, 1) * 8) + 1.5f;
		transform.localScale = new Vector3(size, size, size);
		
		if (Input.GetMouseButtonDown(0))
		{
			Destroy(gameObject);
		}
		if (Vector2.Distance(boat_tip_pos, move_position) < 6.5f)
		{
			Destroy(gameObject);
		}
	}
}
