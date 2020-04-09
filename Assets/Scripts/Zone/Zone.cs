using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Zone : MonoBehaviour {
	public bool PlayerInside;
	public string name;
	public float duration;
	public bool active;
	public GameObject arrowPointer;
	public GameObject zonePointer;
	// public GameObject map;


	protected Entity playerEntity;
	protected Vector3 zonePosition;
	protected Vector3 fromPosition;
	protected RectTransform arrowTransform;
	protected Vector3 zonePositionScreenPoint;
	protected float arrowBorderSize = 50;
		

	void Start(){
		active = true;
		playerEntity = Player.GetInstance();
        zonePointer = GameObject.Instantiate(arrowPointer, this.transform.position, this.transform.rotation, this.transform) as GameObject;	
		arrowTransform = zonePointer.transform.Find("Arrow").GetComponent<RectTransform>();
	}

	void Update(){
		UpdateDurarion();
		ArrowPoint();
		if(duration < 0){
			active = false;
		}
		if(PlayerInside){
			zonePointer.SetActive(false);
		}else{
			zonePointer.SetActive(true);
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

	public void ArrowPoint(){
		zonePosition = this.transform.position;
		fromPosition = Camera.main.transform.position;
		zonePosition.y = 0f;
		fromPosition.y = 0f;
		Vector3 diff = (zonePosition - fromPosition).normalized;
		float angle = getAngleFromVector(diff);
		arrowTransform.localEulerAngles = new Vector3(0,0,-angle);
	
		zonePositionScreenPoint = Camera.main.WorldToScreenPoint(zonePosition);
		bool zoneOffScreen = zonePositionScreenPoint.x <= 0 || zonePositionScreenPoint.x >= Screen.width || zonePositionScreenPoint.y <= 0 || zonePositionScreenPoint.y >= Screen.height;
		
		if(zonePositionScreenPoint.x <= arrowBorderSize ) zonePositionScreenPoint.x = arrowBorderSize;
		if(zonePositionScreenPoint.x > Screen.width - arrowBorderSize) zonePositionScreenPoint.x = Screen.width - arrowBorderSize;
		if(zonePositionScreenPoint.y <= arrowBorderSize ) zonePositionScreenPoint.y = arrowBorderSize;
		if(zonePositionScreenPoint.y > Screen.height - arrowBorderSize) zonePositionScreenPoint.y = Screen.height - arrowBorderSize;
		arrowTransform.position = zonePositionScreenPoint;
		arrowTransform.localPosition = new Vector3(arrowTransform.localPosition.x, arrowTransform.localPosition.y, -5f);
	}

	float getAngleFromVector(Vector3 vector){
		float x = vector.x;
		float y = vector.z;
		float angle = Mathf.Rad2Deg*Mathf.Atan(Mathf.Abs(y)/Mathf.Abs(x));
		if(x>=0 && y>=0){
			return 90 - angle;
		}
		if(x>=0 && y<0){
			return 90 + angle;
		}
		if(x<0 && y<0){
			return 270 - angle;
		}
		if(x<0 && y>=0){
			return 270 + angle;
		}
		return -1;
	}

}