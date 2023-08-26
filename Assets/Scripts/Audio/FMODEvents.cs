using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using FMODUnity;
using System.CodeDom;

public class FMODEvents : MonoBehaviour
{

    [field: Header("Music")]
    [field:SerializeField] public EventReference music { get; private set; }

    [field: Header("Footsteps SFX")]

    [field:SerializeField] public EventReference playerFootsteps { get; private set; }

    [field: Header("Pickup SFX")]

    [field: SerializeField] public EventReference itemPickup { get; private set; }


    public static FMODEvents instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            UnityEngine.Debug.LogError("Found more than one FMOD Events instance in the scene.");
        }
        instance = this;
    }
}
