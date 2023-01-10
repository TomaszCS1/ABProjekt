using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetComponent : MonoBehaviour
{

    public bool m_hitThePlank = false;

    private Rigidbody2D m_rigidBody;

    // uses particle system "ParticleHitPlanks" from Planks
    public ParticleSystem m_particlesHitPlank;

    // uses particle system "ParticleShootEffect" from BallComponent
    public BallComponent particleSystemBall;




    // START 
    void Start()
    {
        //m_particlesHitPlank = GetComponent<ParticleSystem>();
        m_rigidBody = GetComponent<Rigidbody2D>();

        GameplayManager.OnGamePaused += DoPause;
        GameplayManager.OnGamePlaying += DoPlay;
    }




    // UPDATE 
    void Update()
    {
        if (m_hitThePlank)
        {
            particleSystemBall.m_particles.Play();
            //particleSystemBall.m_particles.Stop();

            m_particlesHitPlank.Play();
            //m_particlesHitPlank.Stop();


            m_hitThePlank = false;

        }
    }

    void OnDestroy()
    {
        GameplayManager.OnGamePaused -= DoPause;
        GameplayManager.OnGamePlaying -= DoPlay;
    }


    private void DoPlay()
    {
        m_rigidBody.simulated=true;
    }


    private void DoPause()
    {
        m_rigidBody.simulated = false;
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        //collision with collision layer "Ball"
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Ball"))
        { 
            m_hitThePlank = true;

        }

    }  


    }
