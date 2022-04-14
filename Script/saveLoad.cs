 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class saveLoad : MonoBehaviour
{
    public GameObject lock_btn;
    public GameObject actv_btn;
    public GameObject balance;

    public TextMeshProUGUI tot_score;
    public TextMeshProUGUI balance_text;



    public static saveLoad saver;

    public int level = 1;
    public static int level_required = 10;

    public int lvl_rqr;

  
    void Start()
    {
        /*"lvl_rqrd" and "levelNumber" data held with IndexedDB is checked and caused by data resets
         By making the first assignment of level 0 and 0 points problems, we prevent the loop from breaking.*/

        if (PlayerPrefs.GetInt("lvl_rqrd") < 10) 
        {
            PlayerPrefs.SetInt("lvl_rqrd", 10);
        }
        if (PlayerPrefs.GetInt("levelNumber") < 1)
        {
            PlayerPrefs.SetInt("levelNumber", 1);
        }

    }

    void Update()
    {

        lvl_rqr = PlayerPrefs.GetInt("lvl_rqrd");
        
    }
    public void level_up() 
    {
        level = PlayerPrefs.GetInt("levelNumber")+1;
        /* If there is no "levelNumber" data kept with IndexedDB with Playerprefs.SetInt, it creates a data with that name and assigns the variable level to it.
          If there is "levelNumber" data, it overwrites the variable level.*/
        PlayerPrefs.SetInt("levelNumber", level);
        level_required = PlayerPrefs.GetInt("lvl_rqrd") + 100;
        PlayerPrefs.SetInt("lvl_rqrd",level_required);
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }

    public void res_scene()
    {
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }

    //Activate next level button
    public void active_lvl() 
    {
        //Activates if the accumulated score in the safe is higher or equal to the score required to unlock the new level.
        if (PlayerPrefs.GetInt("score_account") >= lvl_rqr)
        {
            Debug.Log("Score account: "+PlayerPrefs.GetInt("score_account")+" Level Required: "+level_required);
            actv_btn.SetActive(true);
            lock_btn.SetActive(false);
            int x = PlayerPrefs.GetInt("score_account") - PlayerPrefs.GetInt("lvl_rqrd");
            PlayerPrefs.SetInt("score_account", x);
            tot_score.SetText(PlayerPrefs.GetInt("score_account").ToString());
            
        }
        else 
        {
            int y = PlayerPrefs.GetInt("lvl_rqrd")- PlayerPrefs.GetInt("score_account");
            balance.SetActive(true);
            //Error information given in case the accumulated score is not enough
            balance_text.SetText("You need " +y.ToString()+ " points to unlock the next stage");
           
            
        }
    }
}
