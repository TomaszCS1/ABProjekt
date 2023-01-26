using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TargetComponent :  InteractiveComponent
{

    //public bool m_hitThePlank = false;

    //private Rigidbody2D m_rigidbody; /* field moved to interactive component, cannot be serialised in this class*/

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

    public GameSettingsDatabase GameDatabase;

    public GameObject PrefabRef;

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

        ////if(plankSpriteLeft != null)
        ////{

        plankSpriteLeft.transform.localPosition = m_startPositionPlankLeft;
        plankSpriteLeft.transform.localRotation = m_startRotationPlankLeft;
        //}

        //if(plankSpriteRight != null)
        //{
        plankSpriteRight.transform.localPosition = m_startPositionPlankRight;
        plankSpriteRight.transform.localRotation = m_startRotationPlankRight;
        //}

        //if(plankSpriteUpper != null)
        //{
        plankSpriteUpper.transform.localPosition = m_startPositionPlankUpper;
        plankSpriteUpper.transform.localRotation = m_startRotationPlankUpper;

        
        //GameObject.Instantiate(GameDatabase.TargetPrefab, new Vector3(7.5f, 7.0f, 0.0f), Quaternion.identity);
   



    }


    public override void OnDestroy()
    {
        base.OnDestroy();
      
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

            // destroy planks after collision
            //GameObject.Destroy(this.gameObject);

        }

       


    }
    
    public override void PlaySoundOnColision()
    {
        base.PlaySoundOnColision();
        
    }

}
