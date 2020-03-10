using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DnaItem : MonoBehaviour
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

    float magnetRange = 4f;
    float maxSpeed = 15f;
    float pickupRangeThreshold = 0.7f; //within this threshold the item will be picked up
    GameObject magnetTarget; //magnet to

    void OnTriggerEnter(Collider collider) {
        if(CanPickUp(collider.gameObject)) {
            magnetTarget = collider.gameObject;
        }
    }

    void OnTriggerExit(Collider collider) {
        if(collider.gameObject == magnetTarget) {
            magnetTarget = null;         
        }
    }

    void Start() {
        //set magnet range
        this.GetComponent<SphereCollider>().radius = magnetRange;
    }

    void Update() {
        if(magnetTarget != null) {
            Vector3 toTarget = (magnetTarget.transform.position - this.transform.position);
            float speed = Mathf.Pow(((magnetRange - toTarget.magnitude)/magnetRange), 3) * maxSpeed;
            this.transform.position += toTarget.normalized * speed * Time.deltaTime;

            if(toTarget.magnitude <= pickupRangeThreshold) {
                OnPickUp(magnetTarget);            
            }
        }

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

    public void SetWorth(float worth) {
        this._worth = worth;
    }
    
    public bool CanPickUp(GameObject picker) {
        //can pickup if not null
        return picker.GetComponent<Player>() != null;
    }

    public void OnPickUp(GameObject picker) {
        Player player = picker.GetComponent<Player>();
        if(player) {
            player.AddDna(_worth);
        }

        Expire();
    }    
    void Expire() {
        GameObject.Destroy(this.gameObject);
    }
}
