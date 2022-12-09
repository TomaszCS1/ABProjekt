using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallComponent : MonoBehaviour
{
    private int frames;
    private float framerate;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        ++frames;
        Debug.Log("Frames passed =" + frames);

        framerate = 1 / Time.deltaTime;
        Debug.Log("Liczba klatek na sekunde =" + framerate);

    }
}
