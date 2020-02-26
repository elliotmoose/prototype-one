using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collider) {
        if(CanPickUp(collider.gameObject, this)) {
            OnPickUp(collider.gameObject, this);            
        }
    }

    public virtual bool CanPickUp(GameObject picker, Item item) {
        return true;
    }

    public virtual void OnPickUp(GameObject picker, Item item) {
        GameObject.Destroy(this.gameObject);
    }
}
