using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class Player : MonoBehaviour
{
    private Rigidbody2D _rigidBody;

    private Vector3 _respawnPosition;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();

        _respawnPosition = transform.position;

        _rigidBody.freezeRotation = true;
    }

    public void Respawn()
    {
        transform.position = _respawnPosition;
    }
}