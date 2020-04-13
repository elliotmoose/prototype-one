using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class eveIntro : TutorialState
{
    public GameObject Eve = new GameObject();
    public string instructionText = "This is Eve. Your mission is to work with Eve and fight off these nasty pathogens — before it's too late! ";
    //public Vector3 spritePosition = new Vector3(0, 0, 0);
    public eveIntro(TutorialManager tutorialManager) : base(tutorialManager){}

    public override void StateStart()
    {
        TutorialManager.SetInstruction(instructionText);

        // TutorialManager.InstructionSprite.GetComponent<Image>().sprite = Resources.Load<Sprite>("Prefabs/Player/Player");
        // TutorialManager.InstructionSprite.transform.position = spritePosition;
        // TutorialManager.InstructionSprite.SetActive(true);

        Eve = GameObject.Instantiate(new GameObject(), TutorialManager.TutorialSprite.transform.position ,  TutorialManager.Overlay.transform.rotation, TutorialManager.TutorialSprite.transform) as GameObject;
        Image image = Eve.AddComponent(typeof(Image)) as Image;
        image.gameObject.transform.localScale += new Vector3 (2.5f,2.5f,0);
        image.sprite = Resources.Load<Sprite>("Prefabs/Player/Player 1");
    }

    // Update is called once per frame
    public override void Update(){
		if(this.pressNumber == 1){
			// Go to the next state
            TutorialManager.SetState(new Moving(TutorialManager));
            //TutorialManager.SetState(new ToggleWeapon(TutorialManager));
		}
	}
}
