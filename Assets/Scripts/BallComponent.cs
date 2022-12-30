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

    public Vector3 vecScaleDirLeft = new Vector3(-1, 1, 1);
    public Vector3 vecScaleDirRight = new Vector3(1, 1, 1);
    public Vector3 vecScaleDirDown = new Vector3(1, -1, 1);
    public Vector3 vecDirUp = new Vector3(0, 0, -270);
    Rigidbody2D m_rigidbody;

    public bool isStopped = false;
    public int countPause = 0;

   public float PhysicsSpeed;

    public Camera _mojaCamera;

  

     
    private void OnMouseDrag()
    {

        Vector3 worldPos = _mojaCamera.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(worldPos.x *Speed, worldPos.y*Speed, 0);
    }

    private void Start()
    {
        _mojaCamera = Camera.main;
        m_rigidbody = GetComponent<Rigidbody2D>();

    }



    public bool IsSimulated()
    {
        return m_rigidbody.simulated;
    }



    private void OnMouseUp()
    {
        m_rigidbody.simulated = true;
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


        PhysicsSpeed = m_rigidbody.velocity.magnitude;
    
    }


}
