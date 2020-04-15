using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public abstract class TutorialState{

	protected TutorialManager TutorialManager;
	protected int pressNumber = 0;
	protected GameObject player;
	protected GameObject spriteClone;
	protected Vector3 offscreenPosition = new Vector3(-100,-100,-100);

	public TutorialState(TutorialManager tutorialManager){
		TutorialManager = tutorialManager;
	}

	public void Start(){
		player = GameObject.Find("Player");
		TutorialManager.NextButton.GetComponent<Button>().onClick.AddListener(() =>
            {
                pressNumber ++;
            });
		Time.timeScale = 0;
		initText();
		StateStart();
	}

	public virtual void StateStart(){}

	public virtual void Update(){}

	public void setOverlay(bool active){
		if(active){
			TutorialManager.Overlay.SetActive(true);
			Time.timeScale = 0;
		}else{
			TutorialManager.Overlay.SetActive(false);
			Time.timeScale = 1;
		}
	}

	public void initText(){
		TutorialManager.InstructionTextTop.GetComponent<Text>().text = "";
		TutorialManager.InstructionTextBottom.GetComponent<Text>().text = "";
	}

	

}