using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Moving: TutorialState{
	public string instructionText = "THIS IS THE MOVING INSTRUCTION\nOn your left is the moving joystick\nyou can use it to control your character!";

	public Moving(TutorialManager tutorialManager) : base(tutorialManager){}

	public override void Update(){
		if(this.pressNumber == 1){
			TutorialManager.Overlay.SetActive(false);
			Time.timeScale = 1;
		}
	}

	public override void StateStart(){
		TutorialManager.movingJoystick.SetActive(true);
		TutorialManager.SetInstruction(instructionText);
		TutorialManager.InstructionSprite.GetComponent<Image>().sprite =  Resources.Load<Sprite>("Sprites/Tutorial/moving");
		TutorialManager.InstructionSprite.transform.position = new Vector3(130, 130, 0);
	}

}