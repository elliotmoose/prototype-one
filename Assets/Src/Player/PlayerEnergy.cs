using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnergy : MonoBehaviour
{
    private float _energy = 1000;
    private float _maxEnergy = 1000;
    private float _energyRegenRate = 20; // per second

    public float energy
    {
        get
        {
            return this._energy;
        }
    }
    public float maxEnergy
    {
        get
        {
            return this._maxEnergy;
        }
    }


    public void UseEnergy(float amt)
    {
        _energy = Mathf.Max(0, _energy - amt);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _energy = Mathf.Min(_maxEnergy, _energy += _energyRegenRate * Time.deltaTime);
    }
}
