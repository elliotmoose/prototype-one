using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleWeapon : TutorialState
{
    public ToggleWeapon(TutorialManager tutorialManager) : base(tutorialManager){}

    //public string closeShop = "Close the Shop Menu by tapping on this button here";
    public string toggleDescription = "Time to try out your new weapon by using the Toggle button here!\nTry to destroy the object with your new weapon!";
    public GameObject toggleSprite = new GameObject();
    private int switchTimes = 0;
    public GameObject tutorialTarget;

    public float cooldown = 1.5f;
    public override void StateStart()
    {
        TutorialManager.SetInstruction(toggleDescription);

        TutorialManager.movingJoystick.SetActive(true);
        TutorialManager.attackJoystick.SetActive(true);
        TutorialManager.switchButton.SetActive(true);

        TutorialManager.switchButton.GetComponent<Button>().onClick.AddListener(() => 
        {
            switchTimes++;
        });

        this.spriteClone = GameObject.Instantiate(TutorialManager.switchButton, TutorialManager.switchButton.transform.position, TutorialManager.switchButton.transform.rotation, TutorialManager.TutorialSprite.transform) as GameObject;
        this.spriteClone.GetComponent<Button>().enabled = false;

        tutorialTarget  = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Tutorial/TutorialTargetSwitchState"), this.player.transform.position + new Vector3(0, 0.5f,5), this.player.transform.rotation) as GameObject;  

    }

    public void StateMain()
    {
        if(tutorialTarget == null){
            cooldown -= Time.deltaTime;
            if(cooldown <= 0){
                TutorialManager.SetState(new Zones(TutorialManager));
            }
        }
    }

    public override void Update(){
		if(this.pressNumber == 1)
        {
            //instructions finish, player gets to try out toggle button
			this.setOverlay(false);
			StateMain();
		}
	}

}
