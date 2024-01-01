using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private bool _canWallJump = false;
    private int _coins;
    private int _lives = 3;

    private CharacterController _controller;
    private Vector3 _direction, _velocity, _wallSurfaceNormal;

    void Start()
    {
        _controller = GetComponent<CharacterController>();

        if (_controller == null)
            Debug.LogError("Character Controller in Player is NULL");

        UIManager.Instance.UpdateLivesText(_lives);
    }

    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        
        if (_controller.isGrounded)
        {
            _direction = new Vector3(horizontal, 0, 0);
            _velocity = _direction * _speed;
            //Jump
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _yVelocity = _jumpHeight;
                _doubleJump = true;
            }

            _canWallJump = false;

        }
        else
        {
            if (_doubleJump == true && _canWallJump == false)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    _yVelocity += _jumpHeight;
                    _doubleJump = false;
                }

            }

            if (_canWallJump == true)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    _velocity = _wallSurfaceNormal * _speed;
                    _yVelocity = _jumpHeight + (_jumpHeight / 3);
                    _canWallJump = false;
                }
            }

            //Apply Gravity
            _yVelocity -= _gravity;
        }

        _velocity.y = _yVelocity;

        _controller.Move(_velocity * Time.deltaTime);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (_controller.isGrounded == false && hit.transform.CompareTag("Wall"))
        {
            Debug.DrawRay(hit.point, hit.normal, Color.blue);
            _wallSurfaceNormal = hit.normal;
            _canWallJump = true;
        }
    }

    public void AddCoin()
    {
        _coins++;
        UIManager.Instance.UpdateCoinsText(_coins);
    }

    public void Damage()
    {

        _lives--;
        UIManager.Instance.UpdateLivesText(_lives);

        if (_lives == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public int Coins()
    {
        return _coins;
    }
}
