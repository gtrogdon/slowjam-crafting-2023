using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController _controller;
    [SerializeField] private float _gravity;
    [SerializeField] private float _playerSpeed = 5.0f;
    [SerializeField] private float _jumpHeight;

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();        
    }

    // Update is called once per frame
    void Update()
    {
        float velocityX = Input.GetAxis("Horizontal");
        float velocityZ = Input.GetAxis("Vertical");
        Vector3 move = new Vector3(velocityX, 0, velocityZ) +
            (_controller.isGrounded ? Vector3.zero : Physics.gravity);
        _controller.Move(move * _playerSpeed * Time.deltaTime);
    }
}
