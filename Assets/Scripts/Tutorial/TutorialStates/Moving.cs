using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Moving: TutorialState{
	public string instructionText = "THIS IS THE MOVING INSTRUCTION\nOn your left is the moving joystick\nyou can use it to control your character!";
	public string finishingText = "GREAT!, TOUCH THE ARROW TO MOVE TO THE NEXT TUTORIAL";

	public GameObject[] tutorialPlane = new GameObject[4];
	public Vector3 spritePosition = new Vector3(130, 130, 0);

	public Moving(TutorialManager tutorialManager) : base(tutorialManager){}

	public override void Update(){
		if(this.pressNumber == 1){
			this.setOverlay(false);
			StateMain();
		}
		if(this.pressNumber == 2){
			// Go to the next state
			Debug.Log("NEXT STATE");
		}
	}

	public override void StateStart(){
		TutorialManager.movingJoystick.SetActive(true);
		TutorialManager.SetInstruction(instructionText);
		TutorialManager.InstructionSprite.GetComponent<Image>().sprite =  Resources.Load<Sprite>("Sprites/Tutorial/moving");
		TutorialManager.InstructionSprite.transform.position = spritePosition;

		// Instantiate planes
		tutorialPlane[0]  = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Tutorial/TutorialPlane"), this.player.transform.position + new Vector3(0, 0,5), this.player.transform.rotation) as GameObject;	
		tutorialPlane[1]  = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Tutorial/TutorialPlane"), this.player.transform.position + new Vector3(0, 0,-5), this.player.transform.rotation) as GameObject;	
		tutorialPlane[2]  = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Tutorial/TutorialPlane"), this.player.transform.position + new Vector3(5, 0,0), this.player.transform.rotation) as GameObject;	
		tutorialPlane[3]  = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Tutorial/TutorialPlane"), this.player.transform.position + new Vector3(-5, 0,0), this.player.transform.rotation) as GameObject;	
	

	}

	public void StateMain(){
		for(int i= 0; i < 4; i ++){
			if(tutorialPlane[i] != null) return;
		}
		StateEnd();
	}

	public void StateEnd(){
		Time.timeScale = 0;
		TutorialManager.SetInstruction(finishingText);
		TutorialManager.InstructionSprite.transform.position  =  this.offscreenPosition;
		this.setOverlay(true);

	}

}