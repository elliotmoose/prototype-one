using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager: MonoBehaviour{
	public GameObject Instruction;
	public GameObject NextButton;
	public GameObject InstructionSprite;
	public GameObject Overlay;

	public GameObject movingJoystick;
	public GameObject attackJoystick;
	public GameObject switchButton;
	public GameObject shopButton;
	public GameObject map;
	public GameObject gameManager;
	public GameObject player;

	public GameObject TutorialSprite;


	
	private TutorialState _currentState;

	void Start(){
		SetState(new Begin(this));
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
		Instruction.GetComponent<Text>().text = instructionText;
	}

	public void ClearTutorialSprite(){
		foreach (Transform child in TutorialSprite.transform)
             Destroy(child.gameObject);
	}
}