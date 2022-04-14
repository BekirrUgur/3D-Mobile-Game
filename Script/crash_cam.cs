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
        //Oyuncu ve kamera aras�ndaki mesafeyi belirler.
        distance = transform.position - pl.transform.position;
    }
    private void Update()
    {
        //Sahne i�erisinde s�rekli olarak aradaki mesafeyi biribirine e�itleyerek iki nesnenin posizyonun aralar�nda sabit kalmas�n� sa�lar ve kamera oyuncuyu takip eder.
        transform.position = pl.transform.position + distance;
    }
}
