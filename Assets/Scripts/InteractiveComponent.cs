using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class InteractiveComponent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //// subscribes Events from GameplayManager
        //GameplayManager.OnGamePaused += DoPause;
        //GameplayManager.OnGamePlaying += DoPlay;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void DoRestart()
    {}


    private void DoPlay()
    {}


    private void DoPause()
    {}

}
