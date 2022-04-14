using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class scoreManager : MonoBehaviour
{
    public static scoreManager Instance;

    public TextMeshProUGUI menu_score,storeScore;
    public TextMeshProUGUI lvl_num;

    public int score=0;

    public static int totalScore=0, scoreTotal=0;
    
    private void Awake()
    {
        Instance = this;
        Time.timeScale = 1;
        menu_score.SetText("0");
    }
    private void Update()
    {
        levelUp();
        
    }

    //Add score function
    public void add_score(int _score) 
    {
        score += _score; 
    }

    //End game score
    public void final_score() 
    {
        totalScore = score;
        scoreTotal=PlayerPrefs.GetInt("score_account")+totalScore;


        storeScore.SetText(scoreTotal.ToString());
        menu_score.SetText(totalScore.ToString());
        PlayerPrefs.SetInt("score_account", scoreTotal);
        totalScore = 0;


    }
     public void levelUp() 
    {
        lvl_num.SetText(PlayerPrefs.GetInt("levelNumber").ToString());
        
    }
     
}
