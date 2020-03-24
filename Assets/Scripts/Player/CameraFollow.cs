
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    
    private Player player;
    private Vector3 velocity = Vector3.zero;
    private Vector3 offset;

    void Start()
    {
        player = Player.GetInstance();
        offset = new Vector3(0, 8.59f, -3.57f) * 1.4f;
    }

    public float smoothSpeed = 0.125f;

    private void LateUpdate()
    {
        if (player != null)
        {
            Vector3 desiredPosition = player.transform.position + offset;
            // Vector3 smoothPosition = Vector3.Lerp(this.transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            desiredPosition.x = Mathf.Clamp(desiredPosition.x, -43f, 43f);
            desiredPosition.z = Mathf.Clamp(desiredPosition.z, -49f, 43f);

            float smoothTime = 0.3f;
            this.transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothTime);;
        }
        else 
        {
            player = Player.GetInstance();
            Debug.LogWarning("Camera follow: Cannot find player");
        }
        
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
