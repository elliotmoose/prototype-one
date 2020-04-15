using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bars : TutorialState
{
    public Bars(TutorialManager tutorialManager) : base(tutorialManager){}

    public string instructionText = "Now, let's explain what all these bars do.";
    public string healthBar = "Below is your health bar! it keeps track of Eve's health.";
    public string waveBar = "And this bar keeps track of how many enemies you killed that wave. Deplete this bar, and you'll move on to the next wave.";
    public string infectionBar = "Beware, an infection will occur if you take too long to get rid of a wave, which is when this bar fills up.";

    public GameObject[] barSprite =  new GameObject[3];

    public override void StateStart()
    {
        TutorialManager.InstructionTextTop.transform.position = new Vector3(Screen.width/2, Screen.height/2,0);
        TutorialManager.SetInstruction(healthBar);

        barSprite[0] = GameObject.Instantiate(TutorialManager.healthBar, TutorialManager.healthBar.transform.position, TutorialManager.healthBar.transform.rotation, TutorialManager.TutorialSprite.transform) as GameObject;
        barSprite[1] = GameObject.Instantiate(TutorialManager.waveBar, TutorialManager.waveBar.transform.position, TutorialManager.waveBar.transform.rotation, TutorialManager.TutorialSprite.transform) as GameObject;
        barSprite[2] = GameObject.Instantiate(TutorialManager.infectionBar, TutorialManager.infectionBar.transform.position, TutorialManager.infectionBar.transform.rotation, TutorialManager.TutorialSprite.transform) as GameObject;
        barSprite[2].transform.Find("Foreground").GetComponent<Image>().fillAmount = 0.75f;
        barSprite[1].SetActive(false);
        barSprite[2].SetActive(false);
    }

    public override void Update(){
		if(this.pressNumber == 1){
            TutorialManager.SetInstruction(waveBar);
            barSprite[0].SetActive(false);
            barSprite[1].SetActive(true);
		}

        if(this.pressNumber == 2){
            TutorialManager.SetInstruction(infectionBar);
            barSprite[1].SetActive(false);
            barSprite[2].SetActive(true);
        }


        if(this.pressNumber == 3){
            TutorialManager.SetState(new RunAndGun(TutorialManager));
        }

	}
}
