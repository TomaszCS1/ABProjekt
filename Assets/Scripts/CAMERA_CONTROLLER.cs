using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private BallComponent ball;
    private Vector3 originalPosition;



    // Start is called before the first frame update
    void Start()
    {
        ball = FindObjectOfType<BallComponent>();
        originalPosition = transform.position;

    }

    // Update is called once per frame
    void Update()
    {


    }

    void FixedUpdate()
    {
        if (!ball.IsSimulated())
            return;

        transform.position = Vector3.MoveTowards(transform.position, originalPosition + ball.transform.position, ball.PhysicsSpeed *Time.deltaTime);
    }

}
