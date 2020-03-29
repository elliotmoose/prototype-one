using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public abstract class TutorialState{

	protected TutorialManager TutorialManager;
	protected int pressNumber = 0;
	public TutorialState(TutorialManager tutorialManager){
		TutorialManager = tutorialManager;
	}

	public void Start(){
		TutorialManager.NextButton.GetComponent<Button>().onClick.AddListener(() =>
            {
                pressNumber ++;
            });
		Time.timeScale = 0;
		StateStart();
	}

	public virtual void StateStart(){}

	public virtual void Update(){}
}