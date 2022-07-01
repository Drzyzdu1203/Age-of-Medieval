using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Example : MonoBehaviour
{
    public void Start()
    {
        // existing components on the GameObject
        AnimationClip clip;
        Animator anim;

        // new event created
        AnimationEvent evt;
        evt = new AnimationEvent();

        // put some parameters on the AnimationEvent
        //  - call the function called PrintEvent()
        //  - the animation on this object lasts 2 seconds
        //    and the new animation created here is
        //    set up to happen 1.3s into the animation
        evt.intParameter = 12345;
        evt.time = 1.3f;
        evt.functionName = "PrintEvent";

        // get the animation clip and add the AnimationEvent
        anim = GetComponent<Animator>();
        clip = anim.runtimeAnimatorController.animationClips[1];
        clip.AddEvent(evt);
    }

    // the function to be called as an event
    public void PrintEvent(int i)
    {
        print("PrintEvent: " + i + " called at: " + Time.time);
    }
}
