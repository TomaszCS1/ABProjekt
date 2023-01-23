using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetComponent :  InteractiveComponent
{

    //public bool m_hitThePlank = false;

    //protected Rigidbody2D m_rigidbody;

    // uses particle system "ParticleHitPlanks" from Planks
    public ParticleSystem m_particlesHitPlank;

    // uses particle system "ParticleShootEffect" from BallComponent
    public BallComponent particleSystemBall;

    private Vector3 m_startPositionPlankLeft = new Vector3(-0.3f, -1.9f, 0.0f);
    private Vector3 m_startPositionPlankRight = new Vector3(0.39f, -1.9f, 0.0f);
    private Vector3 m_startPositionPlankUpper = new Vector3(0f, -0.85f, 0.0f);
    private Quaternion m_startRotationPlankLeft = Quaternion.Euler(0,0, 90.0f);
    private Quaternion m_startRotationPlankRight = Quaternion.Euler(0,0, 90.0f);
    private Quaternion m_startRotationPlankUpper = Quaternion.Euler(0,0, 0.0f);

    public TargetComponent plankSpriteLeft;
    public TargetComponent plankSpriteRight;
    public TargetComponent plankSpriteUpper;

    //public AudioClip HitTheGroundSound;
    //private AudioSource m_audioSource;


    // START 
    public override void Start()
    {
        base.Start();

        //m_particlesHitPlank = GetComponent<ParticleSystem>();
        m_rigidbody = GetComponent<Rigidbody2D>();


        // subscribes Events from GameplayManager
        GameplayManager.OnGamePaused += DoPause;
        GameplayManager.OnGamePlaying += DoPlay;


        // przypisanie komponentu AudioSource do zmiennej m_audioSource
        m_audioSource = GetComponent<AudioSource>();

    }


    // UPDATE 
    void Update()
    {
        if (m_hitThePlank)
        {
            particleSystemBall.m_particles.Play();

            m_particlesHitPlank.Play();

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
        plankSpriteUpper.transform.localRotation = m_startRotationPlankUpper;

    }


    // this method removes functions DoPouse() and DoPlay() from Event: OnGamePaused and Event: OnGamePlaying when a Scene or game ends
    public override void OnDestroy()
    {
        //GameplayManager.OnGamePaused -= DoPause;  //moved to InteractiveComponent
        //GameplayManager.OnGamePlaying -= DoPlay;  //moved to InteractiveComponent
    }


    public override void DoPlay()
    {
        base.DoPlay();
    }


    public override void DoPause()
     {
        base.DoPause();
    }





    public  void OnCollisionEnter2D(Collision2D collision)

    {
        //collision with collision layer "Ball"
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Ball"))
        {
            PlaySoundOnColision();

            // adds point after every collision with every game object on layer Target
            GameplayManager.Instance.Points += 1;

            GameObject.Destroy(this.gameObject, 1.0f);

        }

       


    }
    
    public override void PlaySoundOnColision()
    {
        base.PlaySoundOnColision();
        
    }

}
