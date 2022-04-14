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
        //Oyuncu ve kamera arasýndaki mesafeyi belirler.
        distance = transform.position - pl.transform.position;
    }
    private void Update()
    {
        //Sahne içerisinde sürekli olarak aradaki mesafeyi biribirine eþitleyerek iki nesnenin posizyonun aralarýnda sabit kalmasýný saðlar ve kamera oyuncuyu takip eder.
        transform.position = pl.transform.position + distance;
    }
}
