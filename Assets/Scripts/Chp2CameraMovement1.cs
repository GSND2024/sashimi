using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Chp2CameraMovement1 : MonoBehaviour
{
 public Camera cam;
 public GameObject player;
 public float cameraHeight;
 public float cameraWidth;
 public int roomCounter;

void Start()
{
    roomCounter = PlayerPrefs.GetInt("roomCounter2");
    cameraHeight = 2f * cam.orthographicSize; // Height in world units
    cameraWidth = cameraHeight * cam.aspect;  // Width in world units (height × aspect ratio)
    //Debug.Log(cameraWidth);
    //StartCoroutine(moveToX(cam.transform, 1.0f));
}

void Update()
{
    if (player.transform.position.x > (cam.transform.position.x + cameraWidth/2))
    {
        StartCoroutine(moveTo(cam.transform, 1.0f));
    }

    //if (player.transform.position.x < (cam.transform.position.x - cameraWidth/2))
    //{
     //   StartCoroutine(moveBack(cam.transform, 1.0f));
    //}
}


bool isMoving = false;

IEnumerator moveTo(Transform fromPosition, float duration)
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
    toPosition.x += cameraWidth;
   


    while (counter < duration)
    {
        counter += Time.deltaTime;
        fromPosition.position = Vector3.Lerp(startPos, toPosition, counter / duration);
        yield return null;
    }
    roomCounter++;
    PlayerPrefs.SetInt("roomCounter2", roomCounter);
    isMoving = false;
}

IEnumerator moveBack(Transform fromPosition, float duration)
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
    toPosition.x -= cameraWidth;
    if (roomCounter == 2)
    {
        toPosition.y -= 3.5f;
    }
   


    while (counter < duration)
    {
        counter += Time.deltaTime;
        fromPosition.position = Vector3.Lerp(startPos, toPosition, counter / duration);
        yield return null;
    }
    roomCounter--;
    isMoving = false;
}

}

