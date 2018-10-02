using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

	private int cam_num;
	public GameObject camA;
	public GameObject camB;
	public GameObject camC;
	public GameObject camMain;
	public PersonBehavior person;
	public GameObject end_text;

	private bool atPos;
	private int timer;
	private GameObject[] firepart;
	
	// Use this for initialization
	void Start ()
	{
		cam_num = 0;
		atPos = false;
		timer = 180;
		firepart = GameObject.FindGameObjectsWithTag("smog");
		foreach (GameObject part in firepart)
		{
			part.SetActive(false);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (cam_num == 1)
		{
			camA.transform.position = Vector3.Lerp(camA.transform.position, new Vector3(camA.transform.position.x, 3f, camA.transform.position.z), Time.deltaTime * 0.12f);
		}
		else if (cam_num == 2)
		{
			camB.transform.eulerAngles = new Vector3(camB.transform.eulerAngles.x, camB.transform.eulerAngles.y + 0.25f, camB.transform.eulerAngles.z);
		}
		else if (cam_num == 3)
		{
			float lerp_pos = Mathf.Lerp(camC.transform.eulerAngles.x, 0, Time.deltaTime * 0.8f);
			camC.transform.eulerAngles = new Vector3(lerp_pos, camC.transform.eulerAngles.y, camC.transform.eulerAngles.z);
		}
		else if (cam_num == 4)
		{
			if (!atPos)
			{
				float time_use = Time.deltaTime * 2f;
				camC.transform.eulerAngles = new Vector3(0f, Mathf.Lerp(camC.transform.eulerAngles.y, 0, time_use), 0f);
				camC.transform.position = Vector3.Lerp(camC.transform.position, camMain.transform.position, time_use);
				timer--;
				if (timer <= 0)
				{
					atPos = true;
					foreach (GameObject part in firepart)
					{
						part.SetActive(true);
					}

					person.big_oof = true;
					end_text.SetActive(true);
				}
			}
			else
			{
				camC.transform.eulerAngles = new Vector3(0f, 0f, Random.Range(-5f, 5f));
				camC.transform.position = camMain.transform.position;
				float offset = 0.015f;
				camC.transform.position += new Vector3(Random.Range(-offset, offset), Random.Range(-offset, offset), 0f);
			}
		}
	}

	public void changeCamA()
	{
		if (cam_num == 0)
		{
			setIn();
			camA.gameObject.SetActive(true);
			cam_num++;
		}
	}
	
	public void changeCamB()
	{
		if (cam_num == 1)
		{
			setIn();
			camB.gameObject.SetActive(true);
			cam_num++;
		}
	}
	
	public void changeCamC()
	{
		if (cam_num == 2)
		{
			setIn();
			camC.gameObject.SetActive(true);
			cam_num++;
		}
	}
	
	public void changeCamMain()
	{
		if (cam_num == 3)
		{
			cam_num++;
		}
	}

	private void setIn()
	{
		camA.gameObject.SetActive(false);
		camB.gameObject.SetActive(false);
		camC.gameObject.SetActive(false);
		camMain.gameObject.SetActive(false);
	}
}
