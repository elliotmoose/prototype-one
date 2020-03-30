using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{   
    public GameObject Eve;
    public GameObject Boss;
    public GameObject Virus;
    public GameObject Bacteria;
    public GameObject Infection;
    
    public void PlayGame(){
        SceneManager.LoadScene("Main");
    }

    public void QuitGame(){
        SceneManager.LoadScene("Start");
    }

    void Start(){
        Debug.Log("Gameobjects setting active!");
        Eve.SetActive(true);
        Boss.SetActive(true);
        Virus.SetActive(true);
        Bacteria.SetActive(true);
        Infection.SetActive(true);
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
