using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void AnimationReceiverEvent(string key);

public class AnimationReceiver : MonoBehaviour
{
    public AnimationReceiverEvent OnAnimationBegin;
    public AnimationReceiverEvent OnAnimationCommit;
    public AnimationReceiverEvent OnAnimationExecute;
    public AnimationReceiverEvent OnAnimationEnd;
    
    public void AnimationBegin(string key) 
    {
        if(OnAnimationBegin != null) 
        {
            OnAnimationBegin(key);
        }        
    }

    public void AnimationCommit(string key) 
    {
        if(OnAnimationCommit != null) 
        {
            OnAnimationCommit(key);
        }
    }

    public void AnimationExecute(string key) 
    {
        if(OnAnimationExecute != null) 
        {
            OnAnimationExecute(key);
        }
    }
    public void AnimationEnd(string key) 
    {
        if(OnAnimationEnd != null) 
        {
            OnAnimationEnd(key);
        }
    }
}
