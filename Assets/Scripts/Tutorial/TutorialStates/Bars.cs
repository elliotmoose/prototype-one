using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bars : TutorialState
{
    public Bars(TutorialManager tutorialManager) : base(tutorialManager){}

    public string instructionText = "Now, let's explain what all these bars do.";
    public static string healthBar = "This bar keeps track of Eve's health.";
    public static string waveBar = "This bar keeps track of how many enemies you killed that wave. Deplete this bar, and you'll move on to the next wave.";
    public static string infectionBar = "Beware, an infection will occur if you take too long to get rid of a wave, which is when this bar fills up.";

    public GameObject[] barSprite = new GameObject[3];
    public GameObject[] barDescription = new GameObject[3];
    public string[] barSpriteName = { "healthBar", "waveBar", "infectionBar" };
    public string[] barDescriptionText = { healthBar, waveBar, infectionBar };

    public override void StateStart()
    {
        TutorialManager.SetInstruction(instructionText);

        barSprite[0]  = GameObject.Instantiate(new GameObject(), TutorialManager.TutorialSprite.transform.position + new Vector3(-(Screen.width/4),0,0) ,  TutorialManager.Overlay.transform.rotation, TutorialManager.TutorialSprite.transform) as GameObject;	
		barSprite[1]  = GameObject.Instantiate(new GameObject(), TutorialManager.TutorialSprite.transform.position + new Vector3((Screen.width/4),0,0),  TutorialManager.Overlay.transform.rotation, TutorialManager.TutorialSprite.transform) as GameObject;	
		barSprite[2]  = GameObject.Instantiate(new GameObject(), TutorialManager.TutorialSprite.transform.position ,  TutorialManager.Overlay.transform.rotation, TutorialManager.TutorialSprite.transform) as GameObject;	

        for (int i = 0; i < 3; i++)
        {
            Image image = barSprite[i].AddComponent(typeof(Image)) as Image;
			image.gameObject.transform.localScale += new Vector3 (2.5f,2.5f,0);
			image.sprite = Resources.Load<Sprite>("Sprites/" + barSpriteName[i]);
            barDescription[i] = GameObject.Instantiate(TutorialManager.InstructionTextBottom,  new Vector3(barSprite[i].transform.position.x,TutorialManager.InstructionTextBottom.transform.position.y , 0 ),  barSprite[0].transform.rotation, TutorialManager.TutorialSprite.transform) as GameObject;	
			barDescription[i].GetComponent<RectTransform>().sizeDelta =  new Vector2(170, 100);
			barDescription[i].GetComponent<Text>().text = barDescriptionText[i];
			barDescription[i].GetComponent<Text>().fontSize = 30;
        }
    }

    public override void Update(){
		if(this.pressNumber == 1){
			TutorialManager.SetState(new RunAndGun(TutorialManager));
		}

	}
}
