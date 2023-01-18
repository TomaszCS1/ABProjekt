using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    public static event GameStateCallback OnEscape;

    List<IRestartableObject> m_restartableObjects = new List<IRestartableObject>();

    private HUDController m_HUD;
    private int m_points = 0;

    public PauseMenuController m_PauseMenuController;

    public int Points
    {
        get { return m_points; }
        set
        {
            m_points = value;
            m_HUD.UpdatePoints(m_points);
        }
    }

    private bool isPauseMenuActiv = false;


    // Start is called before the first frame update
    void Start()
    {
            // starts the game and calls method GetAllRestartableObjects
            m_state = EGameState.Playing;
            GetAllRestartableObjects();


        m_HUD = FindObjectOfType<HUDController>();
        Points = 0;


    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyUp(KeyCode.R))
            Restart();

        // if hit Space - change GameState from EGameState.Paused to EGameState.Playing od opposite 
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (!isPauseMenuActiv)
            {
                OnGamePaused();
                m_HUD.ButtonsDisable();
                isPauseMenuActiv = !isPauseMenuActiv;
            }
            else { m_HUD.ButtonsEnable(); m_PauseMenuController.OnResume(); isPauseMenuActiv = !isPauseMenuActiv; }

          

        }


        if (Input.GetKeyUp(KeyCode.Escape))
        {
            // initiate event OnGamePaused(?)
            GameState = EGameState.Paused;

           
            // deactivate HUD buttons when PauseMenu activ
            if (!isPauseMenuActiv)
            {
                m_HUD.ButtonsDisable();
                isPauseMenuActiv = !isPauseMenuActiv;
            }
            // whe second time Escape sets PauseMenuControl Inaktiv and activvate HUD Buttons
            else { m_HUD.ButtonsEnable(); m_PauseMenuController.OnResume(); isPauseMenuActiv = !isPauseMenuActiv; }

            //initiate event: OnEscape
            OnEscape();

        }

    }

                                                                    
    public void PlayPause()
    { 
        switch (GameState)
        {
             case EGameState.Playing: { GameState = EGameState.Paused; } 
                break;
             case EGameState.Paused: { GameState = EGameState.Playing; } 
                break;
        }
    }



    // runs method DoRestart() on every object implementing class InteractiveComponent
    public void Restart()
    {
        foreach (var restartableObject in m_restartableObjects)
            restartableObject.DoRestart();
     
        // resetuje punkty
        Points = 0;
       }


    // collect all objects implementing class InteractiveComponent in to the list m_restartableObjects
    private void GetAllRestartableObjects()
    {
        m_restartableObjects.Clear();

        GameObject[] rootGameObjects = SceneManager.GetActiveScene().GetRootGameObjects();
        foreach (var rootGameObject in rootGameObjects)
        {
            IRestartableObject[] childrenInterfaces = rootGameObject.GetComponentsInChildren<IRestartableObject>();

            foreach (var childInterface in childrenInterfaces)
                m_restartableObjects.Add(childInterface);
        }
    }



}


   

