using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodBehavior : MonoBehaviour {

    [Header("Snake Manager")]

    SnakeMouvement SM;

    public int foodAmount;

	// Use this for initialization
	void Start () {


        SM = GameObject.FindGameObjectWithTag("SnakeManager").GetComponent<SnakeMouvement>();
        foodAmount = Random.Range(1, 10);

        transform.GetComponentInChildren<TextMesh>().text = ""+foodAmount;
        }

    // Update is called once per frame
    void Update () {
		
	}
}
