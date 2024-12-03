using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject rock;

    public float frequency;

    float lastSpawnedTime;

    void Update()
    {
        if(Time.time > lastSpawnedTime + frequency)
        {
            Spawn();
            lastSpawnedTime = Time.time;
        }
    }


    public void Spawn()
    {
        GameObject newSpawnedObject = Instantiate(rock, transform.position, Quaternion.identity);
        newSpawnedObject.transform.parent = transform;
    }
}
