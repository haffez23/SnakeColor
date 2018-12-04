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
        if (SnakeContainer.childCount > 0)
        {
            transform.position = Vector3.Slerp(transform.position, new Vector3(transform.position.x, SnakeContainer.GetChild(0).position.y, 0) + new Vector3(0, 0, -1), 0.1f);
        }
    }
}
