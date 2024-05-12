using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VideoEditor : MonoBehaviour
{
    [SerializeField]
    private List<string> videoOrder = new List<string>(); //Serialized for now but should be not once UI is in place
    [SerializeField]
    private List<string> transitionTexts = new List<string>();

    [SerializeField]
    private bool videoHasStarted = false; //Both bools should be private but this one is currently used to manually start the video
    private bool isInTransition = false;

    [SerializeField]
    private Camera playbackCamera; //Reference to the video playback camera
    [SerializeField]
    private Canvas transitionCanvas; //Reference to the canvas for transitions
    [SerializeField]
    private TextMeshProUGUI textObject; //Reference to the transition text

    [SerializeField]
    private CameraRecording cameraRecording; //Script reference that stores the recordings

    void Start()
    {
        transitionCanvas.gameObject.SetActive(false);
    }

    void Update()
    {
        if(videoHasStarted && isInTransition == false && cameraRecording.isPlaying == false && videoOrder.Count != 0) //Start a new video segment when all these terms and conditions apply
        {
            StartNextSegment();
        }
        if(videoOrder.Count == 0) //If no segment is left end the video
        {
            EndVideo();
        }
    }

    private void ConstructVideo()
    {
        //For now this is useless, but once I finally bother to do UI some logic will go here
    }

    private void StartNextSegment() //Find out if next segment is a recording playback or a transition slide
    {
            if (videoOrder[0].Contains("recording"))
            {
                PlayRecording(videoOrder[0]);
                videoOrder.RemoveAt(0);
            }
            if (videoOrder[0].Contains("transition"))
            {
                PlayTransition(videoOrder[0]);
                videoOrder.RemoveAt(0);
            }
    }

    private void PlayRecording(string index) //Play designated recording
    {
        string newIndex = index.Remove(0,9); //Hacky but does the job
        int indexID = int.Parse(newIndex);
        cameraRecording.PlayRecording(indexID);
    }

    private void PlayTransition(string index) //Play designated transition
    {
        string newIndex = index.Remove(0, 10);
        int indexID = int.Parse(newIndex);
        textObject.text = transitionTexts[indexID];
        StartCoroutine(RenderTransition());
    }

    private void EndVideo() //End the video
    {
        textObject.text = "Thanks for watching!";
        StartCoroutine(RenderTransition());
        videoHasStarted = false;
    }

    IEnumerator RenderTransition()
    {
        isInTransition = true;
        transitionCanvas.gameObject.SetActive(true);
        

        playbackCamera.transform.position = new Vector3(-100,2,20); //This shit is hardcoded garbage but for now I don't care
        playbackCamera.transform.rotation = new Quaternion(0, 0, 0, 0);

        yield return new WaitForSeconds(5f); //Would be cool to make this eventualyl sync with music but that would be fucked to do
        isInTransition = false;
        transitionCanvas.gameObject.SetActive(false);
        StopCoroutine(RenderTransition());
    }
}
