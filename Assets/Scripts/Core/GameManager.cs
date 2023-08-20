using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Ideally, this will maintain any game state that persists between scenes. Then we'll use a prefab
of it on every scene to simplify setup of mechanics */

public class GameManager : MonoBehaviour
{ 
    // Singleton instantiation
    public static GameManager Instance { get; private set; }
    void Awake()
    {
        if (Instance != null && Instance !=this)
        {
            Destroy(this);
            return;
        }
        else
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }
}
