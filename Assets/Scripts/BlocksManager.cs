using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlocksManager : MonoBehaviour {

	[Header("SnakeManager")]
	public SnakeMovement SM;
	public float distanceSnakeBarrier;

	[Header("Block Prefab")]
	public GameObject BlockPrefab;

	[Header("Time to SpawnDelegate Management")]
	public float minSpawnTime;
	public float maxSpawnTime;
	private float thisTime;
	private float randomTime;

	[Header("Snake Value for spawning")]
	public int minSpawnDist;
	Vector2 previousSnakePos;
	public List<Vector3> SimpleBoxPosition = new List<Vector3> ();

	void Start(){
		thisTime = 0;
		spawnBarrier ();
		randomTime = Random.Range (minSpawnTime, maxSpawnTime);
	}

	void Update(){
		if (GameController.gameState == GameController.GameState.GAME) {
			if (thisTime < randomTime) {
				thisTime += Time.deltaTime;
			} else {
				SpawnBlocks ();
				thisTime = 0;
				randomTime = Random.Range (minSpawnTime, maxSpawnTime);
			}
			if (SM.transform.childCount > 0) {
				if (SM.transform.GetChild (0).position.y - previousSnakePos.y > minSpawnDist) {
					spawnBarrier ();
				}
			}
		}
	}

	public void spawnBarrier(){
		float screenWidthWorldPos = Camera.main.orthographicSize * Screen.width / Screen.height;
		float distBetweenBlocks = screenWidthWorldPos / 5;

		for (int i = -2; i < 3; i++) {
			float x = 2 * i * distBetweenBlocks;
			float y = 0;

			if (SM.transform.childCount > 0) {
				y = (int)SM.transform.GetChild (0).position.y + distBetweenBlocks * 2 + distanceSnakeBarrier;
				if (Screen.height / Screen.width == 4 / 3) {
					y *= 4 / 3f;
				}
			}

			Vector3 SpawnPos = new Vector3 (x, y, 0);
			GameObject boxInstance = Instantiate (BlockPrefab,SpawnPos,Quaternion.identity,transform);

			if (SM.transform.childCount > 0) {
				previousSnakePos = SM.transform.GetChild (0).position;
			}
		}
	}


	public void SpawnBlocks(){
		float screenWidthWorldPos = Camera.main.orthographicSize * Screen.width / Screen.height;
		float distBetweenBlocks = screenWidthWorldPos / 5;
		int random;
		random = Random.Range (-2, 3);
		float x = 2 * random * distBetweenBlocks;
		float y = 0;

		if (SM.transform.childCount > 0) {
			y = (int)SM.transform.GetChild (0).position.y + distBetweenBlocks * 2 + distanceSnakeBarrier;
			if (Screen.height / Screen.width == 4 / 3) {
				y *= 2;
			}
		}

		Vector3 SpawnPos = new Vector3 (x, y, 0);
		bool canSpawnBlock = true;
		if (SimpleBoxPosition.Count == 0) {
			SimpleBoxPosition.Add (SpawnPos);
		} else {
			for(int k= 0;k<SimpleBoxPosition.Count;k++){
				if (SpawnPos == SimpleBoxPosition [k]) {
					canSpawnBlock = false;
				}
			}
		}
		GameObject boxInstance;
		if (canSpawnBlock) {
			SimpleBoxPosition.Add (SpawnPos);
			boxInstance = Instantiate (BlockPrefab, SpawnPos, Quaternion.identity, transform);
			boxInstance.name = "SimpleBox";
			boxInstance.tag = "SimpleBox";
			boxInstance.layer = LayerMask.NameToLayer ("Default");
			boxInstance.AddComponent<Rigidbody2D>();
			boxInstance.GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezeAll;

		}
	}

	public void SetPreviousPosAfterGameOver(){
		Invoke ("PreviousPosInvoke", 0.5f);
	}

	public void PreviousPosInvoke(){
		previousSnakePos.y = SM.transform.GetChild (0).position.y;
	}

}
