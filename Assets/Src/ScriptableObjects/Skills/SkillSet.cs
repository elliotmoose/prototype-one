using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill", menuName = "ScriptableObjects/SkillSet", order = 2)]
[System.Serializable]
public class SkillSet : ScriptableObject
{
    public SkillData[] skills;
}
