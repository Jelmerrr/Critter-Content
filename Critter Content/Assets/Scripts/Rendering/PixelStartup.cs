using UnityEngine;

public class PixelStartup : MonoBehaviour
{
    //Enables the screen canvas on run-time so I don't have to see it obstruct my entire fucking scene in Editor.
    public Canvas screenCanvas;
    void Start()
    {
        screenCanvas.gameObject.SetActive(true);
    }
}
