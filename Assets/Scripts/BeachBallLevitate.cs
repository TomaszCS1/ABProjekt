using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeachBallLevitate : MonoBehaviour
{
    /*wartość pozycji startowej*/
    private Vector3 m_startPosition;

    /*tworzymy globalne zmienne cyklicznie zmieniających się wartości*/
    private float m_curYPos = 0.0f;
    private float m_previousYPos;
    private float m_curZRot = 0.0f;
    private float m_curXYScale /*= 1.0f*/;
    /*wystawiamy parametry publiczne do wygodnej modyfikacji*/
    public float Amplitude = 10.0f;
    public float RotationSpeed = 50;
    public float ScaleAmplitude = 2.0f;

    void Start()
    {
        /*zapamiętujemy pozycję startową*/
        m_startPosition = transform.position;

        StartCoroutine(BallCoroutine());
    }

    void Update()
    {

    }

    IEnumerator BallCoroutine()
    {
        while (true)
        {
            /*zmiana pozycji*/
            m_curYPos = Mathf.PingPong(Time.time, Amplitude) - Amplitude * 0.5f;
            transform.position = new Vector3(m_startPosition.x,
                                             m_startPosition.y + m_curYPos,
                                             m_startPosition.z);
            //Debug.Log("y: " + m_curYPos);
            //Debug.Log("m_previousYPos: " + m_previousYPos);

            /*zmiana rotacji*/
            m_curZRot += Time.deltaTime * RotationSpeed;
            transform.rotation = Quaternion.Euler(0, 0, m_curZRot);

            //zmiana scali
            m_curXYScale = Mathf.PingPong(Time.time, ScaleAmplitude) + 1.0f;
            transform.localScale = new Vector3(m_curXYScale, m_curXYScale, 1);

            if (m_previousYPos >= 0 && m_curYPos<0)
            {
                Debug.Log("if statement executed");
                yield return new WaitForSeconds(1f);

            }

            m_previousYPos = m_curYPos;


        }
    }

}
