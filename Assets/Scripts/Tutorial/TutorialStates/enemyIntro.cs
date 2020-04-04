using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemyIntro : TutorialState
{
    public string instructionText1 = "At last, the moment you've been waiting for. Eve's enemies come in different shapes and sizes. We have:";
    public string instructionText2 = "Vivi-rus — that nasty virus getting you choked up in mucus during the flu season. Works well with the AA Blaster (Automatic Antiviral Blaster) , practically immune to  Antibiotics Launcher.";
    public string instructionText3 = "P.Aeru — remember him? This guy's the reason why your flu lasted so long. The Peni Launcher works like a charm, but AA Blaster's useless.";
    public string instructionText4 = "Brittney Streppus — Toxic is her middle name. Don't get rid of her in any way you can, and you're done for. ";

    public enemyIntro(TutorialManager tutorialManager) : base(tutorialManager){}

    public override void StateStart()
    {
        
    }

    public override void Update(){
		if(this.pressNumber == 1){
			// Go to the next state
            TutorialManager.SetState(new Bars(TutorialManager));
		}
	}
}
