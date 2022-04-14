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
    //Ba�lang�� men�s�ndeki "Reset" butonu
    public void delete_all()
    {
        //IndexedDB i�erisindeki t�m veriyi siler
        PlayerPrefs.DeleteAll();
        Debug.Log("DELETED");
        reset_bar.SetActive(true);
        Invoke("reset_close", 1.5f);
    }
    public void mini_menu() 
    {
        //Oyun i�i durudurma ekran�
        canvas.SetActive(true);
        Time.timeScale = 0;
        
    }
    //Ana men�ye d�n�� butonu
    public void go_menu() 
    {
        SceneManager.LoadScene("MainScreen", LoadSceneMode.Single);

    }
    //Oyunu devam ettir butonu
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
