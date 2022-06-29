using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private AnimationClip clip;
    [SerializeField] private float eventTime;


    private void Start()
    {
        clip.AddAnimationEvent(eventTime, "EventFunction");
    }

    private void EventFunction2()
    {
        Debug.Log("ja pierdole dziala");
    }
}
