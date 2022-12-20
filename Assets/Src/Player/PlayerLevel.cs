using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevel : MonoBehaviour
{
    private float _exp = 0;
    public float exp
    {
        get
        {
            return _exp;
        }
    }
    public float maxExp
    {
        get
        {
            return level * 1000;
        }
    }
    public float level = 1;

    public int skillPoints = 0;

    public void AddExp(float exp)
    {
        this._exp += exp;
        if (this._exp > maxExp)
        {
            this._exp -= maxExp;
            this.LevelUp();
        }
    }

    private void LevelUp()
    {
        this.level++;
        this.skillPoints++;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
