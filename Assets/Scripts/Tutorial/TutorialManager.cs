using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager: MonoBehaviour{
	public GameObject Instruction;

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