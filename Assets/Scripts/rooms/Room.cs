using UnityEngine;

[CreateAssetMenu(fileName = "room", menuName = "Text/room")]
public class Room : ScriptableObject
{
    public string roomName;
    [TextArea]
    public string Description;
    public Exit[] exits;
}
