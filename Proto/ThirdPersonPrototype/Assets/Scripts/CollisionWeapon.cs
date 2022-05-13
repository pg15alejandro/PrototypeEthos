using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionWeapon : MonoBehaviour
{

    private CapsuleCollider _collider;
    [SerializeField] private Animator _anim;
    private bool _attackBtn;

    // Start is called before the first frame update
    void Start()
    {
        _collider = gameObject.GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        _attackBtn = Input.GetButtonDown("Fire1");
        if (_attackBtn)
        {
            _collider.enabled = true;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.tag);
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("collision with " + other.gameObject.tag);
            _collider.enabled = false;
            //var foo = other.transform.parent.gameObject;
            Destroy(other.gameObject);
            //Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "Sword")
        {
            Debug.Log("collision with " + other.gameObject.tag);
            _anim.SetTrigger("Hit");
            _collider.enabled = false;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        Debug.Log("colliding with " + other.gameObject.tag);
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("ended collision with " + other.gameObject.tag);
    }


    // private void OnCollisionEnter(Collision other)
    // {
    //     if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Player")
    //     {
    //         Debug.Log("collision with " + other.gameObject.tag);
    //         var rb = other.gameObject.GetComponent<Rigidbody>();
    //         rb.AddForce(100, 0, 100);
    //         _collider.enabled = false;
    //         Destroy(other.gameObject);
    //     }
    // }
}
