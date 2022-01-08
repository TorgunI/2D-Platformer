using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator))]

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private PhysicsMovement _movement;
    [SerializeField] private DirectionVector _directionVector;

    private Animator _animator;
    
    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetDefaultAnimation()
    {
        _animator.Play(AnimatorPlayerControllerStates.Idle);
    }

    public void Animate()
    {
        if (_movement.Grounded)
        {
            if (_directionVector.GetHorisontal().x != 0)
                _animator.Play(AnimatorPlayerControllerStates.Run);
            else
                SetDefaultAnimation();
        }
        else if (_movement.Grounded == false)
        {
            _animator.Play(AnimatorPlayerControllerStates.Jump);
        }
    }
}