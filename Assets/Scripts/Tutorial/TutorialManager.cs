using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager: MonoBehaviour{
	public GameObject InstructionTextTop;
	public GameObject InstructionTextBottom;
	public GameObject NextButton;
	public GameObject Overlay;

	public GameObject movingJoystick;
	public GameObject attackJoystick;
	public GameObject switchButton;
	public GameObject shopButton;
	public GameObject buySellButton;
	public GameObject upgradeButton;
	public GameObject shopScreen;
	public GameObject map;
	public GameObject gameManager;
	public GameObject healthBar;
	public GameObject waveBar;
	public GameObject infectionBar;
	
	public Player player;
	public GameObject Tutorial;
	public NotificationManager notificationManager;

	public GameObject TutorialSprite;

	private TutorialState _currentState;
	public GameObject spriteTemplate;

	public bool active = false;

	public static TutorialManager GetInstance() 
    {
        GameObject GameManager = GameObject.Find("GameManager");
        if(GameManager == null) 
        {
            Debug.LogError("GameManager GameObject has not been instantiated yet");
            return null;
        }

        TutorialManager tutorialManager = GameManager.GetComponent<TutorialManager>();

        if(tutorialManager == null) 
        {
            Debug.LogError("GameManager has no component TutorialManager");
            return null;
        }

        return tutorialManager;
    }

	void Awake(){
		if(!active){
			Tutorial.SetActive(false);
			this.enabled = false;
		}else{
			Tutorial.SetActive(true);
			player = Player.GetInstance();
			SetState(new Begin(this));
		}
	}

	void Update(){
		_currentState.Update();
	}

	public void SetState(TutorialState state){
		ClearTutorialSprite();
		_currentState = state;
		state.Start();
	}

	public void SetInstruction(string instructionText){
		InstructionTextTop.GetComponent<Text>().text = instructionText;
	}

	public void ClearTutorialSprite(){
		foreach (Transform child in TutorialSprite.transform)
             Destroy(child.gameObject);
	}

	// bool checkFirsTime(){
	// 	FirstTime = PlayerPrefs.GetInt("FirstTime", 1);
	// 	if (FirstTime == 0){
	// 		this.gameObject.SetActive(false);
	// 		return false;
	// 	}
	// 	return true;
	// }
}