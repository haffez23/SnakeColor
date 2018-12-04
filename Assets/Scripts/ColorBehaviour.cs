using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ColorBehaviour : MonoBehaviour {



    public BlocksManager blocksManager;
    private List<int> alpha = new List<int> { 0, 128, 255 };


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void SetBoxColors(){

        if(blocksManager.tag.Equals("Box"))
        {
            string currentScene = SceneManager.GetActiveScene().name;

            if (currentScene.Equals("LEVEL3"))
            {
                Color32 thisImageColor = blocksManager.GetComponent<SpriteRenderer>().color;


                for (int i = 0; i < alpha.Count; i++)
                {
                    int temp = alpha[i];
                    int randomIndex = Random.Range(i, alpha.Count);
                    alpha[i] = alpha[randomIndex];
                    alpha[randomIndex] = temp;
                    thisImageColor = new Color32(255, (byte)alpha[i], 0, 255);

                }
                blocksManager.GetComponent<SpriteRenderer>().color = thisImageColor;

            }

        }

    }
}
