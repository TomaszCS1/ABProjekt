using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class InteractiveComponent : MonoBehaviour, IRestartableObject
{
    

    public virtual void DoRestart()
    {}


    public virtual void DoPlay()
    {}


    public virtual void DoPause()
    {}

    public virtual void OnDestroy()
    {
        GameplayManager.OnGamePaused -= DoPause;
        GameplayManager.OnGamePlaying -= DoPlay;
    }

    //public virtual void OnCollisionEnter2D(Collision2D collision) { }


    public virtual void PlaySoundOnColision() { }

}
