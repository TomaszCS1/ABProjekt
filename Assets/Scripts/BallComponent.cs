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

    float distInInstruction = 0;

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
            distInInstruction += RealSpeed;


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
                        vecScale.x -= (1 / scaleIncrement) * Time.deltaTime;
                        vecScale.y -= (1 / scaleIncrement) * Time.deltaTime;
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



            if (distInInstruction > InstructionLength & moveUp)                                  {
                ++CurrentInstruction;
                distInInstruction = 0;

            }

        }
    }
}

