using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAMERA_CONTROLLER : MonoBehaviour
{
    private BallComponent followTarget;
    private Vector3 originalPosition;



    // Start is called before the first frame update
    void Start()
    {
        followTarget = FindObjectOfType<BallComponent>();
        originalPosition = transform.position;

    }

    // Update is called once per frame
    void Update()
    {


    }

    void FixedUpdate()
    {
        //transform.position = followTarget.transform.position;
        //transform.position = followTarget.transform.position + originalPosition;
        
        transform.position = Vector3.MoveTowards(transform.position, originalPosition + followTarget.transform.position, followTarget.PhysicsSpeed *Time.deltaTime);

        if (!followTarget.IsSimulated())
            return;

    }

}
