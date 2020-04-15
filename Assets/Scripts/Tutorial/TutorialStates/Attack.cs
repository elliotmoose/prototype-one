using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Attack: TutorialState{
	public string instructionText = "THIS IS THE ATTACK INSTRUCTION\nOn your right is the moving joystick\nyou can use it to control your character!";
	public string finishingText = "GREAT!, TOUCH THE ARROW TO MOVE TO THE NEXT TUTORIAL";
	public GameObject[] tutorialTarget = new GameObject[4];
	public Attack(TutorialManager tutorialManager) : base(tutorialManager){}

	public override void Update(){
		if(this.pressNumber == 1){
			this.spriteClone.SetActive(false);
			this.setOverlay(false);
			StateMain();
		}
		if(this.pressNumber == 2){
			// Go to the next state
			TutorialManager.SetState(new EnemyIntro(TutorialManager));
		}
	}

	public override void StateStart(){
		TutorialManager.movingJoystick.SetActive(false);
		TutorialManager.attackJoystick.SetActive(true);

		TutorialManager.SetInstruction(instructionText);
		this.spriteClone = GameObject.Instantiate(TutorialManager.attackJoystick, TutorialManager.attackJoystick.transform.position, TutorialManager.attackJoystick.transform.rotation, TutorialManager.TutorialSprite.transform) as GameObject;
		this.spriteClone.GetComponent<Joystick>().enabled = false;

		tutorialTarget[0]  = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Tutorial/TutorialTarget"), this.player.transform.position + new Vector3(0, 0.5f,5), this.player.transform.rotation) as GameObject;	
		tutorialTarget[1]  = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Tutorial/TutorialTarget"), this.player.transform.position + new Vector3(0, 0.5f,-5), this.player.transform.rotation) as GameObject;	
		tutorialTarget[2]  = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Tutorial/TutorialTarget"), this.player.transform.position + new Vector3(5, 0.5f,0), this.player.transform.rotation) as GameObject;	
		tutorialTarget[3]  = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Tutorial/TutorialTarget"), this.player.transform.position + new Vector3(-5, 0.5f,0), this.player.transform.rotation) as GameObject;	


	}

	public void StateMain(){
		for(int i= 0; i < 4; i ++){
			if(tutorialTarget[i] != null) return;
		}
		StateEnd();
	}

	public void StateEnd(){
		TutorialManager.SetInstruction(finishingText);
		this.setOverlay(true);

	}

}