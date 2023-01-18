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
    public Button PopupYes;
    public Button PopupNo;
    public GameObject QuestionPopup1;




    // Start is called before the first frame update
    void Start()
    {
        // AddListener(delegate{method();})
        ResumeButton.onClick.AddListener(delegate { OnResume(); });

        Panel.SetActive(false);

        // tym miejscu jest zasubskrybowany event OnGamePause w GM, kiedy OnGamPaused startuje, wykonana jest metoda OnPause
        GameplayManager.OnEscape += OnPause;


        // quit menu:

        QuitButton.onClick.AddListener(delegate { OnQuit(); });

        ResstartMenuButton.onClick.AddListener(delegate { GameplayManager.Instance.Restart(); });

        PopupYes.onClick.AddListener(delegate { OnPopupYes(); });

        PopupNo.onClick.AddListener(delegate { OnPopupNo(); });

        QuestionPopup1.SetActive(false);
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


    public void OnResume()
    {
        GameplayManager.Instance.GameState = GameplayManager.EGameState.Playing;
        SetPanelVisible(false);
    }


    private void OnQuit()
    {
        SetPopupVisible(true);
        SetPanelVisible(false);
    }


    public void SetPopupVisible(bool visible)
    {
        QuestionPopup1.SetActive(visible);
    }


    public void OnPopupNo()
    {
        SetPopupVisible(false);
        SetPanelVisible(true);
    }


    public void OnPopupYes()
    {
        Application.Quit();

    }



}
