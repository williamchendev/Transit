using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{

	public bool trans_in;
	public string scene_change;
	private float lerp_val;
	private Image img;
	
	// Use this for initialization
	void Start ()
	{
		GameObject canvas = GameObject.FindWithTag("EventManager");
		GetComponent<RectTransform>().SetParent(canvas.transform);
		img = GetComponent<Image>();
		if (trans_in)
		{
			lerp_val = 0;
		}
		else
		{
			lerp_val = 1;
		}
		Update();
	}
	
	// Update is called once per frame
	void Update () {
		if (trans_in)
		{
			lerp_val = Mathf.Lerp(lerp_val, 1, Time.deltaTime * 0.5f);
			if (lerp_val > 0.95f)
			{
				SceneManager.LoadScene(scene_change, LoadSceneMode.Single);
			}
		}
		else
		{
			lerp_val = Mathf.Lerp(lerp_val, 0, Time.deltaTime * 0.5f);
			if (lerp_val < 0.05f)
			{
				Destroy(gameObject);
			}
		}
		img.color = new Color(0f, 0f, 0f, Mathf.Clamp(lerp_val, 0, 1));
	}
}
