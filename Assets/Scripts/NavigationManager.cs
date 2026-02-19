using System.Collections.Generic;
using UnityEngine;

public class NavigationManager : MonoBehaviour

{
    public static NavigationManager instance;

    public Room startingRoom;
    public Room currentRoom;

    public delegate void Restart();
    public event Restart onRestart;
   

    private Dictionary<string, Room> exitRooms = new Dictionary<string, Room>();

    public Exit toKeyNorth;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        currentRoom = startingRoom;
        Unpack();
    }

    void Unpack()
    {
        string description = currentRoom.Description;

        exitRooms.Clear();
        foreach (Exit e in currentRoom.exits)
        {
            if (!e.isHidden)
            {
                description += " " + e.description;
                exitRooms.Add(e.direction.ToString(), e.room);
            }
        }

        InputManager.instance.UpdateStory(description);

        if (currentRoom.name == "dragon")
        {
            onRestart.Invoke();             // calling the restsrt event
            currentRoom = startingRoom;     // puts the player back at the start
            Unpack();
        }

    }

    public bool SwitchRooms(string direction)
    {
        if (exitRooms.ContainsKey(direction))
        {
            if (GameManager.instance.inventory.Contains("key") || !getExit(direction).isLocked)
            {
                currentRoom = exitRooms[direction];
                InputManager.instance.UpdateStory("you go " + direction);
                Unpack();
                return true;
            }
            else
                return false;




        }
        return false;

    }

    Exit getExit(string direction)
    {
        foreach (Exit e in currentRoom.exits)
        {
            if (e.direction.ToString() == direction)
                return e;
            
        }
        return null;
    }

    public bool getItem(string item)

    {
        bool isFound = false;
        foreach (string i in currentRoom.items)
        {
            if (i == item)
            {
                isFound = true;
                if(item == "orb")
                {
                    toKeyNorth.isHidden = false;
                }
            }
            
        }
        return isFound;
    }

}