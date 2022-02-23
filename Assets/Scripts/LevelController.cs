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
    public TextMeshPro  enemyScoreText;
    public bool gameActive = false;
   

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

    public void NextLevel()
    {
        StartCoroutine(LevelUp());
    }

    public IEnumerator LevelUp()
    {
       


        //Debug.Log((levels.IndexOf(CurrentLevel)+1).ToString() +"  " +levels.Count.ToString());


        if ((levels.IndexOf(CurrentLevel) + 1) == levels.Count)
        {
            

            //FindObjectOfType<SkillManager>().SetFinishSkillPanel();

            PlayerPrefs.SetInt("level", PlayerPrefs.GetInt("level") + 1);
            yield return new WaitForSeconds(.5f);

            yield return new WaitForSeconds(1.5f);
            //  GameHandler.Instance.Appear_TransitionPanel();
            

                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);


        }


        else
        {
            CurrentLevel = levels[(PlayerPrefs.GetInt("level") + 1) % levels.Count];
            yield return new WaitForSeconds(1f);

            
            levels[(PlayerPrefs.GetInt("level")) % levels.Count].SetActive(false);


            PlayerPrefs.SetInt("level", PlayerPrefs.GetInt("level") + 1);
            yield return new WaitForSeconds(1f);
            levels[PlayerPrefs.GetInt("level") % levels.Count].SetActive(true);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        }


    }

    /*public void ChangeScore(int increment)
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

       
        
        
    }*/

    /*public void Update()
    {
        scoreText.text = score.ToString();

 
        

        if(score >= 4)
        {
            adjectiveText.text = "Intermediate";
            PlayerController.Current.Wing.SetActive(false);
            PlayerController.Current.backpack.SetActive(false);
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


        if (PlayerController.Current.fail)
        {
            CameraController.Current.target = null;
        }
        else
        {
            return;
        }
     
        
    }*/


    public void StartLevel()
    {
        gameActive = true;
        PlayerController.Current.anim.SetBool("Run", true);
        PlayerController.Current.anim.SetBool("Idle", false);
        PlayerController.Current.playButton.SetActive(false);
    }


}
