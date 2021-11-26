using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Patrol : MonoBehaviour
{
    [SerializeField] private Vector3[] _wayPoints;

    void Start()
    {
        Tween tween = transform.DOPath(_wayPoints, 5, PathType.Linear)
            .SetOptions(true)
            .SetLookAt(1, stableZRotation: true)
            .SetLoops(-1);
    }
}