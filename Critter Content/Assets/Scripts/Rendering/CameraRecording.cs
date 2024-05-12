using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraRecording : MonoBehaviour
{
    private bool isRecording = false; //Bools to store states (might change when video recorder system is properly in place)
    public bool isPlaying = false;

    [SerializeField]
    private Camera recordingCamera; //Camera used to store positions
    [SerializeField]
    private Camera playbackCamera; //Camera used to repeat positions stored

    private List<RecordingPoints> cameraLocations = new List<RecordingPoints>(); //List of camera locations in a given recording
    public List<Recording> recordings = new List<Recording>(); //List of recordings

    public int wantedRecording = 0; //placeholder untill newer systems are implemented (video editor)

    void Update() //Stuff in here is placeholder untill newer systems are implemented (video recorder and video editor)
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            cameraLocations.Clear();
            isRecording = true;
        }
        if (Input.GetKeyUp(KeyCode.R))
        {
            isRecording = false;
            SaveRecording();
        }
        if(Input.GetKeyDown(KeyCode.T))
        {
            PlayRecording(wantedRecording);
        }
    }

    private void FixedUpdate() //Stuff in here is placeholder untill newer systems are implemented (video recorder)
    {
        if (isRecording)
        {
            RecordCamera();
        }
    }

    private void RecordCamera() //Stores current camera position and rotation
    {
        RecordingPoints newRecordingPoint = new RecordingPoints(recordingCamera.transform.position, recordingCamera.transform.rotation);
        cameraLocations.Add(newRecordingPoint);
    }

    private void SaveRecording() //Stores a list of recorded camera positions as a recording
    {
        string recordingName;
        recordingName = "Recording" + recordings.Count;
        Recording newRecording = new Recording(new List<RecordingPoints>(cameraLocations), recordingName);
        recordings.Add(newRecording);
        Debug.Log("Recording saved as: " + recordingName);
    }

    public void PlayRecording(int recordingID) //Plays back a given recording 
    {
        if(isPlaying == false)
        {
            StartCoroutine(RenderVideo(recordingID));
        }
    }

    IEnumerator RenderVideo(int recordingIndex)  //Coroutine that updates camera positions based on stored recording
    {
        //TODO: Might be cool to intentionally make this lower fps (25) too simulate older cameras
        isPlaying = true;
        for (int i = 0; i < recordings[recordingIndex].frames.Count; i++)
        {
            RecordingPoints recordingPoints = recordings[recordingIndex].frames[i];
            playbackCamera.transform.position = recordingPoints.position;
            playbackCamera.transform.rotation = recordingPoints.rotation;
            if(i == recordings[recordingIndex].frames.Count - 1)
            {
                isPlaying = false;
                StopCoroutine(RenderVideo(recordingIndex));
            }
            yield return new WaitForSeconds(.02f);
            //Fixedupdate is 50fps so to account for that this coroutine simulates positions on a 50fps basis
        }
    }
}
