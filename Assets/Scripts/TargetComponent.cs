using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetComponent :  InteractiveComponent
{

    public bool m_hitThePlank = false;

    private Rigidbody2D m_rigidbody;

    // uses particle system "ParticleHitPlanks" from Planks
    public ParticleSystem m_particlesHitPlank;

    // uses particle system "ParticleShootEffect" from BallComponent
    public BallComponent particleSystemBall;

    private Vector3 m_startPositionPlankLeft = new Vector3(-0.28f, -1.9f, 0.0f);
    private Vector3 m_startPositionPlankRight = new Vector3(0.39f, -1.88f, 0.0f);
    private Vector3 m_startPositionPlankUpper = new Vector3(0f, -0.99f, 0.0f);
    private Quaternion m_startRotationPlankLeft = Quaternion.Euler(0,0, 90.0f);
    private Quaternion m_startRotationPlankRight = Quaternion.Euler(0,0, 90.0f);

    public TargetComponent plankSpriteLeft;
    public TargetComponent plankSpriteRight;
    public TargetComponent plankSpriteUpper;



    // START 
    void Start()
    {
        //m_particlesHitPlank = GetComponent<ParticleSystem>();
        m_rigidbody = GetComponent<Rigidbody2D>();


        // subscribes Events from GameplayManager
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

    public override void DoRestart()
    {
        base.DoRestart();

        plankSpriteLeft.transform.localPosition = m_startPositionPlankLeft;
        plankSpriteLeft.transform.localRotation = m_startRotationPlankLeft;

        plankSpriteRight.transform.localPosition = m_startPositionPlankRight;
        plankSpriteRight.transform.localRotation = m_startRotationPlankRight;

        plankSpriteUpper.transform.localPosition = m_startPositionPlankUpper;



        //transform.position = m_startPositionPlankLeft;
        //transform.rotation = m_startRotation;

        m_rigidbody.velocity = Vector3.zero;
        m_rigidbody.angularVelocity = 0.0f;
        m_rigidbody.simulated = true;

        
    }


    // this method removes functions DoPouse() and DoPlay() from Event: OnGamePaused and Event: OnGamePlaying when a Scene or game ends
    public override void OnDestroy()
    {
        //GameplayManager.OnGamePaused -= DoPause;
        //GameplayManager.OnGamePlaying -= DoPlay;
    }


    public override void DoPlay()
    {
        m_rigidbody.simulated = true;
    }


    public override void DoPause()
    {
        m_rigidbody.simulated = false;
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
