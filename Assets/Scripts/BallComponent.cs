using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Text;
using Unity.Mathematics;
using UnityEngine;

public class BallComponent : InteractiveComponent
{
    //protected Rigidbody2D m_rigidbody; /*field moved to interactive component, cannot be serialised in this class 

    public float rotationSpeed = 5f;
    public Vector3 vecRotation = Vector3.forward;


    public enum BallInstruction
    {
        Idle = 0,
        MoveUp,
        MoveDown,
        MoveLeft,
        MoveRight,
        ScaleUp,
        ScaleDown,
    }

  
    public float Speed = 1.0f;

    public List<BallInstruction> Instructions = new List<BallInstruction>();

    public Vector2 vecScale = Vector2.one;
   
    public float scaleSpeed = 2.0f;
    public bool isScaleTwo=false;
    public int countPause = 0;
    public float PhysicsSpeed;
    public Camera _mojaCamera;

 
    private SpringJoint2D m_connectedJoint;         // field holds reference to component SprintJoint2D
    private Rigidbody2D m_connectedBody;            // field holds reference to connected Rigid Body in component SprintJoint2D

    public float SlingStart = 0.5f;
  
    public float MaxSpringDistance = 2.9f;

    private LineRenderer m_lineRenderer;

    private Vector2 slingLineFix = new Vector2(-0.3f,0);

    private TrailRenderer m_trailRenderer;

    private bool m_hitTheGround = false;

    private Vector3 m_startPosition;
    private Quaternion m_startRotation;

    public bool isRestarted = false;
    public bool wasGroundSoundPlayed;

    private Animator m_animator;

    public ParticleSystem m_particles;

    public ParticleSystem m_particleAtraktor;

    public bool wasBallOnGround =false;

    //this field holds reference to game settings (to class GameSettingsDatabase)
    public GameSettingsDatabase GameDatabase;

   
    

    // START
    public override void Start()
    {
        base.Start();
         
        _mojaCamera = Camera.main;

        //GetComponent return reference to <Component>
        m_rigidbody = GetComponent<Rigidbody2D>();      

        m_connectedJoint = GetComponent<SpringJoint2D>();
        m_connectedBody = m_connectedJoint.connectedBody;

        // Set the damping ratio to a high value to reduce oscillation
        m_connectedJoint.dampingRatio = 1.0f;

        // Set the frequency to a low value to slow down oscillation
        m_connectedJoint.frequency = 5.0f;

        m_lineRenderer = GetComponent<LineRenderer>();
        m_trailRenderer = GetComponent<TrailRenderer>();

        m_trailRenderer.enabled = false;

        m_startPosition = transform.position;
        m_startRotation = transform.rotation;

        m_audioSource = GetComponent<AudioSource>();

        m_animator = GetComponentInChildren<Animator>();

        m_particles = GetComponentInChildren<ParticleSystem>();

        m_particleAtraktor = GetComponentInChildren<ParticleSystem>();

        StartCoroutine(SpringJointCoroutine());

    }

    IEnumerator SpringJointCoroutine()
    {
        while (true)
        {
            //Debug.Log("Frame: " + Time.frameCount);

        
                if (Time.frameCount % 2 == 0)
                {
                    if (transform.position.x > m_connectedBody.transform.position.x + SlingStart)
                    {
                        m_connectedJoint.enabled = false;
                        //yield return null;
                        //Debug.Log("joint2D disabled: ");
                    }

                       
                }

            yield return new WaitUntil( ()=>(Time.frameCount % 2 == 0));

        }
    }


    // UPDATE
    void Update()
    {

        if (transform.position.x > m_connectedBody.transform.position.x + SlingStart) // jesli pozycja kuli osiagnie wieksza wartosc niz pozycja punktu sprezynowego
        {
            /*m_connectedJoint.enabled = false;      */                                   // wylacz polaczenie sprezynowe  po wystrzeleniu kuli

            m_lineRenderer.enabled = false;                                           // wylacz renderer po wystrzeleniu kuli

            m_trailRenderer.enabled = !m_hitTheGround;                                // jesli kolizja z Ground to TrailRender wylaczony

        }

      
        //Velocity of BallComponent used to move camera
        PhysicsSpeed = m_rigidbody.velocity.magnitude;

    }


 

        public void OnCollisionEnter2D(Collision2D collision)

    {
        //collision with collision layer "Ground"
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            PlaySoundOnColision();
        }

        //calls animation
        m_animator.enabled = true;

        // 0 is the number of animation layer, (default value = 0)
        m_animator.Play(0);

    }


    public override void PlaySoundOnColision()
    {
        base.PlaySoundOnColision();
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



    private void OnMouseDown()
    {
        m_audioSource.PlayOneShot(clip:  GameDatabase.PullSound);
    }

    private void OnMouseDrag()
    {
        // CHANGES JOINT COMPONENT BACK AFTER MOUSE DRAG TO IMPROVE FLYING
        m_connectedJoint.dampingRatio = 0.0f;
        m_connectedJoint.frequency = 1.0f;


        Vector3 worldPos = _mojaCamera.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(worldPos.x * Speed, worldPos.y * Speed, 0);

        Vector2 newBallPos = new Vector3(worldPos.x, worldPos.y);
        float CurJointDistance = Vector3.Distance(newBallPos, m_connectedBody.position);

        if (CurJointDistance > MaxSpringDistance)
        {
            Vector2 direction = (newBallPos - m_connectedBody.position).normalized;
            transform.position = m_connectedBody.position + direction * MaxSpringDistance;
        }
        else
        {
            transform.position = newBallPos;
        }

        // Line Renderer
        SetLineRendererPoints();
       

        // contains information if game object hit ground 
        m_hitTheGround = false;

    }

    // returns the information if game object is currently simulated, used in CameraControl
    public bool IsSimulated()  
    {
        return m_rigidbody.simulated;
    }


    // function initiated after releasing LMB -> shoots BallComponent
    private void OnMouseUp()
    {
        AudioClip m_shotSound = GameDatabase.ShootSound;

        m_rigidbody.simulated = true;

        m_audioSource.PlayOneShot(m_shotSound);

        m_particles.Play();


    }

 
    public override void DoRestart()
    {
        base.DoRestart();

        transform.position = m_startPosition;
        transform.rotation = m_startRotation;

        m_connectedJoint.enabled = true;
        m_lineRenderer.enabled = true;
        m_trailRenderer.enabled = false;

        // Set the damping ratio to a high value to reduce oscillation
        m_connectedJoint.dampingRatio = 1.0f;

        // Set the frequency to a low value to slow down oscillation
        m_connectedJoint.frequency = 5.0f;

        SetLineRendererPoints();

        // plays Restart Sound 
        m_audioSource.PlayOneShot(GameDatabase.RestartSound);

        isRestarted = true;

        //reset the attributes to play HitGroundSound in Update()
        m_hitTheGround=false;
        wasGroundSoundPlayed = false;


    }


    private void SetLineRendererPoints()
    {
        m_lineRenderer.positionCount = 3;
        m_lineRenderer.SetPositions(new Vector3[] { m_connectedBody.position + slingLineFix, transform.position, m_connectedBody.position - slingLineFix });
    }


   ////example of a method which changes property of Joint component
   // public void WylaczJoint()
   // {
   //     m_connectedJoint.enabled = false;
   //     Debug.Log("joint disabled!");
   // }




}
