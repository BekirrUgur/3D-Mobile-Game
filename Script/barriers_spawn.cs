using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barriers_spawn : MonoBehaviour
{

    private GameObject nos;
    private GameObject[] spawn_place;
    private GameObject[] barriers;

    int lvl_barriers;

    // Start is called before the first frame update
    void Start()
    {
        spawn_place = GameObject.FindGameObjectsWithTag("Respawn");
        barriers = GameObject.FindGameObjectsWithTag("barriers");
        nos = GameObject.FindGameObjectWithTag("nos");
        Invoke("spawn_barriers", 0.0f);

        lvl_barriers = PlayerPrefs.GetInt("levelNumber");
        
    }

    // Update is called once per frame
    void Update()
    {
        //Her seviyede gelecek olan bariyer sayısını kontrol eder eğer maksimum seviyeyi aşmışsa oyunda kırılma olmaması için sabitler.
        if(lvl_barriers >= 16) 
        {
            lvl_barriers = 16;
        }
    }
    void spawn_barriers()
    {
        //16
        int control = -1;
        //Sahne içerisine yüklenecek olan bariyerler belirlenen "spawn" ortaya çıkma noktalarında bariyerler arasında rastgele bir şekilde sahneye çağırılır.
        for (int i = 0; i < lvl_barriers+6; i++)
        {
            int rast = Random.Range(0, barriers.Length);

            if (rast != control) 
            {
                control = rast;
                GameObject new_barriers = Instantiate(barriers[rast], spawn_place[i].transform.position, Quaternion.identity);
            }
            else if (rast==control && rast == 0) 
            {
                control = rast;
                GameObject new_barriers = Instantiate(barriers[rast+1], spawn_place[i].transform.position, Quaternion.identity);
            }
            else 
            {
                control = rast;
                GameObject new_barriers = Instantiate(barriers[rast - 1], spawn_place[i].transform.position, Quaternion.identity);
            }

        } 
        //Sahnede toplanılan domateslerin engellerin posizyonlarına göre belirli bir aralıkla sahneye çağırılır.
        for (int i = 0; i < lvl_barriers+6; i++)
        {
            int rnos = Random.Range(0, 3);
            GameObject new_nos = Instantiate(nos, new Vector3(spawn_place[i].transform.position.x+2, spawn_place[i].transform.position.y, spawn_place[i].transform.position.z+2), Quaternion.identity);
        }
        


    }
}
