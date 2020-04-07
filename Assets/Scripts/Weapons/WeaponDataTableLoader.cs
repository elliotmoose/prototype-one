using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDataTableLoader : MonoBehaviour {
    public TextAsset weaponDataTableFile;
    void Awake() 
    {
        string weaponDataTableContent = weaponDataTableFile.ToString();
        Debug.Log(weaponDataTableContent);
    }
}