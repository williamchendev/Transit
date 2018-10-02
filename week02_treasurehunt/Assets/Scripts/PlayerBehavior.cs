using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehavior : MonoBehaviour {

	//Components
	private Rigidbody rb;
	private bool canmove;
	private bool inspect;
	private bool dead;
	private bool win;
	private bool end;
	private int artifacts;
	private float spd;
	
	//Settings
	public int oxy;
	private float oxy_display;
	private int oxy_timer;
	
	// Use this for initialization
	void Start ()
	{
		rb = GetComponent<Rigidbody>();
		spd = 7f;
		canmove = true;
		inspect = false;
		dead = false;
		win = false;
		end = false;
		artifacts = 0;

		oxy = 100;
		oxy_display = 100;
		oxy_timer = 200;
		
		Camera.main.gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
	}
	
	// Update is called once per frame
	void Update () {
	    //Interactables
		GameObject interactable = null;
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2f, Screen.height / 2f));

		if (Physics.Raycast(ray, out hit)) {
			if (hit.transform.gameObject.CompareTag("Interactable")) {
				if (Vector3.Distance(hit.transform.gameObject.transform.position, transform.position) < 2.5f) {
					interactable = hit.transform.gameObject;
				}
			}
		}

		float alpha = Camera.main.gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().color.a;
		float dot_alpha = Camera.main.gameObject.transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.GetComponent<Image>().color.a;
		if (interactable != null) {
			Camera.main.gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f, Mathf.SmoothStep(alpha, 1, 0.11f));
			Camera.main.gameObject.transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f, Mathf.SmoothStep(dot_alpha, 0, 0.16f));
		}
		else
		{
			Camera.main.gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f, Mathf.SmoothStep(alpha, 0, 0.16f));
			Camera.main.gameObject.transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f, Mathf.SmoothStep(dot_alpha, 1, 0.11f));
		}
		
		//Oxygen
		if (canmove){
			oxy_timer--;
			if (oxy_timer <= 0)
			{
				oxy_timer = Random.Range(220, 440);
				oxy -= Random.Range(3, 7);
				Instantiate(Resources.Load<GameObject>("pBubbles"),
					new Vector3(transform.position.x, transform.position.y - 0.3f, transform.position.z) +
					(new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z) * 0.25f),
					Resources.Load<GameObject>("pBubbles").transform.rotation);
			}

			if (Mathf.RoundToInt(oxy_display) != oxy)
			{
				oxy_display = Mathf.Lerp(oxy_display, oxy, 0.027f);
				if (Mathf.Abs(oxy_display - oxy) <= 1)
				{
					oxy_display = oxy;
				}

				Camera.main.gameObject.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject
					.GetComponent<OxyDisplayScript>()
					.shakeText(Mathf.Clamp(Mathf.RoundToInt(Mathf.Abs(oxy_display - oxy)) * 3, 8, 22));
			}

			Camera.main.gameObject.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject
					.GetComponent<Text>().text = "Oxy " + Mathf.Clamp(Mathf.RoundToInt(oxy_display), 0, 100)  + "%";

			if (oxy <= 0 && Mathf.RoundToInt(oxy_display) <= 0)
			{
				dead = true;
				canmove = false;
			}
		}

		//Player Movement
		Vector3 force = Vector3.zero;
		if (canmove) {
			//WASD Movement
			if (Input.GetKey(KeyCode.W)) {
				force += (Camera.main.transform.forward * spd);
			}
			else if (Input.GetKey(KeyCode.S)) {
				force += (Camera.main.transform.forward * -spd);
			}
			
			if (Input.GetKey(KeyCode.A)) {
				force += (Camera.main.transform.right * -spd);
			}
			else if (Input.GetKey(KeyCode.D)) {
				force += (Camera.main.transform.right * spd);
			}

			if (Input.GetKeyDown(KeyCode.K))
			{
				addHint("Nice to meet you!");
			}

			if (isGrounded())
			{
				if (Input.GetKeyDown(KeyCode.Space))
				{
					rb.AddForce(0, 1.5f, 0, ForceMode.Impulse);
				}
			}

			if (artifacts >= 5)
			{
				win = true;
				canmove = false;
			}
			
			//Click
			if (Input.GetMouseButtonDown(0)) {
				if (interactable != null)
				{
					interactable.GetComponent<InteractableBehavior>().interact();
				}
			}
		}
		else
		{
			if (dead)
			{
				Camera.main.gameObject.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.GetComponent<Text>().color = Color.red;
				if (!end)
				{
					end = true;
					GameObject black_obj = Instantiate(Resources.Load<GameObject>("EndTrans"));
					black_obj.transform.SetParent(Camera.main.gameObject.transform.GetChild(0));
					black_obj.GetComponent<EndTrans>().win = false;
					black_obj.transform.localPosition = new Vector2(0, 0);
				}
			}
			else if (win)
			{
				if (!end)
				{
					end = true;
					GameObject black_obj = Instantiate(Resources.Load<GameObject>("EndTrans"));
					black_obj.transform.SetParent(Camera.main.gameObject.transform.GetChild(0));
					black_obj.GetComponent<EndTrans>().win = true;
					black_obj.transform.localPosition = new Vector2(0, 0);
				}
			}
			else if (inspect) {
				if (Input.GetMouseButtonDown(0))
				{
					canmove = true;
				}
			}
		}
		force.y = 0;
		rb.AddForce(force);

		float friction_spd = 0.08f;
		Vector3 velocity = rb.velocity;
		velocity = new Vector3(Mathf.Lerp(velocity.x, 0f, friction_spd), velocity.y, Mathf.Lerp(velocity.z, 0f, friction_spd));
		rb.velocity = velocity;
	}

	public void addOxy()
	{
		oxy_timer = 600;
		oxy = Mathf.Clamp(oxy + 15, 0, 100);
	}

	public void addHint(string hint)
	{
		GameObject hint_obj = Instantiate(Resources.Load<GameObject>("pHintText"));
		hint_obj.transform.SetParent(Camera.main.gameObject.transform.GetChild(0));
		hint_obj.transform.localPosition = new Vector2(0, 0);
		hint_obj.transform.localScale = new Vector2(1, 1);
		hint_obj.GetComponent<HintTextScript>().changeText(hint);
	}

	public void addImage(int num)
	{
		artifacts++;
		inspect = true;
		canmove = false;
		
		GameObject black_obj = Instantiate(Resources.Load<GameObject>("BlackScreenInspect"));
		black_obj.transform.SetParent(Camera.main.gameObject.transform.GetChild(0));
		black_obj.transform.localPosition = new Vector2(0, 0);
		GameObject img_obj = Instantiate(Resources.Load<GameObject>("PieceText_" + num));
		img_obj.transform.SetParent(Camera.main.gameObject.transform.GetChild(0));
		img_obj.transform.localPosition = new Vector2(0, 0);
	}

	public bool can_move()
	{
		return canmove;
	}

	private bool isGrounded()
	{
		return Physics.Raycast(transform.position, -Vector3.up, GetComponent<Collider>().bounds.extents.y + 0.1f);
	}
}
