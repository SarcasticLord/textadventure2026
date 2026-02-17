using UnityEditor.Experimental.GraphView;
using UnityEngine;

[CreateAssetMenu(fileName = "exit", menuName = "Text/exit")]
public class Exit : ScriptableObject
{
    public enum Direction { north, south, east, west };

    public Direction direction;
    [TextArea]
    public string description;
    public Room room; // the room this exit will be attached to
}
