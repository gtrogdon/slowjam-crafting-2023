using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Actor
{
    // Singleton Instantiation
    public static Player Instance { get; private set; }
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        else
        {
            Instance = this;
        }
    }

    new void Start()
    {
        // Execute parent's start class
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
