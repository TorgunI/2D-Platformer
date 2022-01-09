using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private PhysicsMovement _movement;
    [SerializeField] private DirectionVector _directionVector;

    private Animator _animator;

    private const string Idle = nameof(Idle);
    private const string Run = nameof(Run);
    private const string Jump = nameof(Jump);

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void SetDefaultState()
    {
        _animator.Play(Idle);
    }

    private void SetRunState()
    {
        _animator.Play(Run);
    }

    private void SetJumpState()
    {
        _animator.Play(Jump);
    }

    public void Animate()
    {
        if (_movement.Grounded)
        {
            if (_directionVector.GetHorisontal() != 0)
                SetRunState();
            else
                SetDefaultState();
        }
        else if (_movement.Grounded == false)
        {
            SetJumpState();
        }
    }
}