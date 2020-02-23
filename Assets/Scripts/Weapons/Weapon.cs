using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Weapon : MonoBehaviour
{

	public Rigidbody projectile;
	public Material m1;
	public Material m2;
	public Transform FireTransform;
	Joystick attackJoystick;
	Transform weapon;
	GameObject weaponItem;
	// Projectile projectile;

    // Start is called before the first frame update
    void Start()
    {
        attackJoystick = GameObject.Find("AttackJoystick").GetComponent<Joystick>();;        
    	// weapon = ;
    	weaponItem = gameObject.transform.GetChild(0).gameObject;
    	// projectile = weaponItem.transform.GetChild(0).GetComponent<Projectile>();

    }

    // Update is called once per frame
    void Update()
    {
    	if( attackJoystick.isActive){
    		weaponItem.transform.GetComponent<Renderer>().material = m2;
    		Fire();
    	}else{
    		weaponItem.transform.GetComponent<Renderer>().material = m1;
    	}
        
    }

    void Fire(){    
    	Rigidbody projectileInstance = Instantiate(projectile, FireTransform.transform.position,FireTransform.transform.rotation) as Rigidbody;
    	projectileInstance.velocity = FireTransform.TransformDirection(Vector3.forward*10);
    }
}
