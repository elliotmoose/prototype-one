using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Begin: TutorialState{
	public string instructionText = "HELLO THERE, WELCOME TO INFECTIO";

	public Begin(TutorialManager tutorialManager) : base(tutorialManager){}

	public override void Update(){
		if (Input.GetKeyDown("space"))
        {
            Time.timeScale = 1;
        }
	}

	public override void Start(){
		TutorialManager.SetInstruction(instructionText);
		Debug.Log("WAITNG");
		Time.timeScale = 0;
	}

}