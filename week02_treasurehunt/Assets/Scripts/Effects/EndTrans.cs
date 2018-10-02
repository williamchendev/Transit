using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndTrans : MonoBehaviour {

	private Image img;
	public bool win;
	
	// Use this for initialization
	void Start () {
		img = GetComponent<Image>();
		img.color = new Color(1, 1, 1, 0);
	}
	
	// Update is called once per frame
	void Update () {
		img.color = new Color(1, 1, 1, Mathf.Lerp(img.color.a, 1, Time.deltaTime * 1f));
		if (img.color.a >= 0.99f)
		{
			if (win)
			{
				SceneManager.LoadScene("End", LoadSceneMode.Single);
			}
			else
			{
				SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
			}
		}
	}
	
}
