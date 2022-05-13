using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climbing : MonoBehaviour
{
    private Rigidbody _rb;
    private Animator _anim;

    [SerializeField] private PlayerController _controller;
    [SerializeField] private GameObject _leftHand;
    [SerializeField] private GameObject _rightHand;
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _chest;

    [SerializeField] private KeyCode _key1;
    private List<GameObject> _list = new List<GameObject>();

    private void Start()
    {
        _rb = gameObject.GetComponentInParent<Rigidbody>();
        _anim = gameObject.GetComponentInParent<Animator>();
    }

    bool _climb;
    bool _offWall;
    private float _XInput;
    [SerializeField] private float _ClimbingSpeed;
    private bool debug = true;
    private bool _currentlyClimbing = false;
    private bool _notFall = true;
    private bool _reached;

    private void Update()
    {
        Debug.DrawRay(_chest.transform.position, transform.TransformDirection(Vector3.forward));
        RaycastHit hit;
        bool wall = Physics.Raycast(_chest.transform.position, _chest.transform.TransformDirection(Vector3.forward), out hit, .5f);
        _climb = Input.GetButtonDown("Fire2");
        _offWall = Input.GetKeyDown(_key1);
        if (_climb && wall)
        {
            if (hit.collider.tag == "Climbable")
            {
                Debug.Log("start climb");
                gameObject.GetComponent<SphereCollider>().enabled = true;
                _rb.useGravity = false;
                _anim.SetTrigger("Climb");
            }
        }

        if (_offWall && wall)
        {
            if (hit.collider.tag == "Climbable")
            {
                foreach (GameObject objet in _list)
                {
                    objet.GetComponent<BoxCollider>().enabled = true;
                }
                Debug.Log("stop climb");
                _rb.AddForce(new Vector3(0f, 0, -10f), ForceMode.Impulse);
                _rb.useGravity = true;
                gameObject.GetComponent<SphereCollider>().enabled = false;
                _anim.SetTrigger("CancelClimb");
                _controller.Climbing = false;
                _anim.SetBool("Reached", false);
            }
        }
        _XInput = Input.GetAxis("Horizontal");

        if (_currentlyClimbing)
        {
            _currentlyClimbing = true;
        }
    }
    private void FixedUpdate()
    {
        if (_controller.Climbing) ApplyMovement();
    }

    private void ApplyMovement()
    {
        var newVel = new Vector3(_XInput, 0f, 0f) * _ClimbingSpeed;
        _rb.AddForce(newVel, ForceMode.Impulse);
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.layer == 11)
        {
            if (other.gameObject.layer == 11 && gameObject.name == "handCollLeft")
            {
                _rb.AddForce(new Vector3(0f, 1f, 0f), ForceMode.Impulse);
                _list.Add(other.gameObject);
                _anim.SetTrigger("ClimbIdle");
                _controller.Climbing = true;
                _controller.debug = false;
                Debug.Log("left hand");
            }
            else if (other.gameObject.layer == 11 && gameObject.name == "handCollRight")
            {
                _rb.AddForce(new Vector3(0f, -1f, 0f), ForceMode.Impulse);
                other.gameObject.GetComponent<BoxCollider>().enabled = false;
                _currentlyClimbing = true;
                Debug.Log("right hand");
            }
            _anim.SetBool("Reached", true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 12)
        {
            Debug.Log("Bumper Exit");
            _notFall = true;
        }
    }
}
