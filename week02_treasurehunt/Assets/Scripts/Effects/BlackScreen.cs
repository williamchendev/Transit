using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackScreen : MonoBehaviour
{

	private Image img;
	// Use this for initialization
	void Start ()
	{
		img = GetComponent<Image>();
		img.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
		img.color = new Color(1, 1, 1, Mathf.Lerp(img.color.a, 0, Time.deltaTime * 0.3f));
		if (img.color.a <= 0.05f)
		{
			Destroy(gameObject);
		}
	}
}
