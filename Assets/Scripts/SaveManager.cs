using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;


public class SaveManager : Singleton<SaveManager> 
{
    float m_overallTime = 0.0f;   
    //float m_timeSinceLastSave = 0.0f;

    private int LifetimeHits = GameplayManager.Instance.m_points;

    public GameSaveData SaveData;

    private string m_pathBin;
    private string m_pathJSON;

    public bool UseBinary = true;


    public void SaveSettings()
    {
        m_overallTime += SaveData.m_timeSinceLastSave;

        Debug.Log("Saving overall time value: " + m_overallTime);

        PlayerPrefs.SetFloat("OverallTime", m_overallTime);

        SaveData.m_timeSinceLastSave = 0.0f;


        if(UseBinary)
        {
            FileStream file = new FileStream(m_pathBin, FileMode.OpenOrCreate);

            //convert every type of objects to binary format
            BinaryFormatter binFormatt= new BinaryFormatter();

            binFormatt.Serialize(file, SaveData);

            file.Close();
        }

    }

    public void LoadSettings() 
    {
        m_overallTime = PlayerPrefs.GetFloat("OverallTime", 0.0f);
        Debug.Log("Loaded overall time value: " + m_overallTime);

        LifetimeHits = PlayerPrefs.GetInt ("Player points", LifetimeHits );
        Debug.Log("Total points: " + LifetimeHits);


        if (UseBinary && File.Exists(m_pathBin))
        {
            FileStream file = new FileStream(m_pathBin, FileMode.Open);
            BinaryFormatter binFormatt= new BinaryFormatter();
            SaveData = (GameSaveData)binFormatt.Deserialize(file);
            file.Close();
        }
        else
        {
            SaveData.m_timeSinceLastSave=0.0f;
        }

    }

    private void OnApplicationQuit()
    {
        SaveSettings();
        PlayerPrefs.SetInt("Player points", LifetimeHits);

    }

    // Start is called before the first frame update
    void Start()
    {
        SaveData.m_timeSinceLastSave = 0.0f;
        LoadSettings();

        m_pathBin = Path.Combine(Application.persistentDataPath, "save.bin");
        m_pathJSON = Path.Combine(Application.persistentDataPath, "save.json");

    }

    // Update is called once per frame
    void Update()
    {
        SaveData.m_timeSinceLastSave += Time.deltaTime;   
    }
}

[Serializable]
public struct GameSaveData
{
    public float m_timeSinceLastSave;



}
