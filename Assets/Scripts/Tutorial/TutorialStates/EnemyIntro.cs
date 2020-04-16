using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyIntro: TutorialState{
	public string instructionText = "These are Eve's enemies.\nThey come in different shapes and sizes.";
	public string finishingText = "GREAT!, TOUCH THE ARROW TO MOVE TO THE NEXT TUTORIAL";

	public static string e1Desciption = "Vivi-rus\nthat nasty virus getting you choked up in mucus during the flu season. Works well with the AA Blaster (Automatic Antiviral Blaster), practically immune to Antibiotics Launcher.";
	public static string e2Desciption = "P.Aeru\nremember him? This guy's the reason why your flu lasted so long. The Peni Launcher works like a charm, but AA Blaster's useless.";
	public static string e3Desciption = "Brittney Streppus\nToxic is her middle name. Don't get rid of her in any way you can, and you're done for. ";

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

		enemySprite[0]  = GameObject.Instantiate(TutorialManager.spriteTemplate, TutorialManager.TutorialSprite.transform.position + new Vector3(-(Screen.width/6),0,0) ,  TutorialManager.Overlay.transform.rotation, TutorialManager.TutorialSprite.transform) as GameObject;	
		enemySprite[1]  = GameObject.Instantiate(TutorialManager.spriteTemplate, TutorialManager.TutorialSprite.transform.position + new Vector3((Screen.width/6),0,0),  TutorialManager.Overlay.transform.rotation, TutorialManager.TutorialSprite.transform) as GameObject;	

		for( int i = 0; i < 2; i++){
			Image image = enemySprite[i].GetComponent<Image>();
			image.gameObject.transform.localScale += new Vector3 (1.8f, 1.8f,0);
			image.sprite = Resources.Load<Sprite>("Sprites/Tutorial/e" + i);

			enemyDescription[i] = GameObject.Instantiate(TutorialManager.InstructionTextBottom,  new Vector3(enemySprite[i].transform.position.x,TutorialManager.InstructionTextBottom.transform.position.y , 0 ),  enemySprite[0].transform.rotation, TutorialManager.TutorialSprite.transform) as GameObject;	
			enemyDescription[i].GetComponent<RectTransform>().sizeDelta =  new Vector2(280, 180);
			enemyDescription[i].GetComponent<Text>().text = enemyDescriptionText[i];
			enemyDescription[i].GetComponent<Text>().fontSize = 30;
		}
	}

}