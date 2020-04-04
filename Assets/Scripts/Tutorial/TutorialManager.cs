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


	
	private TutorialState _currentState;

	void Start(){
		SetState(new Begin(this));
	}

	void Update(){
		_currentState.Update();
	}

	public void SetState(TutorialState state){
		_currentState = state;
		state.Start();
	}

	public void SetInstruction(string instructionText){
		Instruction.GetComponent<Text>().text = instructionText;
	}
}