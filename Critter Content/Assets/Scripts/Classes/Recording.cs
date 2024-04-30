using System.Collections.Generic;

public class Recording //Handles a collection of recording points (a list of camera locations)
{
    public string name;
    public List<RecordingPoints> frames = new List<RecordingPoints>();

    public Recording(List<RecordingPoints> _frames, string _name)
    {
        frames = _frames;
        name = _name;
    }
}
