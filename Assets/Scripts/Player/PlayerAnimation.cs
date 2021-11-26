using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class PlayerAnimation : MonoBehaviour
{
    private Animator _animator;
    private string _currentState;

    public readonly string Idle = "Idle";
    public readonly string Run = "Run";
    public readonly string Jump = "Jump";

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void ChangeAnimation(string state)
    {
        ChangeState(state);
        PlayState();
    }

    public void SetDefaultAnimation()
    {
        ChangeAnimation(Idle);
    }

    private void ChangeState(string newState)
    {
        if (_currentState == newState)
            return;

        _currentState = newState;
    }

    private void PlayState()
    {
        _animator.Play(_currentState);
    }
}