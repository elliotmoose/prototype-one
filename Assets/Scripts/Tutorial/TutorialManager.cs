using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager: MonoBehaviour{
	public GameObject InstructionTextTop;
	public GameObject InstructionTextBottom;
	public GameObject NextButton;
	public GameObject InstructionSprite;
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

	public GameObject TutorialSprite;

	private TutorialState _currentState;
	public GameObject spriteTemplate;

	private static GameObject TutorialManagerGO;
	public bool wave0 = true;

	public static TutorialManager GetInstance() 
    {
        GameObject tutorialManagerGO = GameObject.Find("TutorialManager");
        if(tutorialManagerGO == null) 
        {
            Debug.LogError("TutorialManager GameObject has not been instantiated yet");
            return null;
        }

        TutorialManager tutorialManager = tutorialManagerGO.GetComponent<TutorialManager>();

        if(tutorialManager == null) 
        {
            Debug.LogError("GameManager has no component MapManager");
            return null;
        }

        return tutorialManager;
    }

	void Start(){
		SetState(new Begin(this));
		player = Player.GetInstance();
		TutorialManagerGO = this.gameObject;
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
}