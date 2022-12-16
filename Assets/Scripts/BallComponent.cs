using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallComponent : MonoBehaviour
{
    //2-------------------------------------
    //public float SpeedX = 1.0f;
    //public float SpeedY = 0.75f;
    public float rotationSpeed = 5f;
    public Vector3 vecRotation = Vector3.forward;

    //public Vector3 vecTransform;
    public Vector2 vecScale;
    //public float scaleUpperLimit = 3.0f;
    //public float scaleLowerLimit = 1.0f;

    public float scaleIncrement = 0.8f;
    //public bool scaleUp = true;

    //3--------------------------------------
    //cw bonusowe
    //public enum GameState
    //{
    //    Start,
    //    Pause,
    //    Exit
    //}
    //GameState State = GameState.Start;
    //--------------------------------------



    //4------------------------------------------cwiczenie Pętle a tablice i kolekcje
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

    //private float TimeInInstruction = 0.0f;

    public float Speed = 1.0f;
    
    public List<BallInstruction> Instructions = new List<BallInstruction>();

    private float InstructionLength = 2.0f;

    bool moveUp = true;
    bool moveLeft;


    void Start()
    {
    }

    



    // Update is called once per frame
    void Update()
    {
    
        if (CurrentInstruction < Instructions.Count)
        {
            //TimeInInstruction += Time.deltaTime;
            float RealSpeed = Speed * Time.deltaTime;
            float distInInstruction = Vector3.Distance(Vector3.zero, transform.position);


            switch (Instructions[CurrentInstruction])
            {
                case BallInstruction.MoveUp:
                    transform.position += Vector3.up * RealSpeed;

                    vecRotation += Vector3.forward * rotationSpeed;
                    transform.rotation = Quaternion.Euler(vecRotation);

                    vecScale.x += scaleIncrement * Time.deltaTime;
                    vecScale.y += scaleIncrement * Time.deltaTime;
                    transform.localScale = vecScale;

                    break;

                case BallInstruction.MoveDown:
                    transform.position += Vector3.down * RealSpeed;

                    vecRotation += Vector3.back * rotationSpeed;
                    transform.rotation = Quaternion.Euler(vecRotation);

                    if (vecScale.x >= 1)
                    {
                        vecScale.x -= (1/scaleIncrement) * Time.deltaTime;
                        vecScale.y -= (1/scaleIncrement) * Time.deltaTime;
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
                        vecScale.x -= (1 / scaleIncrement) * Time.deltaTime;
                        vecScale.y -= (1 / scaleIncrement) * Time.deltaTime;
                        transform.localScale = vecScale;
                    }
                    break;

                default:
                    Debug.Log("Idle");
                    break;
            }

            

            if (transform.position.y > InstructionLength & moveUp)                      //move up
            {
                ++CurrentInstruction;
                moveUp=false;

            }
            else if (transform.position.y<0 & !moveUp & transform.position.x==0)        //move down
            {
                ++CurrentInstruction;
                moveLeft = true;
            }

            else if (-transform.position.x > InstructionLength & moveLeft)              //move left
            {
                ++CurrentInstruction;
                moveLeft =!moveLeft;
            }

            else if (transform.position.x> 0 & !moveLeft)                               //move right
            {
                transform.position = Vector3.zero;
                ++CurrentInstruction;

            }



        }


        //4--------------------------------------cwiczenie Petle a tablice i kolekcje
        //switch (Instruction)
        //{
        //    case BallInstruction.MoveUp:
        //        transform.position += new Vector3(0, 1, 0) * Speed * Time.deltaTime;
        //        break;

        //    case BallInstruction.MoveDown:
        //        transform.position -= new Vector3(0, 1, 0) * Speed * Time.deltaTime;
        //        break;

        //    case BallInstruction.MoveLeft:
        //        transform.position -= new Vector3(1, 0, 0) * Speed * Time.deltaTime;
        //        break;

        //    case BallInstruction.MoveRight:
        //        transform.position += new Vector3(1, 0, 0) * Speed * Time.deltaTime;
        //        break;

        //    case BallInstruction.ScaleUp:
        //        transform.localScale += new Vector3(1,1,0) *scaleIncrement* Time.deltaTime;
        //        break;


        //    case BallInstruction.ScaleDown:
        //        transform.localScale -= new Vector3(1, 1, 0) * scaleIncrement * Time.deltaTime;
        //        break;

        //    default:
        //        Debug.Log("Idle");
        //        break;
        //}




        //3--------------------------------------
        //cw bonusowe
        //Debug.Log("State: " + State);

        //int StateVal = (int)State;
        //++StateVal;
        //State = (GameState)StateVal;
        //Debug.Log("New State: " + State);


        //2-----------------------------------------------------------------------------------
        //Scale and rotation

        //    if (vecScale.x <= scaleUpperLimit & scaleUp)
        //    {                                                   // grow up 
        //        vecScale.x += scaleIncrement*Time.deltaTime;
        //        vecScale.y += scaleIncrement*Time.deltaTime;
        //        transform.localScale = vecScale;

        //        vecRotation += Vector3.forward * rotationSpeed;
        //        transform.rotation = Quaternion.Euler(vecRotation);
        //    }
        //     else if (vecScale.x <= scaleUpperLimit & !scaleUp & vecScale.x>scaleLowerLimit) // grow down 
        //    {
        //        vecScale.x -= scaleIncrement * Time.deltaTime;
        //        vecScale.y -= scaleIncrement * Time.deltaTime;
        //        transform.localScale = vecScale;

        //        vecRotation -= Vector3.forward * rotationSpeed;
        //        transform.rotation = Quaternion.Euler(vecRotation);
        //    }

        //    else if (vecScale.x>scaleUpperLimit & scaleUp)      // upper switch
        //    {
        //        scaleUp = !scaleUp;
        //        vecScale.x -= scaleIncrement * Time.deltaTime;
        //        vecScale.y -= scaleIncrement * Time.deltaTime;
        //        transform.localScale = vecScale;
        //    }

        //    else if (vecScale.x < scaleLowerLimit & !scaleUp)   //lower switch
        //    {
        //        scaleUp = !scaleUp;
        //        vecScale.x += scaleIncrement * Time.deltaTime;
        //        vecScale.y += scaleIncrement * Time.deltaTime;
        //        transform.localScale = vecScale;
        //    }

        //    // Position 

        //    double distance = Math.Sqrt(transform.position.sqrMagnitude);


        //    if (distance <= 3.0 & moveForward)
        //    {
        //        transform.position += Vector3.up * Time.deltaTime * SpeedY;
        //        transform.position += Vector3.left * Time.deltaTime * SpeedX;
        //    }
        //    else if (distance <= 3.0 & !moveForward)
        //    {
        //        transform.position -= Vector3.up * Time.deltaTime * SpeedY;
        //        transform.position -= Vector3.left * Time.deltaTime * SpeedX;
        //    }
        //    else if (distance > 3.0 & moveForward)
        //    {
        //        moveForward = !moveForward;
        //        transform.position -= Vector3.up * Time.deltaTime * SpeedY;
        //        transform.position -= Vector3.left * Time.deltaTime * SpeedX;
        //    }
        //    else if (distance > 3.0 & !moveForward)
        //    {
        //        moveForward = true;
        //        transform.position += Vector3.up * Time.deltaTime * SpeedY;
        //        transform.position += Vector3.left * Time.deltaTime * SpeedX;
        //    }
        //-----------------------------------------------------------------------------------


    }

}