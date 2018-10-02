using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BlackTrans : MonoBehaviour {

	private Image img;
	
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
			SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
		}
	}
}
