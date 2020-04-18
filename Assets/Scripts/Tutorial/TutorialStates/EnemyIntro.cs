using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyIntro: TutorialState{
	public string instructionText = "Great! Introducing the nasty pathogens.\n";
	public string finishingText = "GREAT! TOUCH THE ARROW TO MOVE TO THE NEXT TUTORIAL";

	public static string e1Desciption = "Badteria\nBacteria enemy, can shoot out nasty stuff.\nAntibioitc Launcher works wonders against 'em.";
	public static string e2Desciption = "Bossteria\nThe boss.\nYou'll never want to see him. But when you do, look out for its nasty taser! ";
	public static string e3Desciption = "Vivi-rus\nThat flu virus.\n Super vulnerable to the Antiviral Blaster.";

	public EnemyIntro(TutorialManager tutorialManager) : base(tutorialManager){}

	public GameObject[] enemySprite = new GameObject[3];
	public GameObject[] enemyDescription = new GameObject[3];
	public string[] enemyDescriptionText = new string[] {e1Desciption, e2Desciption, e3Desciption}; 

	public override void Update(){
		if(this.pressNumber == 1){
			TutorialManager.SetState(new Bars(TutorialManager));
		}
	}

	public override void StateStart(){

		TutorialManager.SetInstruction(instructionText); 

		enemySprite[0]  = GameObject.Instantiate(TutorialManager.spriteTemplate, TutorialManager.TutorialSprite.transform.position + new Vector3(-(Screen.width/3),0,0) ,  TutorialManager.Overlay.transform.rotation, TutorialManager.TutorialSprite.transform) as GameObject;	
		enemySprite[1]  = GameObject.Instantiate(TutorialManager.spriteTemplate, TutorialManager.TutorialSprite.transform.position + new Vector3((Screen.width/3),0,0),  TutorialManager.Overlay.transform.rotation, TutorialManager.TutorialSprite.transform) as GameObject;	
		enemySprite[2]  = GameObject.Instantiate(TutorialManager.spriteTemplate, TutorialManager.TutorialSprite.transform.position + new Vector3(0,0,0),  TutorialManager.Overlay.transform.rotation, TutorialManager.TutorialSprite.transform) as GameObject;	

		for( int i = 0; i < 3; i++){
			Image image = enemySprite[i].GetComponent<Image>();
			image.gameObject.transform.localScale += new Vector3 (3f, 3f,0);
			image.sprite = Resources.Load<Sprite>("Sprites/Tutorial/e" + i);

			enemyDescription[i] = GameObject.Instantiate(TutorialManager.InstructionTextBottom,  new Vector3(enemySprite[i].transform.position.x,TutorialManager.InstructionTextBottom.transform.position.y , 0 ),  enemySprite[0].transform.rotation, TutorialManager.TutorialSprite.transform) as GameObject;	
			enemyDescription[i].GetComponent<RectTransform>().sizeDelta =  new Vector2(290, 190);
			enemyDescription[i].GetComponent<Text>().text = enemyDescriptionText[i];
			enemyDescription[i].GetComponent<Text>().fontSize = 30;
		}
	}

}