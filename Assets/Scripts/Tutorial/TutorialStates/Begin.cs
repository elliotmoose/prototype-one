using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Begin: TutorialState{
	public string instructionText = "\n\n\nHELLO THERE, WELCOME TO INFECTIO!\n\n Here is a short tutorial.";
	public Begin(TutorialManager tutorialManager) : base(tutorialManager){}
	public override void Update(){
		if(this.pressNumber == 1){
			SetUiInactive();
			//TutorialManager.SetState(new ShopIntro(TutorialManager));
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