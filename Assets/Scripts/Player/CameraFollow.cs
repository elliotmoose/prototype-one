
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    
    public Player player;
    private Vector3 velocity = Vector3.one;
    private Vector3 offset;
    float minPosition = -47f;
    float maxPosition = 47f;

    void Start()
    {
        offset = new Vector3(0, 8.59f, -3.57f);
    }

    public float smoothSpeed = 0.125f;

    private void LateUpdate()
    {
        Vector3 desiredPosition = player.transform.position + offset;
        // Vector3 smoothPosition = Vector3.Lerp(this.transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        desiredPosition.x = Mathf.Clamp(desiredPosition.x, -43f, 43f);
        desiredPosition.z = Mathf.Clamp(desiredPosition.z, -49f, 43f);
        this.transform.position = desiredPosition;
    }
    /*
    void SmoothFollow()
    {

        Vector3 toPos = player.targetPosition + offset;
        toPos.x = Mathf.Clamp(toPos.x, minPosition, maxPosition);
        toPos.z = Mathf.Clamp(toPos.z, minPosition, maxPosition);
        Vector3 curPos = Vector3.SmoothDamp(player.transform.position + offset, toPos, ref velocity, 10f);
        this.transform.position = curPos;
    }*/

}
