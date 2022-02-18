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

        PlayerController.Current.sizeAnim.SetTrigger("size");
       
        
    }

    public void FightScore(int increment)
    {
        if(score > enemyScore)
        {
            score += increment;
            PlayerController.Current.sizeAnim.SetTrigger("size");
        }
        else if (score < enemyScore)
        {
            score--;
            PlayerController.Current.sizeAnim.SetTrigger("size");
        }

       
        
        
    }

    public void Update()
    {
        scoreText.text = score.ToString();
        
        
        

        if(score >= 4)
        {
            adjectiveText.text = "Intermediate";
            PlayerController.Current.Wing.SetActive(false);
            PlayerController.Current.diamondWing.SetActive(true);
            PlayerController.Current.diamondEffect.SetActive(true);
        } 
        if (score >= 15)
        {
            adjectiveText.text = "Advanced";
            PlayerController.Current.diamondWing.SetActive(false);
            PlayerController.Current.goldenWing.SetActive(true);
            PlayerController.Current.goldenEffect.SetActive(true);
        }
        if(score < 15 && score > 4)
        {
            PlayerController.Current.goldenWing.SetActive(false);
            PlayerController.Current.diamondWing.SetActive(true);
        }
        if(score < 10)
        {
            adjectiveText.text = "Noob";
        }

        
        
     
        
    }


}
