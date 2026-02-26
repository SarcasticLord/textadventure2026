using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Analytics;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public List<string> inventory = new List<string>();

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        Load();
        NavigationManager.instance.onRestart += ResetGame; // notice no () its not calling it its pointing to it
        //Save();
        

    }

    void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/player.save"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream aFile = File.Open(Application.persistentDataPath + "/player.save", FileMode.Open);
            SaveState gameState = (SaveState)bf.Deserialize(aFile);
            aFile.Close();

            Room aroom = NavigationManager.instance.GetRoomByName(gameState.currentRoom);
            if (aroom != null)
                NavigationManager.instance.SwitchRooms(aroom);
        }
        //else // new player
        //    NavigationManager.instance.GameRestart();
    }

    public void Save()
    {
        SaveState gameState = new SaveState();
        gameState.currentRoom = NavigationManager.instance.currentRoom.name;

        BinaryFormatter bf = new BinaryFormatter();
        FileStream aFile = File.Create(Application.persistentDataPath + "/player.save");
        //Debug.Log(Application.persistentDataPath);
        bf.Serialize(aFile, gameState);
        aFile.Close();
    }

    void ResetGame()
    {
        inventory.Clear();
    }
}
