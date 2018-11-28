using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxBehavior : MonoBehaviour {


    SnakeMouvement SM;

    

	// Use this for initialization
	void Start () {


        SM = transform.GetComponentInParent<SnakeMouvement>();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    private void OnCollisisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Box"  && transform == SM.bodyParts[0])
        {
            if(SM.bodyParts.Count > 1 &&  SM.bodyParts[0] != null)
            {
                SM.partAmountTextMesh.transform.parent = SM.bodyParts[1];
                SM.partAmountTextMesh.transform.position = SM.bodyParts[1].position + new Vector3(0,0.5f,0);
            }else if(SM.bodyParts.Count == 1)
            {
                SM.partAmountTextMesh.transform.parent = null;
            }

            SM.snakeParticule.Stop();
            SM.snakeParticule.transform.position = collision.contacts[0].point;
            SM.snakeParticule.Play();


            Destroy(this.gameObject);
            GameController.SCORE++;

            collision.transform.GetComponent<AutoDestroy>().life -=1;
            collision.transform.GetComponent<AutoDestroy>().updateText();

            collision.transform.GetComponent<AutoDestroy>().SetBoxColor();

            SM.bodyParts.Remove(SM.bodyParts[0]);
        }
        else if(collision.transform.tag == "SimpleBox" && transform == SM.bodyParts[0])
        {

            SM.snakeParticule.Stop();
            SM.snakeParticule.transform.position = collision.contacts[0].point;
            SM.snakeParticule.Play();


            if(SM.bodyParts.Count > 1 && SM.bodyParts[1] != null)
            {
                SM.partAmountTextMesh.transform.parent = SM.bodyParts[1];
                SM.partAmountTextMesh.transform.position = SM.bodyParts[1].position + new Vector3(0, 0.5f, 0);
            }
            else if (SM.bodyParts.Count == 1)
            {
                SM.partAmountTextMesh.transform.parent = null;
            }


            Destroy(this.gameObject);
            GameController.SCORE++;

            collision.transform.GetComponent<AutoDestroy>().life -= 1;
            collision.transform.GetComponent<AutoDestroy>().updateText();

            collision.transform.GetComponent<AutoDestroy>().SetBoxColor();

            SM.bodyParts.Remove(SM.bodyParts[0]);

        }
        else if(collision.transform.tag == "SimpleBox" && transform != SM.bodyParts[0])
        {
            Physics2D.IgnoreCollision(transform.GetComponent<Collider2D>(),collision.transform.GetComponent<Collider2D>());
            collision.transform.GetComponent<AutoDestroy>().dontMove = true ;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(SM.bodyParts.Count> 0)
        {
            if(collision.transform.tag=="Food" && transform== SM.bodyParts[0])
            {
                for (int i = 0; i < collision.transform.GetComponent<FoodBehavior>().foodAmount; i++)
                {
                    SM.AddBodyPart();
                }
                Destroy(collision.transform.gameObject);
             }
        }
    }
}
