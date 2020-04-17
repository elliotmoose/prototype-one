using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{   
    public Text hiScoreText; 
    
    // public GameObject Boss;
    // public GameObject Virus;
    // public GameObject Bacteria;
    // public GameObject Infection;
    
    public void PlayGame(){
        TutorialManager.active = false;
        SceneManager.LoadScene("Main");
    }

    public void PlayTutorial(){
        TutorialManager.active = true;
        SceneManager.LoadScene("Main");
    }

    public void QuitGame(){
        SceneManager.LoadScene("Start");
        

    }

    void Update(){
        if(hiScoreText) 
        {
            hiScoreText.text = "Hi-Score: " + PlayerPrefs.GetFloat("hiScore", 0);
        }
    }

    bool isPaused = false;
    public void PauseResumeGame(){
        if (isPaused){
            Time.timeScale = 1;
            isPaused = false;
        }
        else{
            Time.timeScale = 0;
            isPaused = true;
        }
    }
}
