using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class starter : MonoBehaviour
{
    public GameObject canvas;
    public GameObject reset_bar;

    public void time_go() 
    {


        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);

        if(PlayerPrefs.GetInt("levelNumber")<1)
        {
            PlayerPrefs.SetInt("levelNumber", 1);
        }
        if (PlayerPrefs.GetInt("lvl_rqrd") < 10)
        {
            PlayerPrefs.SetInt("lvl_rqrd", 10);
        }
    }
    //"Reset" button in the start menu
    public void delete_all()
    {
        //Deletes all data in IndexedDB
        PlayerPrefs.DeleteAll();
        Debug.Log("DELETED");
        reset_bar.SetActive(true);
        Invoke("reset_close", 1.5f);
    }
    public void mini_menu() 
    {
        //In-game pause screen
        canvas.SetActive(true);
        Time.timeScale = 0;
        
    }
    //Return to main menu button
    public void go_menu() 
    {
        SceneManager.LoadScene("MainScreen", LoadSceneMode.Single);

    }
    //Resume game button
    public void continue_game() 
    {
        Time.timeScale = 1;
        canvas.SetActive(false);
    }
    private void reset_close() 
    {

        reset_bar.SetActive(false);
    
    }
}
