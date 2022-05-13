using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Possesion : MonoBehaviour
{
    private RaycastHit hit;
    private bool _shooting;

    CinemachineVirtualCamera cvCamera;
    CameraController camController;
    PlayerController playController;
    LockOn lockOn;
    GameObject enemyHit;

    [SerializeField] GameObject _Model;
    [SerializeField] KeyCode _keyCode;
    [SerializeField] Transform _ModelPivotTransform;
    [SerializeField] CameraController CameraController;
    [SerializeField] Transform camBase;

    private void Update()
    {
        _shooting = Input.GetKeyDown(_keyCode);

        bool enemy = Physics.Raycast(_ModelPivotTransform.position, _ModelPivotTransform.forward * 10, out hit);

        if (_shooting && enemy)
        {
            if (hit.collider.tag == "Enemy")
            {
                enemyHit = hit.transform.gameObject;

                _Model.SetActive(false);

                //camBase.SetPositionAndRotation(new Vector3(1,1,1), new Quaternion(0,0,0,1));
                

                cvCamera = enemyHit.GetComponentInChildren<CinemachineVirtualCamera>();
                cvCamera.m_Priority = 20;

                camController = enemyHit.GetComponentInChildren<CameraController>();
                if (camController != null)
                    camController.enabled = true;

                playController = enemyHit.GetComponentInChildren<PlayerController>();
                if (playController != null)
                    playController.enabled = true;

                lockOn = enemyHit.GetComponentInChildren<LockOn>();
                if (lockOn != null)
                    lockOn.enabled = true;


                StartCoroutine(Depossesing());
            }
        }
    }

    private IEnumerator Depossesing()
    {
        yield return new WaitForSeconds(5);
        
        _Model.SetActive(true);
        CameraController.ResetVals(camController.GetVals());

        cvCamera.m_Priority = 5;
        camController.enabled = false;
        playController.enabled = false;
        lockOn.enabled = false;

        transform.position = enemyHit.transform.position;
        
        transform.GetChild(0).eulerAngles = enemyHit.transform.GetChild(0).eulerAngles;        
        transform.GetChild(1).eulerAngles = enemyHit.transform.GetChild(1).eulerAngles;        
        
        var test = transform.GetChild(0).transform;

        test.Translate(0, 0, -0.5f);        
        transform.position = new Vector3(test.position.x, transform.position.y, test.position.z);

        Destroy(enemyHit, 1);

        StopAllCoroutines();
    }
}
