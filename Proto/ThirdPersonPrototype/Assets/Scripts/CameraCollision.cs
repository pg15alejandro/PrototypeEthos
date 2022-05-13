using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollision : MonoBehaviour
{
    float _MinDistance = 1.0f;
    float _MaxDistance = 4.0f;
    float _Smooth = 10.0f;
    Vector3 _DollyDir, _DollyDirAdjusted;
    float _Distance;
    
    private void Awake()
    {
        _DollyDir = transform.localPosition.normalized;
        _Distance = transform.localPosition.magnitude;        
    }

    void Update()
    {
        Vector3 desiredCameraPos = transform.parent.TransformPoint(_DollyDir * _MaxDistance);
        RaycastHit hit;

        if(Physics.Linecast(transform.parent.position, desiredCameraPos, out hit)){
            _Distance = Mathf.Clamp((hit.distance * .85f), _MinDistance, _MaxDistance);
        }
        else
            _Distance = _MaxDistance;

        transform.localPosition = Vector3.Lerp(transform.localPosition, _DollyDir * _Distance, Time.deltaTime * _Smooth);
    }
}
