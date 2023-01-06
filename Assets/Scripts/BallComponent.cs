using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BallComponent : MonoBehaviour
{

    public float rotationSpeed = 5f;
    public Vector3 vecRotation = Vector3.forward;

    private AudioSource m_audioSource;
    public AudioClip PullSound;
    public AudioClip ShootSound;
    public AudioClip RestartSound;
    public AudioClip HitTheGroundSound;
    

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

    private Rigidbody2D m_rigidbody;                // RigidBody dla kulki
    private SpringJoint2D m_connectedJoint;         // field do kontrolowania polaczenia sprezynowego w kuli
    private Rigidbody2D m_connectedBody;            // RigidBody dla punktu sprezynowego kulki
   
    public float SlingStart = 0.5f;
  
    public float MaxSpringDistance = 2.9f;

    private LineRenderer m_lineRenderer;

    private Vector2 slingLineFix = new Vector2(-0.3f,0);

    private TrailRenderer m_trailRenderer;

    private bool m_hitTheGround = false;

    private Vector3 m_startPosition;
    private Quaternion m_startRotation;

    public bool isRestarted = false;
    public bool soundPlayed = false; 


    private void Start()
    {
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

    }



    void Update()
    {

        if (GameplayManager.Instance.Pause)
        {
            m_rigidbody.simulated = false;
        }
        else
        {
            m_rigidbody.simulated = true;
        }


        if (transform.position.x > m_connectedBody.transform.position.x + SlingStart) // jesli pozycja kuli osiagnie wieksza wartosc niz pozycja punktu sprezynowego
        {
            m_connectedJoint.enabled = false;                                         // wylacz polaczenie sprezynowe  po wystrzeleniu kuli

            m_lineRenderer.enabled = false;                                           // wylacz renderer po wystrzeleniu kuli

            m_trailRenderer.enabled = !m_hitTheGround;                                // jesli kolizja z Ground to TrailRender wylaczony

        }

        if (Input.GetKeyUp(KeyCode.R))
        {
            Restart();
            // plays Restart Sound 
            m_audioSource.PlayOneShot(RestartSound);
        }

            //Velocity of BallComponent used to move camera
            PhysicsSpeed = m_rigidbody.velocity.magnitude;

        if (m_hitTheGround && soundPlayed==false)
        {
            SoundGround();
            soundPlayed = true;
        }
       
    }

    private void OnMouseDown()
    {
        m_audioSource.PlayOneShot(PullSound);
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


        // LINE RENDERER
        SetLineRendererPoints();
       

        // informacja czy nastapila kolizja z ground domyslnie ustawiona na false 
        m_hitTheGround = false;


    }

    public bool IsSimulated()                           // przekazuje informacje o tym czy objekt jest symulowany do klasy camera_controller
    {
        return m_rigidbody.simulated;
    }



    private void OnMouseUp()
    {
        m_rigidbody.simulated = true;

        m_audioSource.PlayOneShot(ShootSound);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.gameObject.layer==LayerMask.NameToLayer("Ground"))
        { m_hitTheGround = true; }
    }

    private void Restart()
    {
        transform.position = m_startPosition;
        transform.rotation = m_startRotation;

        m_rigidbody.velocity = Vector3.zero;
        m_rigidbody.angularVelocity = 0.0f;
        m_rigidbody.simulated = true;

        m_connectedJoint.enabled = true;
        m_lineRenderer.enabled = true;
        m_trailRenderer.enabled = false;

        // Set the damping ratio to a high value to reduce oscillation
        m_connectedJoint.dampingRatio = 1.0f;

        // Set the frequency to a low value to slow down oscillation
        m_connectedJoint.frequency = 5.0f;

        SetLineRendererPoints();

        isRestarted = true;
        soundPlayed = false;



    }


    private void SetLineRendererPoints()
    {
        m_lineRenderer.positionCount = 3;
        m_lineRenderer.SetPositions(new Vector3[] { m_connectedBody.position + slingLineFix, transform.position, m_connectedBody.position - slingLineFix });
    }

    public void SoundGround()
    {
        //If ball hits ground sound will be played
        if (m_hitTheGround)
        { m_audioSource.PlayOneShot(HitTheGroundSound); }

    }


    public void WylaczJoint()
    {
        m_connectedJoint.enabled = false;
        Debug.Log("joint disabled!");
    }




}
