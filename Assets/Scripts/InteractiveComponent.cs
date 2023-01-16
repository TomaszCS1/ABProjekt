using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class InteractiveComponent : MonoBehaviour, IRestartableObject
{
    public Rigidbody2D m_rigidbody;

    public AudioClip HitTheGroundSound;
    public AudioSource m_audioSource;
    public bool m_hitThePlank = false;


    public virtual void Start()
    {
        // subscribes Events from GameplayManager
        GameplayManager.OnGamePaused += DoPause;
        GameplayManager.OnGamePlaying += DoPlay;
    }

    public virtual void DoRestart()
    {
        m_rigidbody.velocity = Vector3.zero;
        m_rigidbody.angularVelocity = 0.0f;
        m_rigidbody.simulated = true;
    }


    public virtual void DoPlay()
    {
        m_rigidbody.simulated = true;
    }


    public virtual void DoPause()
    {
        m_rigidbody.simulated = false;
    }

    public virtual void OnDestroy()
    {
        GameplayManager.OnGamePaused -= DoPause;
        GameplayManager.OnGamePlaying -= DoPlay;
    }


    public virtual void PlaySoundOnColision() 
    {
        m_audioSource.PlayOneShot(HitTheGroundSound);

        m_hitThePlank = false;

    }

}
