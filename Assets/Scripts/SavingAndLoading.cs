using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

public static class SavingAndLoading
{
    //Save player stats and game features method
    public static void SaveData(PlayerStats data)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
        PlayerData player = new PlayerData(data); 
        bf.Serialize(file, player);
        file.Close();
    }

    //Load info and player progress
    public static PlayerData LoadData()
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();
            return data;
        }
        Debug.Log("Nothing to show");
        return null;
    }

    private static string SaveFilePath
    {
        get { return Application.persistentDataPath + "/playerInfo.dat"; }
    }

    //Reset all data
    public static void Reset()
    {
        PlayerPrefs.DeleteAll();
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

