using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyIntro: TutorialState{
	public string instructionText = "THESE ARE THE ENEMY YOU WILL FACE!";
	public string finishingText = "GREAT!, TOUCH THE ARROW TO MOVE TO THE NEXT TUTORIAL";
	public static string e1Desciption = "THIS IS E1";
	public static string e2Desciption = "THIS IS E2";
	public static string e3Desciption = "THIS IS E3";

	public EnemyIntro(TutorialManager tutorialManager) : base(tutorialManager){}

	public GameObject[] enemySprite = new GameObject[3];
	public GameObject[] enemyDescription = new GameObject[3];
	public string[] enemySpriteName = {"e1", "e2", "e3"}; 
	public string[] enemyDescriptionText = new string[] {e1Desciption, e2Desciption, e3Desciption}; 
	public override void Update(){
		if(this.pressNumber == 1){
			this.spriteClone.SetActive(false);
			this.setOverlay(false);
			StateMain();
		}
		if(this.pressNumber == 2){
			// Go to the next state
			Debug.Log("NEXT STATE");
		}
	} 

	public override void StateStart(){

		TutorialManager.SetInstruction(instructionText); 

		enemySprite[0]  = GameObject.Instantiate(new GameObject(), TutorialManager.TutorialSprite.transform.position + new Vector3(-(Screen.width/4),0,0) ,  TutorialManager.Overlay.transform.rotation, TutorialManager.TutorialSprite.transform) as GameObject;	
		enemySprite[1]  = GameObject.Instantiate(new GameObject(), TutorialManager.TutorialSprite.transform.position + new Vector3((Screen.width/4),0,0),  TutorialManager.Overlay.transform.rotation, TutorialManager.TutorialSprite.transform) as GameObject;	
		enemySprite[2]  = GameObject.Instantiate(new GameObject(), TutorialManager.TutorialSprite.transform.position ,  TutorialManager.Overlay.transform.rotation, TutorialManager.TutorialSprite.transform) as GameObject;	

		for( int i = 0; i < 3;i ++){
			Image image = enemySprite[i].AddComponent(typeof(Image)) as Image;
			image.gameObject.transform.localScale += new Vector3 (2.5f,2.5f,0);
			image.sprite = Resources.Load<Sprite>("Sprites/Tutorial/" + enemySpriteName[i]);

			enemyDescription[i] = GameObject.Instantiate(TutorialManager.InstructionTextBottom,  new Vector3(enemySprite[i].transform.position.x,TutorialManager.InstructionTextBottom.transform.position.y , 0 ),  enemySprite[0].transform.rotation, TutorialManager.TutorialSprite.transform) as GameObject;	
			// Text description = enemyDescription[i].AddComponent(typeof(Text)) as Text;
			enemyDescription[i].GetComponent<RectTransform>().sizeDelta =  new Vector2(200, 100);
			enemyDescription[i].GetComponent<Text>().text = enemyDescriptionText[i];
			enemyDescription[i].GetComponent<Text>().fontSize = 45;
		}
	}

	public void StateMain(){
		// for(int i= 0; i < 4; i ++){
		// 	if(tutorialTarget[i] != null) return;
		// }
		// StateEnd();
	}

	public void StateEnd(){
		Time.timeScale = 0;
		TutorialManager.SetInstruction(finishingText);
		this.setOverlay(true);
	}

}