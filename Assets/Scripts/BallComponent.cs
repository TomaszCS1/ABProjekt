using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BallComponent : MonoBehaviour
{
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

    private Rigidbody2D m_rigidbody;                // RigidBody dla kulki

    private SpringJoint2D m_connectedJoint;         // field do kontrolowania polaczenia sprezynowego w kuli
    private Rigidbody2D m_connectedBody;            // RigidBody dla punktu sprezynowego kulki
    public float SlingStart = 0.5f;
    public float MaxSpringDistance = 2.9f;

    private LineRenderer m_lineRenderer;

    private Vector2 slingLineFix = new Vector2(-0.5f,0);

    private TrailRenderer m_trailRenderer;



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


        if (transform.position.x > m_connectedBody.transform.position.x + SlingStart) //jesli pozycja kuli osiagnie wieksza wartosc niz pozycja punktu sprezynowego
        {
            m_connectedJoint.enabled = false;                                         //wylacz polaczenie sprezynowe  po wystrzeleniu kuli

            m_lineRenderer.enabled = false;                                           // wylacz renderer po wystrzeleniu kuli
        }
    }



    private void OnMouseDrag()                      // 
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
        m_lineRenderer.positionCount = 3;
        m_lineRenderer.SetPositions(new Vector3[] { m_connectedBody.position+slingLineFix, transform.position, m_connectedBody.position -slingLineFix});



    }

    public bool IsSimulated()                           // przekazuje informacje o tym czy objekt jest symulowany do klasy camera_controller
    {
        return m_rigidbody.simulated;
    }


    public void WylaczJoint()
    {
        m_connectedJoint.enabled = false;
        Debug.Log("joint disabled!");
    }


    private void OnMouseUp()
    {
        m_rigidbody.simulated = true;
    }






}
