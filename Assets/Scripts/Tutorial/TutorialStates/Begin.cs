using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Begin: TutorialState{
	public string instructionText = "HELLO THERE, WELCOME TO INFECTIO!\n\n Here is a short tutorial for you to get\nfamiliar with our game!";
	public Begin(TutorialManager tutorialManager) : base(tutorialManager){}
	public override void Update(){
		if(this.pressNumber == 1){
			SetUiInactive();
			//TutorialManager.SetState(new EnemyIntro(TutorialManager));
			TutorialManager.SetState(new eveIntro(TutorialManager));
		}
	}

	public override void StateStart(){
		TutorialManager.SetInstruction(instructionText);
	}

	public void SetUiInactive(){
		TutorialManager.movingJoystick.SetActive(false);
		TutorialManager.attackJoystick.SetActive(false);
		TutorialManager.switchButton.SetActive(false);
		TutorialManager.shopButton.SetActive(false);
		// TutorialManager.map.transform.GetChild(0).gameObject.SetActive(false);
	}
}