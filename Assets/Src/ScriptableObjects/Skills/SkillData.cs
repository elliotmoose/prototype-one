using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill", menuName = "ScriptableObjects/SkillData", order = 2)]
[System.Serializable]
public class SkillData : ScriptableObject
{
    public string skillName = "";
    public string skillDescription = "";
    public int maxLevel = 0;
}
