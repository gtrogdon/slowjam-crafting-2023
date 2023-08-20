using UnityEngine;

public class Actor : MonoBehaviour
{
    public float moveSpeed = 3.5f;
    protected CharacterController _controller;
	protected Animator _animator;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        _controller = GetComponent<CharacterController>();        
        _animator = GetComponentInChildren<Animator>();        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveHorizontal(Vector3 move)
    {
        _controller.Move(move * moveSpeed * Time.deltaTime); 
        _animator.SetBool("IsMoving", move.x != 0 ? true : false);
        _animator.SetFloat("Movement", move.x);
    }
}
