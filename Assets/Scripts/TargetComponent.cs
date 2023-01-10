using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetComponent : MonoBehaviour
{

    public bool m_hitThePlank = false;


    // uses particle system "ParticleHitPlanks" from Planks
    public ParticleSystem m_particlesHitPlank;

    // uses particle system "ParticleShootEffect" from BallComponent
    public BallComponent particleSystemBall;

    // Start is called before the first frame update
    void Start()
    {
        //m_particlesHitPlank = GetComponent<ParticleSystem>();

    }

    // Update is called once per frame
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //collision with collision layer "Ball"
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Ball"))
        { 
            m_hitThePlank = true;

        }

    }  


    }
