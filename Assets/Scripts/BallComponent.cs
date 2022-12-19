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

    private int CurrentInstruction = 0;

    public float Speed = 1.0f;

    public List<BallInstruction> Instructions = new List<BallInstruction>();

    private float InstructionLength = 2.0f;

    float distInInstruction = 0;

    public Vector2 vecScale = Vector2.one;
    //public float scaleIncrement;
    //public float scaleUpperLimit = 3.0f;
    public float scaleSpeed = 2.0f;
    public bool isScaleTwo=false;

    public Vector3 vecScaleDirLeft = new Vector3(-1, 1, 1);
    public Vector3 vecScaleDirRight = new Vector3(1, 1, 1);
    public Vector3 vecScaleDirDown = new Vector3(1, -1, 1);
    public Vector3 vecDirUp = new Vector3(0, 0, -270);
    Rigidbody2D m_rigidbody;

    public bool isStopped = false;
    public int countPause = 0;

    private void OnMouseEnter()
    {
        Debug.Log("Mouse entering over object");
    }

    private void OnMouseExit()
    {
        Debug.Log("Mouse leaving object");
    }

    private void OnMouseDrag()
    {
        if (GameplayManager.Instance.Pause)
            return;

        m_rigidbody.simulated = false;

        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(worldPos.x, worldPos.y, 0);
    }

    private void Start()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnMouseUp()
    {
        m_rigidbody.simulated = true;
    }


    private void OnPauseStop()
    {
        m_rigidbody.simulated = false;
    }

    private void OnPauseGo()
    {
        m_rigidbody.simulated = true;
    }



    // Update is called once per frame
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




        //Debug.Log("Mouse position: " + Input.mousePosition);

        //if (Input.GetMouseButtonDown(0))
        //    Debug.Log("Left mouse button has been pressed");

        //Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Debug.Log("Mouse position: " + Input.mousePosition);
        //Debug.Log("Mouse in world position: " + worldPos);




        //if (Input.GetKeyDown(KeyCode.UpArrow))
        //{
        //    transform.position += new Vector3(0, 1, 0) * Time.deltaTime;
        //    //transform.rotation = Quaternion.Euler(vecDirUp);
        //}

        //if (Input.GetKeyDown(KeyCode.DownArrow))
        //{
        //    transform.position -= new Vector3(0, 1, 0) * Time.deltaTime;
        //    transform.localScale = vecScaleDirDown;
        //}


        //  if (Input.GetKeyDown(KeyCode.LeftArrow))
        //{
        //    transform.position -= new Vector3(1, 0, 0) * Time.deltaTime;
        //    transform.localScale = vecScaleDirLeft;
        //}


        //if (Input.GetKeyDown(KeyCode.RightArrow))
        //{ 
        //    transform.position += new Vector3(1, 0, 0) * Time.deltaTime;
        //    transform.localScale = vecScaleDirRight; 
        //}



        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    vecScale.x *= scaleSpeed ;
        //    vecScale.y *= scaleSpeed ;
        //    transform.localScale = vecScale;
        //    isScaleTwo = true;
        //}


        //if (isScaleTwo & Input.GetKeyDown(KeyCode.Space) )
        //{
        //    transform.localScale = Vector3.one * Time.deltaTime;
        //}

    }



}
//if (CurrentInstruction < Instructions.Count)
//{

//    float RealSpeed = Speed * Time.deltaTime;
//    distInInstruction += RealSpeed;


//    switch (Instructions[CurrentInstruction])
//    {
//        case BallInstruction.MoveUp:
//            transform.position += Vector3.up * RealSpeed;

//            vecRotation += Vector3.forward * rotationSpeed;
//            transform.rotation = Quaternion.Euler(vecRotation);

//            if (vecScale.x <= scaleUpperLimit)
//            {
//                scaleIncrement = (distInInstruction / InstructionLength) * scaleUpperLimit;
//                vecScale.x += scaleIncrement * Time.deltaTime;
//                vecScale.y += scaleIncrement * Time.deltaTime;
//                transform.localScale = vecScale;
//            }

//            break;

//        case BallInstruction.MoveDown:
//            transform.position += Vector3.down * RealSpeed;

//            vecRotation += Vector3.back * rotationSpeed;
//            transform.rotation = Quaternion.Euler(vecRotation);

//            if (vecScale.x >= 1)
//            {
//                scaleIncrement = (distInInstruction / InstructionLength)*scaleUpperLimit;
//                vecScale.x -= ( scaleIncrement) * Time.deltaTime;
//                vecScale.y -= (scaleIncrement) * Time.deltaTime;
//                transform.localScale = vecScale;
//            }

//            break;

//        case BallInstruction.MoveLeft:
//            transform.position += Vector3.left * RealSpeed;

//            vecRotation += Vector3.forward * rotationSpeed;
//            transform.rotation = Quaternion.Euler(vecRotation);

//            vecScale.x += scaleIncrement * Time.deltaTime;
//            vecScale.y += scaleIncrement * Time.deltaTime;
//            transform.localScale = vecScale;
//            break;

//        case BallInstruction.MoveRight:
//            transform.position += Vector3.right * RealSpeed;

//            vecRotation += Vector3.back * rotationSpeed;
//            transform.rotation = Quaternion.Euler(vecRotation);

//            if (vecScale.x >= 1)
//            {
//                vecScale.x -= (scaleIncrement) * Time.deltaTime;
//                vecScale.y -= (scaleIncrement) * Time.deltaTime;
//                transform.localScale = vecScale;
//            }
//            break;

//        default:
//            Debug.Log("Idle");
//            break;
//    }



//    if (distInInstruction > InstructionLength)                                  
//    {
//        ++CurrentInstruction;
//        distInInstruction = 0;
//    }