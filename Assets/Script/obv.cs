using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Playercontrol;

public class obv : MonoBehaviour
{
    public Transform respawnPoint; // RespawnPoint for the player object if it goes out the out-of-bound volume
    public GameObject returnTextObject;// Return text object to indicate player you have respawned.
    private Playercontrol PlayercontrolInstance; //Instance from playercontrol so that I can update the count value.

    // Start is called before the first frame update
    void Start()
    {
        returnTextObject.SetActive(false);//keep text object off first
        GameObject player = GameObject.FindGameObjectWithTag("Player");//find player
        PlayercontrolInstance = player.GetComponent<Playercontrol>();//get components from player
    }

    //keep player inside the volume.
    private void OnTriggerStay(Collider other)
    {
            //if player hit out of bound volume
            if (other.CompareTag("Player"))
            {
                // Get the player's position
                Vector3 playerPos = other.transform.position;

                // Check if the player is outside the out-of-bounds volume
                if (!gameObject.GetComponent<Collider>().bounds.Contains(playerPos))
                {
                    // Respawn the player at the specified respawn point
                    other.transform.position = respawnPoint.position;
                    // -50 points panelty
                    PlayercontrolInstance.count = PlayercontrolInstance.count - 50;
                    PlayercontrolInstance.SetCountText ();
                    //turn on returntext to alert player
                    returnTextObject.SetActive(true);
                }
                else
                {
                    //turn off returntext
                    returnTextObject.SetActive(false);
                }

            }

    }

    
}



