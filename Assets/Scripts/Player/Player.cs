using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject moveJoystick;
    public float speed = 0.3f;
    // Start is called before the first frame update
    void Start()
    {
        Joystick joystick = moveJoystick.GetComponent<Joystick>();
        joystick.joystickMovedEvent += updatePlayerPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void updatePlayerPosition(float angle)
    {
        Vector3 position = this.transform.position;
        Vector3 delta = Quaternion.AngleAxis(angle, Vector3.up) * Vector3.forward;
        this.transform.position += delta * Time.deltaTime;
    }
}
