using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollSwitch : MonoBehaviour
{

    [SerializeField] private CapsuleCollider _collider;
    // Start is called before the first frame update
    void Start()
    {
      //  _collider = gameObject.GetComponent<CapsuleCollider>();
    }

    public void Disable()
    {
        _collider.enabled = false;
    }

    public void Enable()
    {
        _collider.enabled = true;
    }
}
