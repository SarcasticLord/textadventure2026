using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "room", menuName = "Text/room")]
public class Room : ScriptableObject
{
    public string roomName;
    [TextArea]
    public string Description;
    public Exit[] exits;

    //public bool hasKey;
    //public bool hasOrb;
  

    public List <string> items;

}
