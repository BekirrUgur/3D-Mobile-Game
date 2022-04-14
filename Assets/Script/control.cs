using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class control : MonoBehaviour
{

    public GameObject data;
   public void go_game() 
    {
                SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);  
    }
    public void quit()
    {
        Application.Quit();
    }
    public void data_reset()
    {
        data.SetActive(true);
        PlayerPrefs.DeleteAll();
        Invoke("invincible",2.0f);
    }

    void invincible() 
    {
        data.SetActive(false);
    }
}
