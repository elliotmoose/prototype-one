using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Begin: TutorialState{
	public string instructionText = "HELLO THERE, WELCOME TO INFECTIO!\n Here is a short tutorial to get familiar\nwith out game!";
	public Begin(TutorialManager tutorialManager) : base(tutorialManager){}

	public override void Update(){
		if(this.pressNumber == 1){
			// Moving to next state
			TutorialManager.SetState(new Moving(TutorialManager));
		}
	}

	public override void StateStart(){
		TutorialManager.SetInstruction(instructionText);
	}

}