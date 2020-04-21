using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyIntro: TutorialState{
	public string instructionText = "BEWARE OF THESE PATHOGENS:\n";
	public string finishingText = "GREAT! TOUCH THE ARROW TO MOVE TO THE NEXT TUTORIAL";

	public static string e1Desciption = "<size=40>Badteria</size>\nBacteria: shoots out nasty stuff.\n\n";
	public static string e2Desciption = "<size=40>Bossteria</size>\nThe Boss.\nYou'll never want to see him. \nBut when you do, look out for its nasty taser! ";
	public static string e3Desciption = "<size=40>Vivi-rus</size>\nVirus: attacks at close range.\n\n";

	public static string w1Description = "Super vulnerable to the Antiviral Flamethrower.";
	public static string w2Description = "Antibioitcs Launcher works wonders against 'em.";


	public EnemyIntro(TutorialManager tutorialManager) : base(tutorialManager){}

	public GameObject[] enemySprite = new GameObject[3];
	public GameObject[] enemyDescription = new GameObject[3];
	public GameObject[] weapon = new GameObject[2];
	public GameObject [] weaponDescription = new GameObject[2];
	public string[] enemyDescriptionText = new string[] {e1Desciption, e2Desciption, e3Desciption}; 
	public string[] weaponDescriptionText = new string[] {w2Description, w1Description};

	public override void Update(){
		if(this.pressNumber == 1){
			TutorialManager.SetState(new Bars(TutorialManager));
		}
	}

	public override void StateStart(){

		TutorialManager.SetInstruction(instructionText); 

		enemySprite[0]  = GameObject.Instantiate(TutorialManager.spriteTemplate, TutorialManager.TutorialSprite.transform.position + new Vector3(-(Screen.width/3) - 30 ,50,0) ,  TutorialManager.Overlay.transform.rotation, TutorialManager.TutorialSprite.transform) as GameObject;	
		enemySprite[1]  = GameObject.Instantiate(TutorialManager.spriteTemplate, TutorialManager.TutorialSprite.transform.position + new Vector3((Screen.width/3 - 30),-150,0),  TutorialManager.Overlay.transform.rotation, TutorialManager.TutorialSprite.transform) as GameObject;	
		enemySprite[2]  = GameObject.Instantiate(TutorialManager.spriteTemplate, TutorialManager.TutorialSprite.transform.position + new Vector3(-30,50,0),  TutorialManager.Overlay.transform.rotation, TutorialManager.TutorialSprite.transform) as GameObject;	

		weapon[0]  = GameObject.Instantiate(TutorialManager.spriteTemplate, TutorialManager.TutorialSprite.transform.position + new Vector3(-(Screen.width/3)- 40 ,-300,0) ,  TutorialManager.Overlay.transform.rotation, TutorialManager.TutorialSprite.transform) as GameObject;	
		weapon[1]  = GameObject.Instantiate(TutorialManager.spriteTemplate, TutorialManager.TutorialSprite.transform.position + new Vector3(-30,-300,0),  TutorialManager.Overlay.transform.rotation, TutorialManager.TutorialSprite.transform) as GameObject;	

		// weaponDescription[0]  = GameObject.Instantiate(TutorialManager.spriteTemplate, TutorialManager.TutorialSprite.transform.position + new Vector3(-(Screen.width/3)- 60 ,-300,0) ,  TutorialManager.Overlay.transform.rotation, TutorialManager.TutorialSprite.transform) as GameObject;	
		// weaponDescription[1]  = GameObject.Instantiate(TutorialManager.spriteTemplate, TutorialManager.TutorialSprite.transform.position + new Vector3(-30,-300,0),  TutorialManager.Overlay.transform.rotation, TutorialManager.TutorialSprite.transform) as GameObject;	
		
		for( int i = 0; i < 3; i++){
			Image image = enemySprite[i].GetComponent<Image>();
			if (i == 2){ //virus 
				image.gameObject.transform.localScale += new Vector3 (1f, 1f,0);
				
			}

			else if (i == 1){ //bacteria 
				image.gameObject.transform.localScale += new Vector3 (3f, 3f,0);
			}

			else {
				image.gameObject.transform.localScale += new Vector3 (3f, 3f,0);
			}
			
			image.sprite = Resources.Load<Sprite>("Sprites/Tutorial/e" + i);

			enemyDescription[i] = GameObject.Instantiate(TutorialManager.InstructionTextBottom,  new Vector3(enemySprite[i].transform.position.x,TutorialManager.InstructionTextBottom.transform.position.y  , 0 ),  enemySprite[0].transform.rotation, TutorialManager.TutorialSprite.transform) as GameObject;	
			enemyDescription[i].GetComponent<RectTransform>().sizeDelta =  new Vector2(290, 190);
			enemyDescription[i].GetComponent<Text>().text = enemyDescriptionText[i];
			enemyDescription[i].GetComponent<Text>().fontSize = 30;
		}

		for (int j = 0; j < 2; j++){
			Image image = weapon[j].GetComponent<Image>();
			if (j == 0){ // bacteria 
				image.sprite = Resources.Load<Sprite>("Sprites/Tutorial/antibiotics launcher");
				image.gameObject.transform.localScale += new Vector3 (3f, 3f,0);
				weaponDescription[j] = GameObject.Instantiate(TutorialManager.InstructionTextBottom,  new Vector3(weapon[j].transform.position.x + 20,enemySprite[1].transform.position.y - 150 , 0 ),  weapon[0].transform.rotation, TutorialManager.TutorialSprite.transform) as GameObject;	

			}

			if (j == 1) { //virus
				image.sprite = Resources.Load<Sprite>("Sprites/Tutorial/flamethrower ");
				image.gameObject.transform.localScale += new Vector3 (3f, 3f,0);
				weaponDescription[j] = GameObject.Instantiate(TutorialManager.InstructionTextBottom,  new Vector3(weapon[j].transform.position.x, enemySprite[2].transform.position.y - 350, 0 ),  weapon[0].transform.rotation, TutorialManager.TutorialSprite.transform) as GameObject;	

			}

			weaponDescription[j].GetComponent<RectTransform>().sizeDelta =  new Vector2(290, 190);
			weaponDescription[j].GetComponent<Text>().text = weaponDescriptionText[j];
			weaponDescription[j].GetComponent<Text>().fontSize = 30;
		}
	}

}