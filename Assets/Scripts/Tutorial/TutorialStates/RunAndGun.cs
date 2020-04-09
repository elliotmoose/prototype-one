using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RunAndGun: TutorialState{
	public string instructionText = "Now let's try to defeat these nasty bacteria";
	public string finishingText = "GREAT!, TOUCH THE ARROW TO MOVE TO THE NEXT TUTORIAL";

	public RunAndGun(TutorialManager tutorialManager) : base(tutorialManager){}

	public override void Update(){
		if(this.pressNumber == 1){
			this.setOverlay(false);
			StateMain();
		}
		if(this.pressNumber == 2){
			// Go to the next state
			Debug.Log("next state");
			// TutorialManager.SetState(new EnemyIntro(TutorialManager));
		}
	}

	public override void StateStart(){
		TutorialManager.movingJoystick.SetActive(true);
		TutorialManager.attackJoystick.SetActive(true);
		TutorialManager.gameManager.GetComponent<WaveManager>().enabled = true;

		TutorialManager.SetInstruction(instructionText);
	
	}

	public void StateMain(){
		// if( GameObject.FindGameObjectsWithTag("enemy") == null && ){
		// 	StateEnd();
		// }
	}

	public void StateEnd(){
		Time.timeScale = 0;
		TutorialManager.SetInstruction(finishingText);
		this.setOverlay(true);
	}

}