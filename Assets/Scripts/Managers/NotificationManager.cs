using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotificationManager : MonoBehaviour
{
     public static NotificationManager GetInstance() 
    {
        GameObject gameManager = GameObject.Find("GameManager");
        if(gameManager == null) 
        {
            Debug.LogError("GameManager has not been instantiated yet");
            return null;
        }

        NotificationManager notificationManager = gameManager.GetComponent<NotificationManager>();

        if(notificationManager == null) 
        {
            Debug.LogError("GameManager has no component NotificationManager");
            return null;
        }

        return notificationManager;
    }

    public Text notificationText;
    public Animator notificationAnimator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private Coroutine currentPresentedCoroutine;
    public void Notify(string message) 
    {
        if(currentPresentedCoroutine != null) 
        {
            StopCoroutine(currentPresentedCoroutine);
        }
        
        currentPresentedCoroutine = StartCoroutine(NotifyCoroutine(message));
    }
    
    float notificationDuration = 2;
    private IEnumerator NotifyCoroutine(string message) 
    {
        notificationAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
        notificationText.text = message;

        notificationAnimator.Play("NotificationPopIn");
        float curTime = 0;
        while(curTime < notificationDuration)
        {
            curTime += Time.unscaledDeltaTime;
            yield return null;
        }
        notificationAnimator.Play("NotificationPopOut");
    }

    
}
