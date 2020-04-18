using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RunAndGun: TutorialState{
	public string instructionText = "Now let's try to defeat these nasty bacteria";
	public string finishingText = "Great work! You can see upon dying, the enemy drops DNA like this on the ground.\nYou will need them to buy and upgrade weapons!";

	public RunAndGun(TutorialManager tutorialManager) : base(tutorialManager){}

	public override void Update(){
		if(this.pressNumber == 1){
			this.setOverlay(false);
			pressNumber ++;
		}

		if(this.pressNumber == 2){
			// Go to the next state
			StateMain();
		}

		if(this.pressNumber == 3){
			// Go to the next state
			TutorialManager.SetState(new ShopIntro(TutorialManager));
		}
	}

	public override void StateStart(){
		TutorialManager.movingJoystick.SetActive(true);
		TutorialManager.attackJoystick.SetActive(true);
		TutorialManager.gameManager.GetComponent<WaveManager>().enabled = true;

		TutorialManager.SetInstruction(instructionText);
	
	}

	public void StateMain(){
		if(WaveManager.GetInstance().tutorialComplete == true){
			StateEnd();
		}
	}

	public void StateEnd(){
		Time.timeScale = 0;
		TutorialManager.SetInstruction(finishingText);
		GameObject sprite  = GameObject.Instantiate(TutorialManager.spriteTemplate, TutorialManager.TutorialSprite.transform.position ,  TutorialManager.Overlay.transform.rotation, TutorialManager.TutorialSprite.transform) as GameObject;	
		Image image = sprite.GetComponent<Image>();
		image.gameObject.transform.localScale += new Vector3 (2f, 2f,0);
		image.sprite = Resources.Load<Sprite>("Sprites/dna 2");

		this.setOverlay(true);
		TutorialManager.gameManager.GetComponent<WaveManager>().enabled = false;
	}

}