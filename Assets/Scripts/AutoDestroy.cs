using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour {

    SnakeMouvement SM;
    public int life;
    public float lifeForColor;
    TextMesh thisTextMesh;

    GameObject[] toDestroy;
    GameObject[] toUnparent;

    int MaxLifeForRed = 50;
    Vector3 initialPos;
    public bool dontMove;

    void setBoxSize()
    {
        float x;
        float y;
        transform.localScale *= ((float) Screen.width/ (float)Screen.height/(9f/16f));
    }


    // Use this for initialization
    void Start () {
        setBoxSize();
        SM = GameObject.FindGameObjectWithTag("SnakeManager").GetComponent<SnakeMouvement>();
        life = Random.Range(1, GameController.SCORE/2 + 5);
        if (transform.tag == "SimpleBox")
        {
            life = Random.Range(5, 50);

        }
        lifeForColor = life;
        thisTextMesh.text = "" + life;
        toDestroy = new GameObject[transform.childCount];
        toUnparent = new GameObject[transform.childCount];
        StartCoroutine("EnableSomeBars");
        setBoxSize();
        initialPos = transform.position;
        dontMove = false;

    }
	
	// Update is called once per frame
	void Update () {

        if (dontMove == true)
        {
            transform.position = initialPos;

        }
        if (SM.transform.childCount > 0 && transform.position.y - SM.transform.GetChild(0).position.y < -10)
        { Destroy(this.gameObject); }
        lifeForColor = life;
        if (life <= 0)
        {
            transform.GetComponent<SpriteRenderer>().enabled = false;
            transform.GetComponentInChildren<MeshRenderer>().enabled = false;
            transform.GetComponent<BoxCollider2D>().enabled = false;
           if (transform.GetComponentInChildren<ParticleSystem>().isStopped)
            {
                transform.GetComponentInChildren<ParticleSystem>().Play();
            }
            Destroy(this.gameObject,0.7f);


        }
	}


    public void updateText()
    {
        thisTextMesh.text = "" + life;

    }
    IEnumerator EnableSomeBars()
    {
        int i = 0;
        while (i < transform.childCount)
        {
            if (transform.GetChild(i).tag == "Bar")
            {
                int r = Random.Range(0, 6);
                if (r == 1)
                {
                    toUnparent[i] = transform.GetChild(i).gameObject;

                }
                else
                    toDestroy[i] = transform.GetChild(i).gameObject;
                i++;
                yield return new WaitForSeconds(0.01f);

            }
            else
                i++;

        }
        for(int k=0;k<toUnparent.Length;k++)
        {
            if (toUnparent[k] != null)
                toUnparent[k].transform.parent = null;
            if(toDestroy[k]!= null)
            {
                Destroy(toDestroy[k]);
            }
        }
        yield return null;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag=="SimpleBox" && transform.tag == "Box")
        {
            Destroy(collision.transform.gameObject);
        }
        else if (transform.tag=="SimpleBox" && collision.transform.tag == "SimpleBox")
        {
            Destroy(collision.transform.gameObject);

        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.transform.tag=="SimpleBox" && transform.tag == "Box")
        {
            Destroy(collision.transform.gameObject);
        }
        else if (transform.tag == "SimpleBox" && collision.transform.tag == "SimpleBox")
        {
            Destroy(collision.transform.gameObject);

        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        
  
        if (collision.transform.tag == "SimpleBox" && transform.tag == "Box")
        {
            Destroy(collision.transform.gameObject);
        }
        else if (transform.tag == "SimpleBox" && collision.transform.tag == "SimpleBox")
        {
            Debug.Log("OverLapping"); ;

        }


    }
    public void SetBoxColor()
    {
        Color32 thisImageColor = GetComponent<SpriteRenderer>().color;
        if (lifeForColor > MaxLifeForRed)
            thisImageColor = new Color32(255, 0, 0, 255);
        else if (lifeForColor >= MaxLifeForRed/2 && lifeForColor<= MaxLifeForRed)
            thisImageColor = new Color32(255, 0, (byte)(510*(1-(lifeForColor/MaxLifeForRed))), 255);
        else if (lifeForColor >= 0 && lifeForColor < MaxLifeForRed)
            thisImageColor = new Color32((byte)(510 * lifeForColor / MaxLifeForRed), 255, 0, 255);
        GetComponent<SpriteRenderer>().color = thisImageColor;


    }

}
