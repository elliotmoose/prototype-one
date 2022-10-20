using System.Collections.Generic;
using UnityEngine;
//weapon level
//Upgrades:
//key: Explosion Radius
//value: [0.2, 0.3, 0.4] //property for this level
//key: Explosion Damage
//value: [1.4, 3.2, 4.6] //property for this level

//attackProperties
//upgradePrice: [300, 400, 500] //amount to pay for this level
//

public class WeaponProperty 
{
    public string name = "Weapon Property";
    public string id = "WEAPON_PROPERTY";
    private List<float> _values = new List<float>(); 
    PropertyRepresentationType type = PropertyRepresentationType.RAW;

    public WeaponProperty(string id, string name, float[] value, PropertyRepresentationType type)
    {
        this.id = id;
        this.name = name;
        this._values = new List<float>(value);
        this.type = type;
    }

    public WeaponProperty(string id, string name) 
    {
        this.id = id;
        this.name = name;
    }


    //get the attack property of this level
    public float GetValueForWeaponLevel(int level)
    {
        return _values[Mathf.Min(Mathf.Max(level, 0), _values.Count-1)];
    }
    
    public List<float> GetValues()
    {
        return _values;
    }

    //set the attack property of this level
    public void SetValueForWeaponLevel(int level, float value)
    {
        if(level < _values.Count) 
        {
            _values[level] = value;
        }
        else 
        {
            _values.Add(value);
        }
    }

    public void AddValue(float value)  
    {
        _values.Add(value);
    }
}

//decides if 0.3 should be displayed as 0.3 or 30%
public enum PropertyRepresentationType 
{
    RAW,
    PERCENTAGE,
    SPLIT
}
