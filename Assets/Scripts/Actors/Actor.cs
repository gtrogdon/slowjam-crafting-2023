using UnityEngine;
using FMOD.Studio;
using System.Diagnostics;

public class Actor : MonoBehaviour
{
    public float moveSpeed = 3.5f;
    protected CharacterController _controller;
	protected Animator _animator;

    // Audio
    private EventInstance playerFootsteps;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        _controller = GetComponent<CharacterController>();        
        _animator = GetComponentInChildren<Animator>();
        playerFootsteps = AudioManager.instance.CreateEventInstance(FMODEvents.instance.playerFootsteps);
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


        // I needed to borrow the value for move.x, but I wasn't sure how to do that outside of this method
        if (move.x != 0)
        {
            // Get playback state
            PLAYBACK_STATE playbackState;
            playerFootsteps.getPlaybackState(out playbackState);
            if (playbackState.Equals(PLAYBACK_STATE.STOPPED))
            {
                playerFootsteps.start();
            }
        }
        // Otherwise, stop footsteps event
        else
        {
            playerFootsteps.stop(STOP_MODE.ALLOWFADEOUT);
        }
    }
}
