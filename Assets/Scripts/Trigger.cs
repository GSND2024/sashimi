using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject door;
    public GameObject spawn = null;

    public bool isPlayer = false;

    public bool isRock = false;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("rock"))
        {
            if (other.gameObject.CompareTag("Player"))
            {
                isPlayer = true;
            }
            else
            {
                isRock = true;
            }
            door.SetActive(false);
           if (spawn != null) {
            spawn.SetActive(true);
           }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
       // Check if the player exits the background area
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("rock"))
        {
            if (other.gameObject.CompareTag("Player"))
            {
                isPlayer = false;
            }
            else
            {
                isRock = false;
            }

            if((isPlayer == false) && (isRock == false))
            {
                door.SetActive(true);
                if (spawn != null) {
                    spawn.SetActive(false);
                }
            }
        }
    }
    
}
