using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySounds : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] clips;
    public AudioClip rollSound;
    public AudioClip spellSound;
    public AudioClip specialSound;
    public AudioClip blockSound;
    public AudioClip wooshSound;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void Step(AnimationEvent animationEvent)
    {
        if (animationEvent.animatorClipInfo.weight > 0.5)
        {
            AudioClip clip = GetRandomClip();
            audioSource.PlayOneShot(clip);
        }
    }
    private void RollSound()
    {
        audioSource.PlayOneShot(rollSound);
    }
    private void WooshSound()
    {
        audioSource.PlayOneShot(wooshSound);
    }
    private void BlockSound()
    {
        audioSource.PlayOneShot(blockSound);
    }
    private void SpellSound()
    {
        audioSource.PlayOneShot(spellSound);
    }
    private void SpecialSound()
    {
        audioSource.PlayOneShot(specialSound);
    }
    private AudioClip GetRandomClip()
    {
        return clips[UnityEngine.Random.Range(0, clips.Length)];
    }








}
