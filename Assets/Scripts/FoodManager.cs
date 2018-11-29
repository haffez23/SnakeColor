using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManager : MonoBehaviour {
	[Header("Snake Manager")]
     SnakeMovement SM;

	[Header("Food Variable")]
	public GameObject FoodPrefab;
	public int appearenceFrequency;

	[Header("Time to spawn Management")]
	public float timeBetweenFoodSpawn;
    float thisTime;

	void Start () {
		SM = GameObject.FindGameObjectWithTag ("SnakeManager").GetComponent<SnakeMovement> ();
		SpawnFood ();
	}
	
	void Update () {
		if (GameController.gameState == GameController.GameState.GAME) {
			if (thisTime < timeBetweenFoodSpawn) {
				thisTime += Time.deltaTime;
			} else {
				SpawnFood ();
				thisTime = 0;
			}
		}
	}


	public SnakeMovement getSM(){
		return SM;
	}

	public void SpawnFood(){
		float screenWidthWorldPos = Camera.main.orthographicSize * Screen.width / Screen.height;
		float distBetweenBlocks = screenWidthWorldPos / 5;

		for (int i = -2; i < 3; i++) {
			float x = 2 * i * distBetweenBlocks;
			float y = 0;

			if (SM.transform.childCount > 0) {
				y = (int)SM.transform.GetChild (0).position.y + distBetweenBlocks * 2 + 10;
			}

			Vector3 SpawnPos = new Vector3 (x, y, 0);
			int number;

			if (appearenceFrequency < 100) {
				number = Random.Range (0, 100 - appearenceFrequency);
			} else {
				number = 1;
			}

			GameObject boxInstance;
			if (number == 1) {
				boxInstance = Instantiate (FoodPrefab,SpawnPos,Quaternion.identity,transform);

			}
				
		}
	}
}
