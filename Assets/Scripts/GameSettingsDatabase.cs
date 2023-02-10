using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//[attribute of the class] together with inheritance from Scriptable object allows to create instances of class in menu Assets /ScriptableObjects /Create Game Settings
[CreateAssetMenu(fileName ="GameSettings", menuName = "ScriptableObjects/Create Game Settings", order =1)]
public class GameSettingsDatabase: ScriptableObject
{

    [Header("Prefabs")]
    public GameObject TargetPrefab;

    [Header("ScriptableObjects")]
    public AudioClip PullSound;

    public AudioClip ShootSound;

    public AudioClip RestartSound;

}
