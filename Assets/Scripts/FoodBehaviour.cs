using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodBehaviour : MonoBehaviour {
	[Header("SnakeManager")]
    SnakeMovement SM;
	public int foodAmount;

	void Start () {
		foodAmount = Random.Range (0,10);
		transform.GetComponentInChildren <TextMesh> ().text = "" + foodAmount;
	}
	
	void Update () {
		SM = transform.parent.GetComponent <FoodManager> ().getSM();
		if (SM.transform.childCount > 0 && transform.position.y - SM.transform.GetChild (0).position.y < -10) {
			Destroy (this.gameObject);
		}
	}
	private void OnTriggerEnter2D(Collider2D collider){
		Destroy (this.gameObject);
	}
}
