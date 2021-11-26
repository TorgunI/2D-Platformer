using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Transform))]

public class Player : MonoBehaviour
{
    private Rigidbody2D _rigidBody;
    private Transform _transform;

    private Vector3 _respawnPosition;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _transform = GetComponent<Transform>();

        _respawnPosition = _transform.position;

        _rigidBody.freezeRotation = true;
    }

    public void Respawn()
    {
        _transform.position = _respawnPosition;
    }
}