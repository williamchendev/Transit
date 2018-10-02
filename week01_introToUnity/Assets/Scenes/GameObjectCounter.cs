using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectCounter : MonoBehaviour {

	// Use this for initialization
	void Start () {
        MeshRenderer[] gameObjects = GameObject.FindObjectsOfType<MeshRenderer>();
        Debug.Log(gameObjects.Length);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}