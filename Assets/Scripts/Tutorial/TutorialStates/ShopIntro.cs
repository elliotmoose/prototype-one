using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopIntro: TutorialState {
    public string introText = "Tap the 'OPEN SHOP' button to take a look at the weapons available. \nDon't worry, the game pauses automatically when you enter the shop!";
    public string buyAndUpgradeText = "Welcome!\nTry upgrading your weapon, and then buy a second weapon! Remember, you can only own 2 weapons in this game";
    //public string sellText = "Awesome! You can only own 2 weapons at a time. Let's try selling one of your weapons.";
    public string closeText = "Nice! Remember you can buy and sell weapons anytime in the shop!";
    public string finishingText = "Great Job! Use this arsenal. \nupgrade to your advantage.";

    protected int shopBtnNumber = 0;
   // protected int buySellBtnNumber = 0;
    protected int upgradeBtnNumber = 0;

    bool isSecondWeaponEquipped = false; 

    // public 

   // public GameObject GameOverScreen { get => gameOverScreen; set => gameOverScreen = value; }


    public ShopIntro(TutorialManager tutorialManager) :base(tutorialManager){}

    public override void Update(){
        if (TutorialManager.player.activeWeapons[1] != null){
            isSecondWeaponEquipped = true;
        }

        if(this.pressNumber == 1) {
            //to let the player open shop 
            //this.spriteClone.SetActive(false);
            this.setOverlay(false);

            if (shopBtnNumber == 1){
                // this.pressNumber += 1;
                // shopBtnNumber += 1; 
                StateBuyandUpgrade();
            }
        }


        if (this.pressNumber == 2){ //for the person to buy weapon 
            this.setOverlay(false);
            if (isSecondWeaponEquipped == true && upgradeBtnNumber >= 1){
                StateClose();
            }
        }

        if (this.pressNumber == 3){ //after sell screen
            GameObject.Find("UI").transform.Find("OpenShopButton").gameObject.GetComponent<Button>().onClick.Invoke();
            TutorialManager.SetState(new ToggleWeapon(TutorialManager));
            // this.setOverlay(false);
        }

    
    }

    public override void StateStart(){
        TutorialManager.shopButton.SetActive(true);
        TutorialManager.shopButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            shopBtnNumber ++;
        });
        TutorialManager.buySellButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            Debug.Log("Buy/sell button pressed!");
            if (!isSecondWeaponEquipped){
                NotificationManager.GetInstance().Notify("Buy another weapon!");
            }
            // else {
            //     buySellBtnNumber ++; 
            // }
            
        });
        TutorialManager.upgradeButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            upgradeBtnNumber ++;
        });
        TutorialManager.SetInstruction(introText);
        TutorialManager.player.AddDna(100000-50);

    }


    public void StateBuyandUpgrade(){
        TutorialManager.SetInstruction(buyAndUpgradeText);
        this.setOverlay(true);
    }


    // public void StateSell(){
    //     this.setOverlay(true);
    //     TutorialManager.SetInstruction(sellText);
    // }

    public void StateClose(){
        TutorialManager.SetInstruction(closeText);
        this.setOverlay(true);
    }

    public void StateEnd(){
        Time.timeScale = 0;
		TutorialManager.SetInstruction(finishingText);
		this.setOverlay(true);

    }

    
}   