using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
 public Camera cam;
 public GameObject player;
 public int roomCounter = 1;

void Start()
{
    //StartCoroutine(moveToX(cam.transform, 1.0f));
}

void Update()
{
    if (player.transform.position.x > (cam.transform.position.x + cam.orthographicSize * cam.aspect))
    {
        StartCoroutine(moveToX(cam.transform, 1.0f));
    }
}


bool isMoving = false;

IEnumerator moveToX(Transform fromPosition, float duration)
{
    //Make sure there is only one instance of this function running
    if (isMoving)
    {
        yield break; ///exit if this is still running
    }
    isMoving = true;

    float counter = 0;

    //Get the current position of the object to be moved
    Vector3 startPos = fromPosition.position;
    Vector3 toPosition = fromPosition.position;
    //dynamic position
    //toPosition.x += 2f * cam.orthographicSize * cam.aspect;
    //hard code for now
    toPosition.x += 20;
    if (roomCounter == 2) {
        toPosition.y += 3.5f;
    }

    while (counter < duration)
    {
        counter += Time.deltaTime;
        fromPosition.position = Vector3.Lerp(startPos, toPosition, counter / duration);
        yield return null;
    }
    roomCounter++;
    isMoving = false;
}
}
