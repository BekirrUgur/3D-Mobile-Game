using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crash_cam : MonoBehaviour
{

    GameObject pl;
    Vector3 distance;

    private void Start()
    {
        pl = GameObject.FindWithTag("Player");
        //Sets the distance between the player and the camera.
        distance = transform.position - pl.transform.position;
    }
    private void Update()
    {
        //By constantly equalizing the distance between the two objects in the scene, the position of the two objects remains fixed between them and the camera follows the actor.
        transform.position = pl.transform.position + distance;
    }
}
