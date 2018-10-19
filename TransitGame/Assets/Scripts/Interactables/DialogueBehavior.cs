using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueBehavior : MonoBehaviour {

	//Components
	private EventManager em;
	private DialogueTrigger dt;
	
	//Settings
	[SerializeField] private int event_data_num = 0;
	[SerializeField] private float radius = 5f;
	[SerializeField] private bool non_destroy = false;
	
	// Use this for initialization
	void Start ()
	{
		em = GameObject.FindGameObjectWithTag("EventManager").GetComponent<EventManager>();
		dt = gameObject.AddComponent<DialogueTrigger>();
	}
	
	// Update is called once per frame
	void Update () {
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		if (Vector2.Distance(new Vector2(player.transform.position.x, player.transform.position.z), new Vector2(transform.position.x, transform.position.z)) <= radius)
		{
			if (!em.bubble_exists)
			{
				createTextBubble();
			}
		}
	}

	private void createTextBubble()
	{
		em.createTextBubble(new Vector2(transform.position.x, transform.position.z), radius, TextData.getData(event_data_num), dt);
	}

	public bool can_destroy
	{
		get
		{
			return !non_destroy;
		}
	}
 
	void OnDrawGizmos()
	{
		Gizmos.color = Color.white;
		float starting_angle = 0f;
		float ending_angle = 0f;
		for (float i = 5; i <= 360; i += 5)
		{
			ending_angle = i;
			Vector3 pos1 = transform.position + (new Vector3(Mathf.Cos(starting_angle * Mathf.Deg2Rad) * radius, 0, Mathf.Sin(starting_angle * Mathf.Deg2Rad) * radius));
			Vector3 pos2 = transform.position + (new Vector3(Mathf.Cos(ending_angle * Mathf.Deg2Rad) * radius, 0, Mathf.Sin(ending_angle * Mathf.Deg2Rad) * radius));
			Gizmos.DrawLine(pos1, pos2);
			starting_angle = ending_angle;
		}
	}
	
}

public class DialogueTrigger : MonoBehaviour {

	public void triggerDialogue()
	{
		if (GetComponent<DialogueBehavior>().can_destroy)
		{
			Destroy(gameObject);
		}
	}
	
}
