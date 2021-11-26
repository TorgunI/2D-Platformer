using System.Collections.Generic;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class PhysicsMovement : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;

    private bool _isJumped;
    private float _minGroundNormalY = .65f;
    private float _gravityModifier = .5f;
    private Vector2 _horisontalVelocity;
    private Rigidbody2D _rigidBody;
    private Vector2 _velocity;
    private Vector2 _targetVelocity;
    private Vector2 _groundNormal;
    private ContactFilter2D _contactFilter;
    private RaycastHit2D[] _hitBuffer;
    private List<RaycastHit2D> _hitBufferList;

    private const float MinMoveDistance = .001f;
    private const float ShellRadius = .01f;
    private const float RunSpeed = 4f;
    private const float JumpSpeed = 7f;

    public bool Grounded { get; private set; }

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();

        _hitBuffer = new RaycastHit2D[16];
        _hitBufferList = new List<RaycastHit2D>(16);

        _layerMask = ~0;

        _isJumped = false;
        Grounded = true;
    }

    private void Start()
    {
        _contactFilter.useTriggers = false;
        _contactFilter.SetLayerMask(_layerMask);
        _contactFilter.useLayerMask = true;
    }

    private void FixedUpdate()
    {
        Grounded = false;

        _velocity += _gravityModifier * Physics2D.gravity * Time.deltaTime;
        _velocity.x = _horisontalVelocity.x;

        Vector2 deltaPosition = _velocity * Time.deltaTime;
        Vector2 moveAlongGround = new Vector2(_groundNormal.y, -_groundNormal.x);

        Vector2 moveVector = moveAlongGround * deltaPosition.x * RunSpeed;

        Move(moveVector, false);
        moveVector = Vector2.up * deltaPosition.y;
        Move(moveVector, true);
    }

    public void GetTargetVelocity(Vector2 horisontalVelocity, bool isJumpPressed)
    {
        _horisontalVelocity = horisontalVelocity;

        if (isJumpPressed)
        {
            _velocity.y = JumpSpeed;
            _isJumped = isJumpPressed;
        }
    }

    private void Move(Vector2 moveVector, bool yMovement)
    {
        float distance = moveVector.magnitude;

        if (distance > MinMoveDistance)
        {
            UpdateHitBufferList(moveVector, distance);

            for (int i = 0; i < _hitBufferList.Count; i++)
            {
                Vector2 currentNormal = GetCurrentNormal(_hitBufferList[i], yMovement);
                CalculateInclinedSurface(currentNormal);
                CalculateDistance(distance, _hitBufferList[i]);
            }
        }

        _rigidBody.position += moveVector.normalized * distance;
    }

    private void UpdateHitBufferList(Vector2 moveVector, float distance)
    {
        int hitCount = _rigidBody.Cast(moveVector, _contactFilter, _hitBuffer, distance + ShellRadius);

        _hitBufferList.Clear();

        for (int i = 0; i < hitCount; i++)
        {
            _hitBufferList.Add(_hitBuffer[i]);
        }
    }

    private Vector2 GetCurrentNormal(RaycastHit2D physicObject, bool yMovement)
    {
        Vector2 currentNormal = physicObject.normal;

        if (currentNormal.y > _minGroundNormalY)
        {
            Grounded = true;

            if (yMovement)
            {
                _groundNormal = currentNormal;
                currentNormal.x = 0;

                _isJumped = false;
            }
        }
        return currentNormal;
    }

    private void CalculateInclinedSurface(Vector2 currentNormal)
    {
        float projection = Vector2.Dot(_velocity, currentNormal);

        if (projection < 0)
        {
            _velocity -= projection * currentNormal;
        }
    }

    private float CalculateDistance(float distance, RaycastHit2D physicObject)
    {
        float modifiedDistance = physicObject.distance - ShellRadius;
        return modifiedDistance < distance ? modifiedDistance : distance;
    }
}