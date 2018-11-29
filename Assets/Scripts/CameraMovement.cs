using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

	public Transform SnakeContainer;
	//Vector3 initialCameraPos;

	void Start () {
		//initialCameraPos = transform.position;
	}
	
	void LateUpdate () {
		if (SnakeContainer.childCount > 1) {
			transform.position = Vector3.Slerp (transform.position,SnakeContainer.GetChild(1).position + new Vector3(0,2f,-10),0.1f);
		}
	}
}
