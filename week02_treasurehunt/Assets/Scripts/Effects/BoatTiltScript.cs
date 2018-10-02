using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatTiltScript : MonoBehaviour {

	//Settings
	private int[] timer;
	private float[] spd;
	private float[] sin_val;

	//Init
	void Start () {
		timer = new int[3];
		spd = new float[3];
		sin_val = new float[3];
		
		for (int i = 0; i < 3; i++) {
			timer[i] = Random.Range(20, 30);
			spd[i] = Random.Range(0.3f, 0.7f);
			sin_val[i] = Random.Range(0f, 1f);
		}
	}
	
	//Update
	void Update () {
		float[] draw_val = new float[3];
		for (int i = 0; i < 3; i++) {
			timer[i]--;
			if (timer[i] < 0) {
				timer[i] = Random.Range(360, 640);
				spd[i] = Random.Range(0.8f, 1f);
			}

			sin_val[i] += 0.0037f;
			if (sin_val[i] > 1) {
				sin_val[i] = 0;
			}

			draw_val[i] = Mathf.Sin(sin_val[i] * 2 * Mathf.PI);
		}
		
		transform.position = new Vector3(transform.position.x, transform.position.y + (draw_val[0] * (spd[0] * 0.025f)), transform.position.z);
		transform.eulerAngles = new Vector3((draw_val[1] * (spd[1] * 6f)), 0, (draw_val[2] * (spd[2] * 7f)));
	}
}
