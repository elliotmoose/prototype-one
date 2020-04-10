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
        SceneManager.LoadScene("Main");
    }

    public void QuitGame(){
        SceneManager.LoadScene("Start");
    }

    void Start(){
        Debug.Log("Gameobjects setting active!");
        
    }

    void Update(){
        hiScoreText.text = "Hi-Score: " + PlayerPrefs.GetFloat("hiScore");
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
