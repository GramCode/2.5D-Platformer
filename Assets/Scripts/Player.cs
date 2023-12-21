using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5f;
    [SerializeField]
    private float _gravity = 0.4f;
    [SerializeField]
    private float _jumpHeight = 14f;

    private float _yVelocity;
    private bool _doubleJump = false;

    private CharacterController _controller;

    void Start()
    {
        _controller = GetComponent<CharacterController>();

        if (_controller == null)
            Debug.LogError("Character Controller in Player is NULL");
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        Vector3 direction = new Vector3(horizontal, 0, 0);
        Vector3 velocity = direction * _speed;

        if (_controller.isGrounded)
        {
            //_doubleJump = false;
            //Jump
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _yVelocity = _jumpHeight;
                _doubleJump = true;
            }
        }
        else
        {
            if (_doubleJump == true)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    _yVelocity += _jumpHeight;
                    _doubleJump = false;
                }
                
            }
            //Apply Gravity
            _yVelocity -= _gravity;
        }

        velocity.y = _yVelocity;

        _controller.Move(velocity * Time.deltaTime);
    }
}
