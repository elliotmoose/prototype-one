using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class eveIntro : TutorialState
{
    public string instructionText = "This is Eve. Your mission is to work with Eve and fight off these nasty pathogens — before it's too late! ";
    public Vector3 spritePosition = new Vector3(0, 0, 0);
    public eveIntro(TutorialManager tutorialManager) : base(tutorialManager){}
    public override void StateStart()
    {
        TutorialManager.SetInstruction(instructionText);
        TutorialManager.InstructionSprite.GetComponent<Image>().sprite = Resources.Load<Sprite>("Prefabs/Player/Player");
        TutorialManager.InstructionSprite.transform.position = spritePosition;
        TutorialManager.InstructionSprite.SetActive(true);
    }

    // Update is called once per frame
    public override void Update(){
		if(this.pressNumber == 1){
			// Go to the next state
            TutorialManager.SetState(new Moving(TutorialManager));
		}
	}
}
