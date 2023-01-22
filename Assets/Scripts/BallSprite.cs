using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSprite : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(BallCoroutine());
    }

    IEnumerator BallCoroutine()
    {
        yield return new WaitForSeconds(1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
