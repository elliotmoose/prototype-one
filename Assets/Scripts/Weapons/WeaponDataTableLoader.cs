using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDataTableLoader : MonoBehaviour {
    public TextAsset weaponDataTableFile;
    public static Dictionary<string, Dictionary<string, WeaponProperty>> templateWeaponProperties = new Dictionary<string, Dictionary<string, WeaponProperty>>();
    private static bool loaded = false;
    void Awake() 
    {
        if(loaded) 
        {
            return;
        }

        string weaponDataTableContent = weaponDataTableFile.ToString();
        string[] rows =  weaponDataTableContent.Split('\n');
        
        Dictionary<string, List<float>> cellByColumns = new Dictionary<string, List<float>>();
        
        Dictionary<string, WeaponProperty> currentWeaponProperties = null;
        if(rows.Length != 0) 
        {
            for(int i=1; i<rows.Length; i++) 
            {
                string row = rows[i];
                string[] cells = row.Split(',');

                WeaponProperty currentWeaponProperty = null;

                for(int j=0; j<cells.Length; j++) 
                {
                    string cell = cells[j].Trim();
                    if(cell != "") 
                    {
                        if(j == 0) 
                        {
                            switch (cell)
                            {
                                //found weapon key
                                case "STANDARD":
                                case "FLAMETHROWER":
                                case "MISSILE":
                                case "LASER":
                                    currentWeaponProperties = new Dictionary<string, WeaponProperty>();
                                    WeaponDataTableLoader.templateWeaponProperties.Add(cell, currentWeaponProperties);                  
                                    Debug.Log("Found Weapon:" + cell);
                                    break;        
                                //found property key                        
                                default:                               
                                    Debug.Log($"Found property {cell}");
                                    currentWeaponProperty = new WeaponProperty(cell, FormatPropertyName(cell));        
                                    currentWeaponProperties[cell] = currentWeaponProperty;                            
                                    break;
                            }
                        }
                        else if(currentWeaponProperty != null) 
                        {
                            Debug.Log($"Found value {cell} for property {currentWeaponProperty.name}");
                            currentWeaponProperty.AddValue(float.Parse(cell));
                        }                        
                    }
                    else 
                    {
                        //if we've reached an empty cell in this row just go next
                        break;
                    }
                }            
            }
        }
        else 
        {
            Debug.LogWarning("Table has no rows");
        }        

        loaded = true; ///only need to load once
        // Debug.Log(WeaponDataTableLoader.templateWeaponProperties["STANDARD"]["BULLET_SPLIT"].GetValues());
    }

    //SOME_NAME => Some Name
    private string FormatPropertyName(string name) 
    {
        if(name.Length == 0) 
        {
            return "";
        }

        string[] components = name.Split('_');
        
        for (int i = 0; i < components.Length; i++)
        {
            string s = components[i];
            s = s.Substring(0,1).ToUpper() + s.Substring(1).ToLower();
            components[i] = s;
        }

        return string.Join(" ", components);
    }
}