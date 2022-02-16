using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelController : MonoBehaviour
{
    public static LevelController Current;
    public List<GameObject> levels = new List<GameObject>();
    public GameObject gameOverMenu, finishGameMenu;
    public TextMeshPro scoreText, enemyScoreText,  adjectiveText;

    public int score;
    
    public int enemyScore;
   

    [Space]
    [Space]
    public GameObject CurrentLevel;
    public bool isTesting = false;
    // Start is called before the first frame update
    void Awake()
    {
        Current = this;
        
        if (isTesting == false)
        {

            if (levels.Count == 0)
            {

                foreach (Transform level in transform)
                {
                    levels.Add(level.gameObject);
                }
            }


            CurrentLevel = levels[PlayerPrefs.GetInt("level") % levels.Count];
            levels[PlayerPrefs.GetInt("level") % levels.Count].SetActive(true);
        }
        else
        {
            CurrentLevel.SetActive(true);
        }
    }

    public void ChangeScore(int increment)
    {
        
        
            score += increment;

       
        
    }

    public void FightScore(int increment)
    {
        if(score > enemyScore)
        {
            score += increment;
        }else if (score < enemyScore)
        {
            score--;
        }

       
        
        
    }

    public void Update()
    {
        scoreText.text = score.ToString();
        
        
        

        if(score >= 10)
        {
            adjectiveText.text = "Intermediate";
            PlayerController.Current.Wing.SetActive(false);
            PlayerController.Current.diamondWing.SetActive(true);
        } 
        if (score >= 20)
        {
            adjectiveText.text = "Advanced";
            PlayerController.Current.diamondWing.SetActive(false);
            PlayerController.Current.goldenWing.SetActive(true);
        }
        if(score < 10)
        {
            adjectiveText.text = "Noob";
        }

        
        
     
        
    }


}
