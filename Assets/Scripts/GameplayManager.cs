using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Threading.Tasks;
using Unity.Mathematics;




public class GameplayManager : Singleton<GameplayManager>
{
    
    public enum EGameState
    {
        Playing,
        Paused
    }

    private EGameState m_state;

    // ACCESSOR definition of Enum GameState of type EGameState (can only have 2 values: Playing and Paused)
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
                            // activates HUD buttons  after pressing PauseMenuC/resume
                            m_HUD.ButtonsEnable();
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

    public static event GameStateCallback OpenPauseMenu;

    List<IRestartableObject> m_restartableObjects = new List<IRestartableObject>();

    private HUDController m_HUD;
    public int m_points = 0;

    public PauseMenuController m_PauseMenuController;

    public int Points
    {
        get { return m_points; }
        set
        {
            m_points = value;
            m_HUD.UpdatePoints(m_points);
            LifetimeHits = m_points;
        }
    }

    private bool isPauseMenuActiv = false;

    private int LifetimeHits;

    //PREFABS
    //public GameObject PrefabRef;
    public GameObject PrefabSimpleAnim;

    //this field stores reference to game settings
    public GameSettingsDatabase GameDatabase;



    // Start is called before the first frame update
    void Start()
    {
            // starts the game and calls method GetAllRestartableObjects
            m_state = EGameState.Playing;
            GetAllRestartableObjects();


        m_HUD = FindObjectOfType<HUDController>();
        Points = 0;


        //// COURUTINES (1) calls coroutine
        //StartCoroutine(TestCoroutine());


        // ASYNC / AWAIT
        SecondTestAsync();


        GameObject.Instantiate(GameDatabase.TargetPrefab, new Vector3(7.5f,4.0f,0.0f), Quaternion.identity);

        GameObject.Instantiate(PrefabSimpleAnim, new Vector3(-14.0f,0,0f), Quaternion.Euler(0,0,0));
        //GetAllResttartableObject();
    }


    // ASYNC 
    async Task TestAsync()
    {
        Debug.Log("Starting async method");

        await Task.Delay(TimeSpan.FromSeconds(3)); 

        Debug.Log("Async done after 3 seconds");
    }


    async void SecondTestAsync()
    {
        Debug.Log("Staring second async method");

        await TestAsync();

        Debug.Log("SecondTestAsync");


    }



    // method returns type: IEnumerator
   IEnumerator TestCoroutine()
    {
        Debug.Log("Starting TestCoroutine");

        yield return new WaitForSeconds(0.5f);

        Debug.Log("Resuming after 1 second");

        while (true)
        {
            Debug.Log("Coroutine called");
            Debug.Log("Resuming after 0.5 second");
            Debug.Log("The number of Frames per second every 0.5 seconds: " + Time.frameCount / Time.time);

            // wait for next frame
            yield return null;
        }

    }

    // provides, that coroutine will properly end
    void Destroy()
    {
        StopAllCoroutines();
    }



    //IEnumerator TestCoroutine()
    //{
    //    Debug.Log("Starting coroutine method");
    //    yield return new WaitForSeconds(3);
    //    Debug.Log("Coroutine done after 3 seconds");
    //}



    // Update is called once per frame
    void Update()
    {
        // if hit R initiate function  Restart() 
        if (Input.GetKeyUp(KeyCode.R))
            Restart();


        // if hit SPACE change GameState from EGameState.Paused to EGameState.Playing opposite 
        if (Input.GetKeyUp(KeyCode.Space))
        {

            // pause the game and disables HUD buttons and pause the game
            if (!isPauseMenuActiv )
            {
                OnGamePaused();
                m_HUD.ButtonsDisable();
                isPauseMenuActiv = !isPauseMenuActiv;
            }
            // when second time press Space - starts the game and enables HUD Buttons 
            else
            {
                // m_HUD.ButtonsEnable();         //moved after starting              
                m_PauseMenuController.OnResume(); 
                isPauseMenuActiv = !isPauseMenuActiv; 
            }
        }

        // if hit ESC  
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            // initiate event: OnGamePaused() and pause the game
            OnGamePaused();


            // initiate event: OpenPauseMenu() which is subscribed by function: OnPause() in class PauseMenuController
            OpenPauseMenu();


            // deactivates HUD buttons when PauseMenu active
            if (!isPauseMenuActiv)
            {
                m_HUD.ButtonsDisable();
                isPauseMenuActiv = !isPauseMenuActiv;
            }

            // when second time press Escape - sets PauseMenuControl inactive and enables HUD Buttons
            else 
            {
               // m_HUD.ButtonsEnable();         //moved after starting      

                // sets PauseMenu invisible
                m_PauseMenuController.OnResume();  
                isPauseMenuActiv = !isPauseMenuActiv; 
            }

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
     
        // reset points 
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


   

