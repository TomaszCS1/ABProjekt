using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HUDController : MonoBehaviour
{
    public Button PauseButton; 
    public Button RestartButton;

    public TMPro.TextMeshProUGUI PointsText;



    // Start is called before the first frame update
    void Start()
    {
        //after pressing PauseButton, delegate starts PlayPause() function in GameplayManager
        PauseButton.onClick.AddListener(delegate {GameplayManager.Instance.PlayPause(); });

        //after pressing RestartButton, delegate starts PlayPause() function in GameplayManager
        RestartButton.onClick.AddListener(delegate {GameplayManager.Instance.Restart(); });

    }

    //TEST MAIN W INNYM BRUNCH
   
    public void UpdatePoints(int points)
    {
        PointsText.text = "Points: " + points;
    }


    //function deactivates HUD buttons, will be called in GM
    public void ButtonsDisable()
    {
        PauseButton.interactable = false;
        RestartButton.interactable = false; 
    }

    //function activates HUD buttons, will be called in GM
    public void ButtonsEnable()
    {
        PauseButton.interactable = true;
        RestartButton.interactable = true;
    }
}

