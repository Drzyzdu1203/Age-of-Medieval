using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySounds : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] Clips;
    [SerializeField]
    private AudioClip whoosh;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Damage()
    {
        AudioClip clip = GetRandomClip();
        audioSource.PlayOneShot(clip);
    }
    private void Whoosh()
    {
        
        audioSource.clip = whoosh;
        audioSource.PlayOneShot(whoosh);
    }

    private AudioClip GetRandomClip()
    {
        return Clips[UnityEngine.Random.Range(0, Clips.Length)];
    }





}
