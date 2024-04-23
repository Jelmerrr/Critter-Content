using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour
{
    //This is intended to handle pausing in the future.
    public bool StateFPS = false;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (StateFPS == false) { StateFPS = true; }
            else { StateFPS = false; }
        }
    }
}
