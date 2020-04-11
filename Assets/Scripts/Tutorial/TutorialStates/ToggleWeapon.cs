using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleWeapon : TutorialState
{
    public ToggleWeapon(TutorialManager tutorialManager) : base(tutorialManager){}

    //public string closeShop = "Close the Shop Menu by tapping on this button here";
    public string toggleDescription = "Time to try out your new weapon by using the Toggle button here! Use this button to easily switch between the 2 weapons that you own";
    public GameObject toggleSprite = new GameObject();

    public override void StateStart()
    {
        TutorialManager.SetInstruction(toggleDescription);

        toggleSprite = GameObject.Instantiate(new GameObject(), TutorialManager.TutorialSprite.transform.position ,  TutorialManager.Overlay.transform.rotation, TutorialManager.TutorialSprite.transform) as GameObject;
        Image image = toggleSprite.AddComponent(typeof(Image)) as Image;
        image.gameObject.transform.localScale += new Vector3 (2.5f,2.5f,0);
        image.sprite = Resources.Load<Sprite>("Sprites/ChangeWeaponButton");
    }

    public void StateMain()
    {
        TutorialManager.movingJoystick.SetActive(true);
		TutorialManager.attackJoystick.SetActive(true);
        TutorialManager.toggleWeaponButton.SetActive(true);
    }

    public override void Update(){
		if(this.pressNumber == 1)
        {
            //instructions finish, player gets to try out toggle button
			this.setOverlay(false);
			StateMain();
		}
		if(this.pressNumber == 2){
			// Go to the next state
			//TutorialManager.SetState(new Zones(TutorialManager));
		}
	}

    public void StateEnd()
    {
        this.setOverlay(true);
    }
}
