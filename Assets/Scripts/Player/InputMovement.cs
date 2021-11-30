using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMovement : MonoBehaviour
{
    [SerializeField] private PhysicsMovement _movement;
    [SerializeField] private PlayerAnimation _playerAnimation;

    private Vector2 _horisontal;

    private bool _isJumpPressed;

    private void Update()
    {
        _horisontal = new Vector2(Input.GetAxis("Horizontal"), 0);

        _isJumpPressed = _movement.Grounded && Input.GetKey(KeyCode.Space);

        Animate();

        _movement.GetTargetVelocity(_horisontal, _isJumpPressed);
    }

    private void Animate()
    {
        if (_movement.Grounded)
        {
            if (_horisontal.x != 0)
                _playerAnimation.ChangeAnimation(_playerAnimation.Run);
            else
                _playerAnimation.SetDefaultAnimation();
        }
        else if(_movement.Grounded == false)
        {
            _playerAnimation.ChangeAnimation(_playerAnimation.Jump);
        }
    }
}