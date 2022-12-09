using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallComponent : MonoBehaviour
{
    private int frames;
    private float framerate;

    public float Speed = 1.0f;
    public float rotationSpeed = 10.0f;
    public Vector3 vecRotation = Vector3.zero;

    public Vector3 vecScale;
    public float scaleUpperLimit = 3.0f;
    public float scaleGrowRatio = 0.100000000f;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // transform.position += Vector3.up * Time.deltaTime * Speed;

        //vecRotation += Vector3.forward * rotationSpeed;
        //transform.rotation = Quaternion.Euler(vecRotation);

        //++frames;
        //Debug.Log("Frames passed =" + frames);

        //framerate = 1 / Time.deltaTime;
        //Debug.Log("Liczba klatek na sekunde =" + framerate);


        //5.
        if (vecScale.x <= scaleUpperLimit)
        {
            vecScale.x += scaleGrowRatio;
            vecScale.y += scaleGrowRatio;
            vecScale.z += scaleGrowRatio;
            transform.localScale = vecScale;
        }
    }

 }

    //4.
    //if (transform.localScale.x <= scaleUpperLimit)
    //{
    //    transform.localScale = transform.localScale + new Vector3(scaleGrowRatio, scaleGrowRatio, scaleGrowRatio);


    //3.
    //while (vecScale.x <= scaleUpperLimit)
    //{
    //    vecScale.x += scaleGrowRatio;
    //    vecScale.y += scaleGrowRatio;
    //    vecScale.z += scaleGrowRatio;
    //    transform.localScale = vecScale;
    //};

    //2.
    //do
    //{
    //    vecScale.x += scaleGrowRatio;
    //    vecScale.y += scaleGrowRatio;
    //    vecScale.z += scaleGrowRatio;
    //    transform.localScale = vecScale;
    //}
    //while (vecScale.x >= 3) ;

    //1.
    //do
    //{

    //    transform.localScale = vecScale * (1 + scaleGrowRatio);
    //    vecScale = vecScale * (1 + scaleGrowRatio);

    //}
    //while (vecScale.x < scaleUpperLimit);










}
