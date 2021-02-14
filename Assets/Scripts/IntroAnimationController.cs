using System;
using System.Collections;
using System.Collections.Generic;
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
