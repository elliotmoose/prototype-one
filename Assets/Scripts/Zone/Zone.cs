using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Zone : MonoBehaviour {
	public bool PlayerInside;
	public string name;
	public float duration;
	public bool active;
	// public GameObject zoneGameObject;


	protected Entity PLayerEntity;

	void Start(){
		active = true;
		PLayerEntity = GameObject.Find("Player").GetComponent<Entity>();
	}

	void Update(){
		UpdateDurarion();
		if(duration < 0){
			active = false;
		}
	}

	void UpdateDurarion() 
	{
		duration -= Time.deltaTime;     
	}

	void OnTriggerEnter(Collider target){	
		if(target.gameObject.name == "Player"){
			PlayerInside = true;
			OnEnterZone();
		}
	}

	void OnTriggerExit(Collider target){
		if(target.gameObject.name == "Player"){
			PlayerInside = false;
			OnExitZone();
		}
	}

	void OnTriggerStay(Collider target){
		if(target.gameObject.name == "Player"){
			StayInZone();
		}
	}

	void OnDestroy() {
		if(PlayerInside){
			OnExitZone();
		}
    }

	public virtual void OnEnterZone(){}
	public virtual void OnExitZone(){}
	public virtual void StayInZone(){}

}