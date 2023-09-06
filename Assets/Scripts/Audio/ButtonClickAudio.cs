using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class ButtonClickAudio : MonoBehaviour
{
    [SerializeField] private EventReference soundEvent;

    public void PlaySoundEvent()
    {
        AudioManager.instance.PlayOneShot(soundEvent);
    }
}
