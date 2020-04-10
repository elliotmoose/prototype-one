using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Begin: TutorialState{
	public string instructionText = "HELLO THERE, WELCOME TO INFECTIO!\n Here is a short tutorial for you to get\nfamiliar with our game!";
	public Begin(TutorialManager tutorialManager) : base(tutorialManager){}

	public override void Update(){
		if(this.pressNumber == 1){
			// Moving to next state
			SetUiInactive();
			TutorialManager.SetState(new eveIntro(TutorialManager));
			//TutorialManager.SetState(new EnemyIntro(TutorialManager));
		}
	}

	public override void StateStart(){
		TutorialManager.SetInstruction(instructionText);
	}

	public void SetUiInactive(){
		// attackJoystick.GetComponent<Joystick>().disabled = true; 
		TutorialManager.movingJoystick.SetActive(false);
		TutorialManager.attackJoystick.SetActive(false);
		TutorialManager.switchButton.SetActive(false);
		TutorialManager.shopButton.SetActive(false);
		TutorialManager.map.transform.GetChild(0).gameObject.SetActive(false);
		TutorialManager.gameManager.GetComponent<WaveManager>().enabled = false;
	}
}