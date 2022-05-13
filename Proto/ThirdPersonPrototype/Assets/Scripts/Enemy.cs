using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private CapsuleCollider _collider;
    private bool _Attack;
    [SerializeField] private Animator _anim;
    [SerializeField] private GameObject _parent;
    private int hitCounter;

    // Start is called before the first frame update
    void Start()
    {
        _collider = gameObject.GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        _Attack = Input.GetButtonDown("Fire1");

        if (_Attack)
        {
            _anim.SetTrigger("Attack");
            _collider.enabled = true;
        }
        
        
            
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Sword")
        {
            AddHit();
        }
        
        Debug.Log("AAAAA MI PICHULA");
    }

    private void AddHit()
    {
       if (hitCounter < 3)
       {
           hitCounter++;
       }
       else
       {
            Destroy(_parent);
       }
    }
}
