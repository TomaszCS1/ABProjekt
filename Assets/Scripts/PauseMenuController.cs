using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour
{
    public Button ResumeButton;
    public Button ResstartMenuButton;
    public Button QuitButton;
    public GameObject Panel;

    
    // Start is called before the first frame update
    void Start()
    {
        // AddListener(delegate{method();})
        ResumeButton.onClick.AddListener(delegate { OnResume(); });

        QuitButton.onClick.AddListener(delegate { OnQuit(); });

        Panel.SetActive(false);


        GameplayManager.OnGamePaused += OnPause;

        ResstartMenuButton.onClick.AddListener(delegate { GameplayManager.Instance.Restart(); });

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SetPanelVisible(bool visible)
    {
        Panel.SetActive(visible);
    }

    private void OnPause()
    {
        SetPanelVisible(true);
    }

    private void OnResume()
    {
        GameplayManager.Instance.GameState = GameplayManager.EGameState.Playing;
        SetPanelVisible(false);
      
    }

    private void OnQuit()
    {
        Application.Quit();
    }


}
