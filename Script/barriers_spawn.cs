using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barriers_spawn : MonoBehaviour
{

    private GameObject nos;
    private GameObject[] spawn_place;
    private GameObject[] barriers;

    int lvl_barriers;

    
    void Start()
    {
        spawn_place = GameObject.FindGameObjectsWithTag("Respawn");
        barriers = GameObject.FindGameObjectsWithTag("barriers");
        nos = GameObject.FindGameObjectWithTag("nos");
        Invoke("spawn_barriers", 0.0f);

        lvl_barriers = PlayerPrefs.GetInt("levelNumber");
        
    }

    
    void Update()
    {
        //It controls the number of barriers that will come in each level, and if it exceeds the maximum level, it is fixed so that there is no break in the game.
        if (lvl_barriers >= 16) 
        {
            lvl_barriers = 16;
        }
    }
    void spawn_barriers()
    {
        //16
        int control = -1;
        //Barriers to be loaded into the scene are randomly summoned to the scene between the barriers at the determined "spawn" spawn points.
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
        //The part where the tomatoes collected on the stage are called to the stage at a certain interval according to the positions of the obstacles.
        for (int i = 0; i < lvl_barriers+6; i++)
        {
            int rnos = Random.Range(0, 3);
            GameObject new_nos = Instantiate(nos, new Vector3(spawn_place[i].transform.position.x+2, spawn_place[i].transform.position.y, spawn_place[i].transform.position.z+2), Quaternion.identity);
        }
        


    }
}
