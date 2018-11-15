using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blockManager : MonoBehaviour {

    public snakeMouvement SM;
    public float distanceSnakeBarrier;

    public GameObject BlockPrefab;

    public float minSpawnTime;
    public float maxSpawnTime;
    private float thisTime;
    private float randomTime;

    public int minSpawnDist;
    Vector2 previousSnakePos;
    public List<Vector3> SimpleBoxPositions = new Boo.Lang.List<Vector3>();


    // Use this for initialization
    void Start () {
        thisTime = 0;
        //spawnBarrier();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
