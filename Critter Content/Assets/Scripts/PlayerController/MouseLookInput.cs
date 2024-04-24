using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLookInput : MonoBehaviour
{
    public Transform playerRigidBody;
    public MouseController mouseController;
    public StateController stateController;

    public float mouseInputX;
    public float mouseInputY;

    public float lookSensitivity = 0f;

    public bool inFPSMode 
    {
        get{ return _inFPSMode; }
        set
        {
            _inFPSMode = value;
            if(_inFPSMode == true)
            {
                ActivateFPS();
            }
            else
            {
                DeactivateFPS();
            }
        }
    }
    private bool _inFPSMode;

    float upDownRotation = 0f;

    void Update()
    {
        if(inFPSMode == true){ UpdatePosition(); }
        inFPSMode = stateController.StateFPS;
    }

    void UpdatePosition()
    {
        mouseInputX = Input.GetAxis("Mouse X") * lookSensitivity * Time.deltaTime;
        mouseInputY = Input.GetAxis("Mouse Y") * lookSensitivity * Time.deltaTime;

        upDownRotation -= mouseInputY;
        upDownRotation = Mathf.Clamp(upDownRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(upDownRotation, 0f, 0f);
        playerRigidBody.Rotate(Vector3.up * mouseInputX);
    }

    void ActivateFPS()
    {
        mouseController.ActivateFPS();
    }

    void DeactivateFPS()
    {
        mouseController.DeactivateFPS();
    }
}
