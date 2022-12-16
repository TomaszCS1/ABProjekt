using System;
using System.Collections;
using System.Collections.Generic;
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
    public float scaleIncrement;
    public float scaleUpperLimit = 3.0f;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        if (CurrentInstruction < Instructions.Count)
        {

            float RealSpeed = Speed * Time.deltaTime;
            distInInstruction += RealSpeed;


            switch (Instructions[CurrentInstruction])
            {
                case BallInstruction.MoveUp:
                    transform.position += Vector3.up * RealSpeed;

                    vecRotation += Vector3.forward * rotationSpeed;
                    transform.rotation = Quaternion.Euler(vecRotation);

                    if (vecScale.x <= scaleUpperLimit)
                    {
                        scaleIncrement = (distInInstruction / InstructionLength) * scaleUpperLimit;
                        vecScale.x += scaleIncrement * Time.deltaTime;
                        vecScale.y += scaleIncrement * Time.deltaTime;
                        transform.localScale = vecScale;
                    }

                    break;

                case BallInstruction.MoveDown:
                    transform.position += Vector3.down * RealSpeed;

                    vecRotation += Vector3.back * rotationSpeed;
                    transform.rotation = Quaternion.Euler(vecRotation);

                    if (vecScale.x >= 1)
                    {
                        scaleIncrement = (distInInstruction / InstructionLength)*scaleUpperLimit;
                        vecScale.x -= ( scaleIncrement) * Time.deltaTime;
                        vecScale.y -= (scaleIncrement) * Time.deltaTime;
                        transform.localScale = vecScale;
                    }

                    break;

                case BallInstruction.MoveLeft:
                    transform.position += Vector3.left * RealSpeed;

                    vecRotation += Vector3.forward * rotationSpeed;
                    transform.rotation = Quaternion.Euler(vecRotation);

                    vecScale.x += scaleIncrement * Time.deltaTime;
                    vecScale.y += scaleIncrement * Time.deltaTime;
                    transform.localScale = vecScale;
                    break;

                case BallInstruction.MoveRight:
                    transform.position += Vector3.right * RealSpeed;

                    vecRotation += Vector3.back * rotationSpeed;
                    transform.rotation = Quaternion.Euler(vecRotation);

                    if (vecScale.x >= 1)
                    {
                        vecScale.x -= (scaleIncrement) * Time.deltaTime;
                        vecScale.y -= (scaleIncrement) * Time.deltaTime;
                        transform.localScale = vecScale;
                    }
                    break;

                default:
                    Debug.Log("Idle");
                    break;
            }



            if (distInInstruction > InstructionLength)                                  
            {
                ++CurrentInstruction;
                distInInstruction = 0;
            }

        }
    }
}

