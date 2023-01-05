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
      if (followTarget.isRestarted)
        { transform.position = originalPosition; }

    }

    void FixedUpdate()
    {
        if (!followTarget.IsSimulated())
            return;

        transform.position = Vector3.MoveTowards(transform.position, originalPosition + followTarget.transform.position, followTarget.PhysicsSpeed *Time.deltaTime);

        Debug.Log("Transform.position = " + transform.position + " followTarget.PhysicsSpeed *Time.deltaTime = " + followTarget.PhysicsSpeed * Time.deltaTime);


    }




}
