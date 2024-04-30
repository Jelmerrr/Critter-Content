using UnityEngine;

public class RecordingPoints //Handles camera position and rotation
{
    public Vector3 position;
    public Quaternion rotation;

    public RecordingPoints(Vector3 _position, Quaternion _rotation)
    {
        position = _position;
        rotation = _rotation;
    }
    
}
