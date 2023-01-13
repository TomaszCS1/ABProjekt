using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayManager : Singleton<GameplayManager>
{
    //public bool Pause;


    public enum EGameState
    {
        Playing,
        Paused
    }

    private EGameState m_state;

    // ACCESSOR definition of enum GameState of type EGameState (can only have 2 values: Playing and Paused)
    public EGameState GameState
    {
        get { return m_state; }
        set
        { 
            m_state = value;
            switch (m_state)
            {
                case EGameState.Paused:
                    {  // if EGameState.Paused starts Event OnGamePaused
                        if (OnGamePaused != null)
                            OnGamePaused();
                    }
                    break;

                case EGameState.Playing:
                    {  // if EGameState.Playing starts Event OnGamePlaying
                        if (OnGamePlaying != null)
                            OnGamePlaying();
                    }
                    break;
            }
        }
    }

    // DELEGATE 
    public delegate void GameStateCallback();

    // EVENTS 
    public static event GameStateCallback OnGamePaused;
    public static event GameStateCallback OnGamePlaying;

    List<InteractiveComponent> m_restartableObjects = new List<InteractiveComponent>();


    // Start is called before the first frame update
    void Start()
    {
            // starts the game (L.60) and calls method GetAllRestartableObjects(L.61)
            m_state = EGameState.Playing;
            GetAllRestartableObjects();
        

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyUp(KeyCode.R))
            Restart();

        // if hit Space - change GameState from EGameState.Paused to EGameState.Playing od opposite 
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
        }


        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Debug.Log("Quit");
            Application.Quit();
        }

    }

    // runs method DoRestart() on every object implementing class InteractiveComponent
    private void Restart()
    {
        foreach (var restartableObject in m_restartableObjects)
            restartableObject.DoRestart();


    }


    // collect all objects implementing class InteractiveComponent in to the list m_restartableObjects
    private void GetAllRestartableObjects()
    {
        m_restartableObjects.Clear();

        GameObject[] rootGameObjects = SceneManager.GetActiveScene().GetRootGameObjects();
        foreach (var rootGameObject in rootGameObjects)
        {
            InteractiveComponent[] childrenInterfaces = rootGameObject.GetComponentsInChildren<InteractiveComponent>();

            foreach (var childInterface in childrenInterfaces)
                m_restartableObjects.Add(childInterface);
        }
    }



}


   

