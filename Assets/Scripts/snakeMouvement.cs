using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snakeMouvement : MonoBehaviour {

    public GameObject gc;

    public List<Transform> bodyParts = new List<Transform>();
    public float minDistance = 0.25f;
    public int initialAmount;
    public float speed = 1;
    public float rotationSpeed = 50;
    public float lerpTimeX;
    public float lerpTimeY;

    //Snake Head Prefab
    public GameObject bodyPrefab;
    public TextMesh partAmountTextMesh;
    //Private Variables
    private float distance;
    private Vector3 refVelocity;
    private Transform curBodyPart;
    private Transform prevBodyPart;
    private bool firstPart;

    //Mouse Controll

    Vector2 mousePreviousPosition;
    Vector2 mouseCurrentPosition;

    public ParticleSystem snakeParticule;

    // Use this for initialization
    void Start () {

        firstPart = true;
        for (int i=0; i < initialAmount; i++)
        {
            Invoke("addBodyPart", 0.1f);

        }
	}

    public void SpawnBodyPart()
    {
        firstPart = true;
        for (int i = 0; i < initialAmount; i++)
        {
            Invoke("addBodyPart", 0.1f);

        }
    }
	
	// Update is called once per frame
	void Update () {
        /**
          if (GameController.gameState == GameController.GameState.GAME)
        {
            
            if (bodyParts.Count == 0)
            {
                gc.SetGameOver();

            }
        }
        if (partAmountTextMesh != null)
        {
            partAmountTextMesh.text = transform.childCount + "";
        }
        **/
        Move();
        if (partAmountTextMesh != null)
        {
            partAmountTextMesh.text = transform.childCount + "";
        }

    }
    public void Move()
    {
        float curSpeed = speed;
        if (bodyParts.Count > 0)
        {
            bodyParts[0].Translate(Vector2.up * curSpeed * Time.smoothDeltaTime);
            float maxX = Camera.main.orthographicSize * Screen.width / Screen.height;
            if  (bodyParts.Count > 0)
            {
                //A VERIIIIFier
                if (bodyParts[0].position.x > maxX)
                {

                    bodyParts[0].position = new Vector3(maxX - 0.10f, bodyParts[0].position.y, bodyParts[0].position.z);
                        
                }
                else if (bodyParts[0].position.x > -maxX)
                {

                    bodyParts[0].position = new Vector3(-maxX + 0.10f, bodyParts[0].position.y, bodyParts[0].position.z);

                }
            }

            if (Input.GetMouseButtonDown(0))
            {
                mousePreviousPosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            }
            else if (Input.GetMouseButtonDown(0))
            {
                if (bodyParts.Count > 0 && Mathf.Abs(bodyParts[0].position.x) < maxX) { 
                    mouseCurrentPosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
                float deltaMousePos = Mathf.Abs(mousePreviousPosition.x - mouseCurrentPosition.x);
                float sign = Mathf.Sign(mousePreviousPosition.x - mouseCurrentPosition.x);
                bodyParts[0].GetComponent<Rigidbody2D>().AddForce(Vector2.right * rotationSpeed * deltaMousePos * -sign);
                mousePreviousPosition = mouseCurrentPosition;

                } else if (bodyParts.Count > 0 && bodyParts[0].position.x > maxX)
                {
                    bodyParts[0].position = new Vector3(maxX - 0.01f, bodyParts[0].position.y, bodyParts[0].position.z);
                }else if (bodyParts.Count > 0 && bodyParts[0].position.x <maxX){

                    bodyParts[0].position = new Vector3(-maxX + 0.01f, bodyParts[0].position.y, bodyParts[0].position.z);

                }
            }


            for(int i = 0; i < bodyParts.Count; i++)
            {
                curBodyPart = bodyParts[i];
                prevBodyPart = bodyParts[i - 1];
                distance = Vector3.Distance(prevBodyPart.position, curBodyPart.position);
                Vector3 newPos = prevBodyPart.position;
                newPos.z = bodyParts[0].position.z;
                Vector3 pos = curBodyPart.position;
                pos.x = Mathf.Lerp(pos.x, newPos.x, lerpTimeX);
                pos.y = Mathf.Lerp(pos.y, newPos.y, lerpTimeY);
                curBodyPart.position = pos;

            }
            
        }
        
        

       

    }

    public void AddBodyPart()
    {
        Transform newPart;
        if (firstPart)
        {
            newPart = (Instantiate(bodyPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject).transform;
            partAmountTextMesh.transform.position = newPart.position + new Vector3(0, 0.5f, 0);
            firstPart = false;

        }
        else
            newPart = (Instantiate(bodyPrefab, 
                bodyParts[bodyParts.Count - 1].position, 
                bodyParts[bodyParts.Count - 1].rotation) 
                as GameObject).transform;
        newPart.SetParent(transform);
        bodyParts.Add(newPart);
    }
}
