using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class Destroyer : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)

    {
        if (other.gameObject.CompareTag("Player"))
        {
           SceneManager.LoadScene("Chapter2");
        }

        if (other.gameObject.CompareTag("rock"))
        {
            Destroy(other.gameObject);
        }

    }
}
