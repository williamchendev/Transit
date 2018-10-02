using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackScreenInspect : MonoBehaviour {

	
	private Image img;
	
	// Use this for initialization
	void Start () {
		img = GetComponent<Image>();
		img.color = new Color(1, 1, 1, 0);
	}
	
	// Update is called once per frame
	void Update () {
		if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehavior>().can_move())
		{
			img.color = new Color(1, 1, 1, Mathf.Lerp(img.color.a, 0, Time.deltaTime * 0.6f));
			if (img.color.a <= 0.05f)
			{
				Destroy(gameObject);
			}
		}
		else
		{
			img.color = new Color(1, 1, 1, Mathf.Lerp(img.color.a, 0.6f, Time.deltaTime * 1f));
		}
	}
}
