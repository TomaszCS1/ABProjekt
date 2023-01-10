using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : Singleton<GameplayManager>
{
    public bool Pause;

    public enum EGameState
    {
        Playing,
        Paused
    }

    private EGameState m_state;


    // ACCESSOR
    public EGameState GameState
    {
        get { return m_state; }
        set { m_state = value; }
    }

    // DELEGATE 
    public delegate void GameStateCallback();

    public static event GameStateCallback OnGamePaused;
    public static event GameStateCallback OnGamePlaying;


    // Start is called before the first frame update
    void Start()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            switch (GameState)
            {
                case EGameState.Playing:
                    {
                        GameState = EGameState.Paused;
                    }
                    break;

                case EGameState.Paused:
                    {
                        GameState = EGameState.Playing;
                    }
                    break;
            }

            switch (m_state)
            {
                case EGameState.Paused:
                    {
                        if (OnGamePaused != null)
                            OnGamePaused();
                    }
                    break;

                case EGameState.Playing:
                    {
                        if (OnGamePlaying != null)
                            OnGamePlaying();
                    }
                    break;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyUp(KeyCode.Space))
        //    Pause = !Pause;


        if (Input.GetKeyUp(KeyCode.Escape))
        {

            Debug.Log("Quit");
            Application.Quit();

        }
    }


   
}
