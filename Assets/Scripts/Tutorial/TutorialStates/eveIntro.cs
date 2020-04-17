using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class eveIntro : TutorialState{
    public GameObject Eve = new GameObject();
    public string instructionText = "This is Eve. Your mission is to work with Eve and fight off these nasty pathogens — before it's too late! ";
    public Vector3 spritePosition = new Vector3(0, 0, 0);
    public eveIntro(TutorialManager tutorialManager) : base(tutorialManager){}

    public override void StateStart()
    {
        TutorialManager.SetInstruction(instructionText);

        Eve = GameObject.Instantiate(TutorialManager.spriteTemplate, TutorialManager.Overlay.transform.position,  TutorialManager.Overlay.transform.rotation, TutorialManager.TutorialSprite.transform) as GameObject;
        Eve.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Tutorial/Eve");
        Eve.transform.localScale += new Vector3 (3f,3f,0);
        // Eve.transform.position = spritePosition;
    }

    // Update is called once per frame
    public override void Update(){
		if(this.pressNumber == 1){
			// Go to the next state
            TutorialManager.SetState(new MovingAndShooting(TutorialManager));
		}
	}
}
