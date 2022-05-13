using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float _CameraRotVel = 2;

    public float _RotY;
    public float _RotX;

    public bool LockOn;

    private void Start()
    {
        LockOn = false;
    }

    private void Update()
    {
        if(!LockOn)
            UpdateCameraRot();
    }

    private void UpdateCameraRot()
    {
        var mouseX = Input.GetAxisRaw("Mouse X");
        var mouseY = Input.GetAxisRaw("Mouse Y");

        //Multiply the input by -1 (so that the input is not inverted)
        mouseY *= -1 * _CameraRotVel;
        mouseX *= 3;

        //Add the input to the Y rotation
        _RotY += mouseY;
        _RotX += mouseX;

        //Clamp the value between -90 and 90 (180 total)
        _RotY = Mathf.Clamp(_RotY, -30, 45);

        transform.localEulerAngles = new Vector3(_RotY, _RotX, 0);

    }

    public void ResetVals(Vector2 rot){
            
        _RotX = rot.x;
        _RotY = rot.y;

        print("DID IT");
    }

    public Vector2 GetVals(){

        print(_RotX);
        print(_RotY);

        return new Vector2(_RotX, _RotY);
    }
}
