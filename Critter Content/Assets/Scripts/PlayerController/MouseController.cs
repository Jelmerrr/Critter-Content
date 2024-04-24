using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{

    public bool mouseFPSActive = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateFPS()
    {
        mouseFPSActive = true;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void DeactivateFPS()
    {
        mouseFPSActive = false;
        Cursor.lockState = CursorLockMode.None;
    }
}
