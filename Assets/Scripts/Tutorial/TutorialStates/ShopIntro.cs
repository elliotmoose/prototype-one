using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopIntro: TutorialState {
    public string introText = "Tap the 'OPEN SHOP' button to take a look at the weapons available. \nDon't worry, the game automatically pauses when you enter the shop!";
    public string buyAndUpgradeText = "Welcome! Buy, and then upgrade another weapon! **PRESS BUY AND UPGRADE ONCE PLEASE**";
    public string sellText = "Awesome! You can only own 2 weapons at a time. Let's try selling one of your weapons.";
    public string closeText = "Nice! Now close the shop.";
    public string finishingText = "Great Job! Use this arsenal and upgrade to your advantage.";

    protected int shopBtnNumber = 0;
    protected int buySellBtnNumber = 0;
    protected int upgradeBtnNumber = 0;

   // public GameObject GameOverScreen { get => gameOverScreen; set => gameOverScreen = value; }


    public ShopIntro(TutorialManager tutorialManager) :base(tutorialManager){}

    public override void Update(){
        if(this.pressNumber == 1) {
            //to let the player open shop 
            //this.spriteClone.SetActive(false);
            this.setOverlay(false);
        
        }

        if (shopBtnNumber == 1){
            this.pressNumber += 1;
            shopBtnNumber = 0; 
            StateBuyandUpgrade();

        }

        if (this.pressNumber == 3){ //for the person to buy weapon 
            this.setOverlay(false);

        }

        if (buySellBtnNumber == 1 && upgradeBtnNumber == 1){
            this.pressNumber += 1; //pressNumber = 4
            buySellBtnNumber += 1; //buySellBtnNumber = 2 
            upgradeBtnNumber += 1; //upgradeBtnNumber = 2 
            StateSell();
        }

        if (this.pressNumber == 5){ //after sell screen
            this.setOverlay(false);
        }

        if (buySellBtnNumber == 3){
            this.pressNumber += 1; //pressNumber = 6 
            buySellBtnNumber += 1;
            StateClose();
        }

        if (this.pressNumber == 7){ //after sell screen
            this.setOverlay(false);
        }

        if (this.pressNumber == 8){
            TutorialManager.SetState(new ToggleWeapon(TutorialManager));
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
            buySellBtnNumber ++;
        });
        TutorialManager.upgradeButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            upgradeBtnNumber ++;
        });
        TutorialManager.SetInstruction(introText);
        TutorialManager.player.AddDna(1000000-50);

    }


    public void StateBuyandUpgrade(){
        TutorialManager.SetInstruction(buyAndUpgradeText);
        this.setOverlay(true);
    }


    public void StateSell(){
        this.setOverlay(true);
        TutorialManager.SetInstruction(sellText);
    }

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