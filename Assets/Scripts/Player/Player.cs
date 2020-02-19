using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float dnaAmount = 0;
    public void AddDna(float amount) {
        dnaAmount += amount;
    }
   
    public GameObject moveJoystick;
    public GameObject attackJoystick;
    private float speed = 4f;
    // Start is called before the first frame update
    void Start()
    {
        Joystick joystickMove = moveJoystick.GetComponent<Joystick>();
        Joystick joystickAttack = attackJoystick.GetComponent<Joystick>();
        joystickMove.joystickMovedEvent += UpdatePlayerPosition;
        joystickAttack.joystickMovedEvent += RotatePlayer;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void UpdatePlayerPosition(float angle)
    {
        Vector3 position = this.transform.position;
        Quaternion rotation = this.transform.rotation;
        Vector3 delta = Quaternion.AngleAxis(angle, Vector3.up) * Vector3.forward;
        this.transform.position += delta * Time.deltaTime * speed;
        this.transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);

    }

    private void RotatePlayer(float angle)
    {
        Quaternion rotation = this.transform.rotation;
        this.transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
    }
}
