using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SkillItemController
{
    SkillData skillData;
    VisualElement element;

    public SkillItemController(VisualElement el)
    {
        this.element = el;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetSkillData(SkillData skillData)
    {
        this.skillData = skillData;
        this.element.Q<Label>("skill-name-label").text = this.skillData.skillName.ToUpper();
        this.element.Q<Label>("skill-description-label").text = this.skillData.skillDescription;
        Debug.Log(this.element);
        Debug.Log(this.element.Q<Label>("skill-name-label").text);
    }
}
