using System;
using UnityEngine;
using UnityEngine.Events;

public class IntroAnimationController : MonoBehaviour
{
    [NonSerialized]
    public UnityEvent IntroAnimationCompleteEvent;

    private void Start()
    {
        IntroAnimationCompleteEvent = new UnityEvent();    
    }

    public void IntroAnimationComplete()
    {
        IntroAnimationCompleteEvent.Invoke();
    }
}
