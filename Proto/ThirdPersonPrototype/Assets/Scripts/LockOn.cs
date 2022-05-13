using UnityEngine;

public class LockOn : MonoBehaviour
{
    [SerializeField] private KeyCode _keyCode;
    private bool _lock = false;
    private Transform targetDir;
    private bool _onLock;
    [SerializeField] CameraController _CCamera;
    [SerializeField] Transform _ModelPivotTransform;
    [SerializeField] Transform _ModelTransform;
    [SerializeField] Transform _CameraBaseTransform;

    private void Update()
    {
        RaycastHit hit;
        if (Input.GetKeyDown(_keyCode)) _lock = !_lock;

        Debug.DrawRay(_ModelPivotTransform.position, _ModelPivotTransform.forward * 10, Color.red);

        if (_lock)
        {
            bool hitSomething = Physics.Raycast(_ModelPivotTransform.position, _ModelPivotTransform.forward * 10, out hit);
            if (hitSomething)
            {
                if (hit.collider.tag == "Enemy")
                {
                    Debug.Log("Enemy");
                    targetDir = hit.collider.gameObject.transform;
                    _onLock = true;
                    _CCamera.LockOn = true;
                    _CCamera.ResetVals(new Vector2(0, 0));
                }
            }
        }
        else
        {
            _onLock = false;
            _CCamera.LockOn = false;
        }
        SetPosition();
    }

    private void SetPosition()
    {
        if(targetDir == null){
            _onLock = false;
            _lock = false;
            _CCamera.LockOn = false;
        }

        if (_onLock)
        {
            var tranMod = targetDir.position;
            tranMod.y = 0;

            _ModelTransform.LookAt(tranMod);

            tranMod.y = 1.6f;
            _CameraBaseTransform.LookAt(tranMod);

        }
    }
}