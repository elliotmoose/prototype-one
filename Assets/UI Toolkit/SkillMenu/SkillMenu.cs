using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SkillMenu : MonoBehaviour
{
    [SerializeField]
    public VisualTreeAsset skillItemTemplate;
    int curTabIndex = 0;

    // menu is player dependent
    public Player targetPlayer;

    private VisualElement root;
    // Start is called before the first frame update
    void Start()
    {
        targetPlayer.playerControls.Player.OpenMenuSkill.performed += (ctx) =>
        {
            if (root.visible)
            {
                root.visible = false;
                Time.timeScale = 1;
            }
            else
            {
                root.visible = true;
                Time.timeScale = 0;
            }
        };
    }

    // Update is called once per frame
    void Update()
    {

    }

    private SkillData[] GetSkillsForTierIndex(int index)
    {
        return targetPlayer.GetComponent<PlayerSkills>().skillSet.skills;
    }

    public void OnEnable()
    {
        UIDocument menu = GetComponent<UIDocument>();
        root = menu.rootVisualElement;

        UpdateSkillDisplay();


        UQueryBuilder<Label> tabs = root.Query<Label>(className: "tab");
        tabs.ForEach((tab) =>
        {
            tab.RegisterCallback<ClickEvent>((evt) =>
            {
                // update selected tab
                curTabIndex = tabs.ToList().IndexOf(tab);
                UpdateSelectedTab();
                UpdateSkillDisplay();
            });
        });
    }

    void UpdateSkillDisplay()
    {
        var skillScrollView = root.Q<ScrollView>("skill-scroll-view");
        if (skillScrollView == null)
        {
            Debug.LogWarning("Could not find scroll view");
            return;
        }
        var skills = GetSkillsForTierIndex(curTabIndex);

        // 1. delete children in scroll view
        // 2. create items
        // 3. register callbacks
        // 4. add to scroll view

        // 1. delete children in scroll view
        skillScrollView.Clear();

        // 2. create items
        foreach (var skill in skills)
        {
            var element = skillItemTemplate.Instantiate();
            var controller = new SkillItemController(element);
            controller.SetSkillData(skill);
            element.userData = controller;
            // 3. register callbacks
            // 4. add to scroll view
            skillScrollView.Add(element);
            Debug.Log($"skill added: {skill.skillName}");
        }
    }

    void UpdateSelectedTab()
    {
        UQueryBuilder<Label> tabs = root.Query<Label>(className: "tab");
        // update selected tab style
        for (int i = 0; i < tabs.ToList().Count; i++)
        {
            var tab = tabs.ToList()[i];
            if (i == curTabIndex)
            {
                tab.AddToClassList("tab-selected");
            }
            else
            {
                tab.RemoveFromClassList("tab-selected");
            }
        }
    }
}
