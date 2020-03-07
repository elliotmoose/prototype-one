using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    

    public void PlayGame(){
        SceneManager.LoadScene("Main");
    }

    public void QuitGame(){
        SceneManager.LoadScene("Start");
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
