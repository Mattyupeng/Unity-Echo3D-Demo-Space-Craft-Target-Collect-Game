using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healthbar1 : MonoBehaviour
{
    public GameObject[] hearts;
    public GameObject loseTextObject;
    public int life;
    private bool dead;

    private void Start()
    {
        life = hearts.Length;
        loseTextObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Point deduct");

        if (other.gameObject.CompareTag("BP")) 
        {
            //other.gameObject.SetActive(false);
			//life decrease
			life--;

			// Run the 'SetCountText()' function
			TakeDamage(life);
            if (life <= 0)
            {
                Destroy(hearts[life].gameObject);
                dead = true;
                loseTextObject.SetActive(true);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(dead == true)
        {
            //Set dead code
            Debug.Log("DEAD");
            //Display losetext
            loseTextObject.SetActive(true);
        }
    }

    public void TakeDamage(int d)
    {
        if(life >= 1)
        {
            //life -= d;
            Destroy(hearts[life].gameObject);
            // if(life==0)
            // {
            //     dead=true;
            //     return;
            // }
        }
    }
}
