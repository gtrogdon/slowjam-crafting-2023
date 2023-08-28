using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class CraftAudio : MonoBehaviour
{
    [SerializeField]
    [EventRef]


    private string soundEvent = null;

    public void PlaySoundEvent()
    {
        if (soundEvent != null)
        {
            RuntimeManager.PlayOneShot(soundEvent);
        }
    }
}
