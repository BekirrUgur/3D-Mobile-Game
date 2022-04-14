using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kyleCam : MonoBehaviour
{
    // Start is called before the first frame update
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
