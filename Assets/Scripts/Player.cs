using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController _controller;
	private Animator _animator;
    [SerializeField] private float _gravityModifier;
    [SerializeField] private float _playerSpeed = 3.5f;
    [SerializeField] private float _jumpHeight;

    // Singleton Instantiation
    private static Player instance;
    public static Player Instance {
        get {
            if (instance == null) instance = GameObject.FindObjectOfType<Player>();
            return instance;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();        
        _animator = GetComponentInChildren<Animator>();        
    }

    // Update is called once per frame
    void Update()
    {
        float velocityX = Input.GetAxis("Horizontal");
        Vector3 move = new Vector3(velocityX, 0, 0) +
            (_controller.isGrounded ? Vector3.zero : Physics.gravity * _gravityModifier);
        _controller.Move(move * _playerSpeed * Time.deltaTime);
        _animator.SetBool("IsMoving", velocityX != 0 ? true : false);
        _animator.SetFloat("Movement", velocityX);          // allow animator state machine access to value
    }
}
