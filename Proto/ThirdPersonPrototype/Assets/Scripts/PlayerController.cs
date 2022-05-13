using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [System.NonSerialized] public bool debug = false;
    [System.NonSerialized] public bool Climbing = false;
    [SerializeField] private float _JumpForce;
    [SerializeField] private float _PlayerSpeed;
    [SerializeField] Transform _CameraTransform;
    [SerializeField] Transform _CameraBaseTransform;
    [SerializeField] Transform _ModelTransform;

    private Rigidbody _RB;

    //Input variables
    [SerializeField] private float _XInput;
    [SerializeField] private float _ZInput;
    private bool _Jump;

    private CharacterController _CharController;
    private Vector3 _Direction = Vector3.zero;
    private Animator _Anim;

    private void Start()
    {
        _RB = GetComponent<Rigidbody>();
        _Anim = GetComponentInChildren<Animator>();
    }
    private void Update()
    {
        if (Climbing == false)
        {
            ReadMoveInputs();
            SetAnimValues();
            UpdateRot();
        }
    }

    private void UpdateRot()
    {
        if (_XInput != 0 || _ZInput != 0)
        {
            //Obtain the degrees of the player
            var degrees = Mathf.Rad2Deg * Mathf.Atan2(_XInput, _ZInput);

            _ModelTransform.eulerAngles = new Vector3(transform.eulerAngles.x, degrees + _CameraBaseTransform.eulerAngles.y, transform.eulerAngles.z);
        }
    }

    private void FixedUpdate()
    {
        ApplyMovement();
    }

    private void ReadMoveInputs()
    {
        _XInput = Input.GetAxis("Horizontal");
        _ZInput = Input.GetAxis("Vertical");
        _Jump = Input.GetButtonDown("Jump");
    }

    private void ApplyMovement()
    {
        var newVel = new Vector3(_XInput, 0f, _ZInput) * _PlayerSpeed;

        newVel = _CameraTransform.TransformVector(newVel);

        newVel.y = _Jump ? _JumpForce : _RB.velocity.y;
        _RB.velocity = newVel;
    }

    private void SetAnimValues()
    {
        if (_Anim != null)
        {
            _Anim.SetBool("Movement", (_XInput != 0 || _ZInput != 0));

            if (Input.GetButtonDown("Fire1"))
                _Anim.SetTrigger("Attack");

        }
    }
}
