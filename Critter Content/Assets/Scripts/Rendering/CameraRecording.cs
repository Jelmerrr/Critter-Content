using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraRecording : MonoBehaviour
{
    public bool isRecording = false;
    public bool isPlaying = false;

    public Camera recordingCamera;
    public Camera playbackCamera;

    List<Vector3> cameraLocations;
    List<Quaternion> cameraRotations;
    //This could be optimized and put into 1 list through a class but I'm too lazy and can't be bothered.


    //TODO: Split recordings into different list for later use
    //TODO: Rewrite the playback code to account for different recordings
    //TODO: Add a better failsafe to prevent multiple playbacks from happening at the same time (this breaks a lot of stuff)
    //TODO: Add an option for recordings to looOOooOp


    void Start()
    {
        cameraLocations = new List<Vector3>();
        cameraRotations = new List<Quaternion>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            isRecording = true;
        }
        if (Input.GetKeyUp(KeyCode.R))
        {
            isRecording = false;
        }
    }

    private void FixedUpdate()
    {
        if (isRecording)
        {
            RecordCamera();
        }

        if(isPlaying)
        {
            PlayRecording();
        }
    }

    private void PlayRecording()
    {
        isPlaying = false;
        StartCoroutine(RenderFrame());
    }

    private void RecordCamera()
    {
        cameraLocations.Add(recordingCamera.transform.position);
        cameraRotations.Add(recordingCamera.transform.rotation);
    }

    IEnumerator RenderFrame() //Fixedupdate is 50fps so to account for that this coroutine exists that only simulates positions on a 50fps basis.
    {
        for (int i = 0; i < cameraLocations.Count; i++)
        {
            playbackCamera.transform.position = cameraLocations[i];
            playbackCamera.transform.rotation = cameraRotations[i];
            yield return new WaitForSeconds(.02f);
        }
    }
}
