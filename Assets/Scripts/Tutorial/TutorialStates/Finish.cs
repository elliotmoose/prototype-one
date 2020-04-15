using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Finish: TutorialState {
    public string instructionText = "This concludes the turotial! Go forth and protect the body from being infected. \nI wish you good luck! ";

    public Finish(TutorialManager tutorialManager) : base(tutorialManager){}

    public override void StateStart(){
        TutorialManager.SetInstruction(instructionText);
    }

    // Update is called once per frame
    public override void Update(){
		if(this.pressNumber == 1){
			// turn off tutorial and start game 
            TutorialManager.gameObject.SetActive(false);
            SetUIActive();
            this.setOverlay(false);

		}
	}

    public void SetUIActive(){
        TutorialManager.movingJoystick.SetActive(true);
		TutorialManager.attackJoystick.SetActive(true);
		TutorialManager.switchButton.SetActive(true);
		TutorialManager.shopButton.SetActive(true);
		// TutorialManager.map.transform.GetChild(0).gameObject.SetActive(false);
		TutorialManager.gameManager.GetComponent<WaveManager>().enabled = true;
    }
 
}