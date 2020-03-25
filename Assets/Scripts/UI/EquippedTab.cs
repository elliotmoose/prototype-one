using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquippedTab : MonoBehaviour {
	
	private Image[] _tabWeaponImage = new Image[2];
	private Sprite[] _spt;

	void Start(){
		_spt = Resources.LoadAll<Sprite>("Sprites/WeaponsSprite");
		_tabWeaponImage[0] = GameObject.Find("Weapon1Image").GetComponent<Image>();
		_tabWeaponImage[1] = GameObject.Find("Weapon2Image").GetComponent<Image>();
	}

	void Update(){
		UpdateWeaponImage();
	}

	public void UpdateWeaponImage(){
		Player player = Player.GetInstance();
		for(int i = 0; i < 2; i ++){
        	WeaponData selectedEquipWeapon = player.GetActiveWeaponAtIndex(i);
        	if(selectedEquipWeapon != null){
        		// Debug.Log(selectedEquipWeapon.name);
        		_tabWeaponImage[i].sprite = selectedEquipWeapon.weaponSprite;
        	}
		}
	}
}