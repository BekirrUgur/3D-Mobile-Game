using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kyleCam : MonoBehaviour
{
   
    GameObject pl;
    Vector3 distance;

    private void Start()
    {
        pl = GameObject.FindWithTag("kyle");
        distance = transform.position - pl.transform.position;
    }
    private void Update()
    {
        transform.position = pl.transform.position + distance;
    }

}
