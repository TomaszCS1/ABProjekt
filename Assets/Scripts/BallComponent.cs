using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallComponent : MonoBehaviour
{
    private int frames;
    private float framerate;

    public float SpeedX = 1.0f;
    public float SpeedY = 0.75f;
    public float rotationSpeed = 1f;
    public Vector3 vecRotation = Vector3.zero;
    public Vector3 vecTransform;
    public Vector2 vecScale;
    public float scaleUpperLimit = 3.0f;
    public float scaleLowerLimit = 1.0f;

    public float scaleIncrement = 1.0f;
    bool moveForward = true;
    public bool scaleUp = true;


    void Start()
    {

    }
    


    // Update is called once per frame
    void Update()
    {
        //Scale an rotation

        if (vecScale.x <= scaleUpperLimit & scaleUp)
        {                                                   // grow up 
            vecScale.x += scaleIncrement*Time.deltaTime;
            vecScale.y += scaleIncrement*Time.deltaTime;
            transform.localScale = vecScale;

            vecRotation += Vector3.forward * rotationSpeed;
            transform.rotation = Quaternion.Euler(vecRotation);
        }
         else if (vecScale.x <= scaleUpperLimit & !scaleUp & vecScale.x>scaleLowerLimit) // grow down 
        {
            vecScale.x -= scaleIncrement * Time.deltaTime;
            vecScale.y -= scaleIncrement * Time.deltaTime;
            transform.localScale = vecScale;

            vecRotation -= Vector3.forward * rotationSpeed;
            transform.rotation = Quaternion.Euler(vecRotation);
        }

        else if (vecScale.x>scaleUpperLimit & scaleUp)      // upper switch
        {
            scaleUp = !scaleUp;
            vecScale.x -= scaleIncrement * Time.deltaTime;
            vecScale.y -= scaleIncrement * Time.deltaTime;
            transform.localScale = vecScale;
        }

        else if (vecScale.x < scaleLowerLimit & !scaleUp)   //lower switch
        {
            scaleUp = !scaleUp;
            vecScale.x += scaleIncrement * Time.deltaTime;
            vecScale.y += scaleIncrement * Time.deltaTime;
            transform.localScale = vecScale;
        }

        // Position 

        double distance = Math.Sqrt(transform.position.sqrMagnitude);
       

        if (distance <= 3.0 & moveForward)
        {
            transform.position += Vector3.up * Time.deltaTime * SpeedY;
            transform.position += Vector3.left * Time.deltaTime * SpeedX;
        }
        else if (distance <= 3.0 & !moveForward)
        {
            transform.position -= Vector3.up * Time.deltaTime * SpeedY;
            transform.position -= Vector3.left * Time.deltaTime * SpeedX;
        }
        else if (distance > 3.0 & moveForward)
        {
            moveForward = !moveForward;
            transform.position -= Vector3.up * Time.deltaTime * SpeedY;
            transform.position -= Vector3.left * Time.deltaTime * SpeedX;
        }
        else if (distance > 3.0 & !moveForward)
        {
            moveForward = true;
            transform.position += Vector3.up * Time.deltaTime * SpeedY;
            transform.position += Vector3.left * Time.deltaTime * SpeedX;
        }
    }
 
}




//5.
//if (vecScale.x <= scaleUpperLimit)
//{
//    vecScale.x += scaleIncrement;
//    vecScale.y += scaleIncrement;
//    vecScale.z += scaleIncrement;
//    transform.localScale = vecScale;
//}


//4.
//if (transform.localScale.x <= scaleUpperLimit)
//{
//    transform.localScale = transform.localScale + new Vector3(scaleIncrement, scaleIncrement, scaleIncrement);


//3.
//while (vecScale.x <= scaleUpperLimit)
//{
//    vecScale.x += scaleIncrement;
//    vecScale.y += scaleIncrement;
//    vecScale.z += scaleIncrement;
//    transform.localScale = vecScale;
//};

//2.
//do
//{
//    vecScale.x += scaleIncrement;
//    vecScale.y += scaleIncrement;
//    vecScale.z += scaleIncrement;
//    transform.localScale = vecScale;
//}
//while (vecScale.x >= 3) ;

//1.
//do
//{

//    transform.localScale = vecScale * (1 + scaleIncrement);
//    vecScale = vecScale * (1 + scaleIncrement);

//}
//while (vecScale.x < scaleUpperLimit);




