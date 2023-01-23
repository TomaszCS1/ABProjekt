using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//attribute of the class
[CreateAssetMenu(fileName ="GameSettings", menuName = "ScriptableObjects/Create Game Settings", order =1)]
public class GameSettingsDatabase: ScriptableObject
{
    public GameObject TargetPrefab;



}

//public class GameSettingsDatabase : MonoBehaviour
//{
//    // Start is called before the first frame update
//    void Start()
//    {

//    }

//    // Update is called once per frame
//    void Update()
//    {

//    }
//}
