using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Zones : TutorialState
{
    public Zones(TutorialManager tutorialManager) : base(tutorialManager){}

    public string instructionText = "These are zones! The green zones revives your health, while the yellow zones gives you more speed. They spawn randomly in the map, so look out for these icons to tell you where they currently are.";

    public GameObject[] zoneSprite = new GameObject[2];
    public string[] zoneSpriteName = {"healthZone", "fastZone"};

    // Start is called before the first frame update
    public override void StateStart()
    {
        this.setOverlay(true);
        TutorialManager.SetInstruction(instructionText);

        zoneSprite[0]  = GameObject.Instantiate(TutorialManager.spriteTemplate, TutorialManager.TutorialSprite.transform.position + new Vector3(-(Screen.width/6),(-Screen.height/5),0) ,  TutorialManager.Overlay.transform.rotation, TutorialManager.TutorialSprite.transform) as GameObject;	
		zoneSprite[1]  = GameObject.Instantiate(TutorialManager.spriteTemplate, TutorialManager.TutorialSprite.transform.position + new Vector3((Screen.width/6),(-Screen.height/5),0),  TutorialManager.Overlay.transform.rotation, TutorialManager.TutorialSprite.transform) as GameObject;	

        for( int i = 0; i < 2; i++){
			Image image = zoneSprite[i].GetComponent<Image>();
			image.gameObject.transform.localScale += new Vector3 (3.5f, 3.5f,0);
			image.sprite = Resources.Load<Sprite>("Sprites/Tutorial/" + zoneSpriteName[i]);
        }

    }

    public override void Update(){
		if(this.pressNumber == 1){
			TutorialManager.SetState(new Finish(TutorialManager));
		}

	}
}
