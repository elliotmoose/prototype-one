using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TutorialState{

	protected TutorialManager TutorialManager;
	protected bool Paused;
	public TutorialState(TutorialManager tutorialManager){
		TutorialManager = tutorialManager;
	}

	public virtual void Start(){}

	public virtual void Update(){}

}