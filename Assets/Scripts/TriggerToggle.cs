using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerToggle : MonoBehaviour
{
    public GameObject door;
    public GameObject door2 = null;
    public GameObject on;
    public GameObject off;
    public GameObject spawn = null;
    
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("rock"))
        {
            door.SetActive(!door.activeSelf);
            if (door2 != null) {
                door2.SetActive(!door2.activeSelf);
            }
            on.SetActive(!on.activeSelf);
            off.SetActive(!off.activeSelf);
            if (spawn != null) {
                spawn.SetActive(!spawn.activeSelf);
            }
        }
    }
}
