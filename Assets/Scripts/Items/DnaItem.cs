using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DnaItem : Item
{
    private float timeTillDisappear = 20f; //10 seconds till the item disappears
    
    private float _worth = 20;
    [SerializeField]
    private float _yOffset = 0.7f; //how high above ground it floats
    [SerializeField]
    private float _oscillateAmplitude = 0.25f; //how far it oscillates
    [SerializeField]
    private float _oscillateFrequency = 0.7f; //how fast it oscillates

    private float _curOscAngle = 0;

    public void SetWorth(float worth) {
        this._worth = worth;
    }
    
    override public bool CanPickUp(GameObject picker, Item item) {
        //can pickup if not null
        return picker.GetComponent<Player>() != null;
    }
    override public void OnPickUp(GameObject picker, Item item) {
        Player player = picker.GetComponent<Player>();
        if(player) {
            player.AddDna(_worth);
        }

        base.OnPickUp(picker, item); //cleanup
    }    

    void Update() {
        //get model (we only want to animate the model, not the parent collider)
        Transform modelTransform = transform.GetChild(0).transform;
        _curOscAngle += Time.deltaTime * Mathf.PI * _oscillateFrequency;        
        float y = _oscillateAmplitude * Mathf.Sin(_curOscAngle) + _yOffset;
        modelTransform.position = new Vector3(modelTransform.position.x, y, modelTransform.position.z);

        //item expiration
        timeTillDisappear -= Time.deltaTime;
        if(timeTillDisappear <= 0) {
            Expire();
        }
    }

    void Expire() {
        GameObject.Destroy(this.gameObject);
    }
}
