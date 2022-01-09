using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionVector : MonoBehaviour
{
    [SerializeField] private PhysicsMovement _movement;

    private Vector2 _horisontal;

    private bool _isJumpPressed;

    private void Update()
    {
        _horisontal = new Vector2(Input.GetAxis("Horizontal"), 0);

        _isJumpPressed = _movement.Grounded && Input.GetKey(KeyCode.Space);

        _movement.AssignTargetVelocity(_horisontal, _isJumpPressed);
    }

    public float GetHorisontal()
    {
        return _horisontal.x;
    }
}