using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zones : TutorialState
{
    public Zones(TutorialManager tutorialManager) : base(tutorialManager){}

    public string instructionText = "These are zones! The green zones revives your health, while the yellow zones gives you more speed. They spawn randomly in the map, so look out for these icons to tell you where they currently are.";

    // Start is called before the first frame update
    public override void StateStart()
    {
        this.setOverlay(true);
        TutorialManager.SetInstruction(instructionText);
    }

    public override void Update(){
		if(this.pressNumber == 1){
			TutorialManager.SetState(new Finish(TutorialManager));
		}

	}
}
