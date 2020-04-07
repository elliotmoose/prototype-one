using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class BossMode : MonoBehaviour {

    public bool isBossMode = false;

    void OnMouseDown(){
        Debug.Log("Boss was clicked!");
        if (isBossMode == false) {
            isBossMode = true;
            PlayerPrefs.SetInt("hack", 1);
            SceneManager.LoadScene("Main");
        }

        else {
            isBossMode = false;
        }
    }

}