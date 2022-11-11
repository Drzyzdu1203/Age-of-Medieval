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
    public AudioClip damageSound;
    public AudioClip killChanceSound;
    public AudioClip riposteSound;
    public AudioClip riposteSuccesSound;
    public AudioClip riposteSuccesSoundEnd;
    public AudioSource audioSource;
    

    private void Start()
    {
        
    }
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        
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
    private void KillChanceSound()
    {
        audioSource.PlayOneShot(killChanceSound);
    }
    private void RiposteSound()
    {
        audioSource.PlayOneShot(riposteSound);
    }
    private void RiposteSuccesSound()
    {
        audioSource.PlayOneShot(riposteSuccesSound);
    }
    private void RiposteSuccesSoundEnd()
    {
        audioSource.PlayOneShot(riposteSuccesSoundEnd);
    }
    private void SpellSound()
    {
        audioSource.PlayOneShot(spellSound);
    }
    private void SpecialSound()
    {
        audioSource.PlayOneShot(specialSound);
    }
    private void DamageSound()
    {
        audioSource.PlayOneShot(damageSound);
    }
    private void StopSound()
    {
        audioSource.Stop();
    }
    private AudioClip GetRandomClip()
    {
        return clips[UnityEngine.Random.Range(0, clips.Length)];
    }








}
