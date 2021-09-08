using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SceneSaveLoad
{
    ///Save scene stats
    public static void Save(SceneData data)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/sceneInfo.dat");
		bf.Serialize(file, data);
		file.Close();
	}

    //Load scene
    public static SceneData LoadData()
    {
        if (File.Exists(Application.persistentDataPath + "/sceneInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/sceneInfo.dat", FileMode.Open);
            SceneData data = (SceneData)bf.Deserialize(file);
            file.Close();
            return data;
        }
        Debug.Log("Nothing to show");
        return null;
    }

    private static string SaveFilePath
    {
        get { return Application.persistentDataPath + "/sceneInfo.dat"; }
    }

    //Reset all data
    public static void Reset()
    {
        try
        {
            File.Delete(SaveFilePath);
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
    }
}
