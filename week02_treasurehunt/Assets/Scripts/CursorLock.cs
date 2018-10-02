using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorLock : MonoBehaviour
{
	public bool start_locked;
	private bool cursor_locked;
	
	// Use this for initialization
	void Start () {
		if (start_locked)
		{
			lockCursor();
		}
		else
		{
			unlockCursor();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (cursor_locked)
		{
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				unlockCursor();
			}
		}
		else
		{
			if (Input.GetMouseButtonDown(0))
			{
				lockCursor();
			}
		}
	}
	
	private void lockCursor()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		cursor_locked = true;
	}
	
	private void unlockCursor()
	{
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
		cursor_locked = false;
	}
}
