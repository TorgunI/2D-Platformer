using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _coin;
    [SerializeField] private Transform _spawn;

    private Transform[] _spawnPoints;

    private int _currentPointIndex;

    private void Awake()
    {
        _spawnPoints = new Transform[_spawn.childCount];

        for (int i = 0; i < _spawn.childCount; i++)
        {
            _spawnPoints[i] = _spawn.GetChild(i);
        }

        ShufflePoints();
    }

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        for (_currentPointIndex = 0; _currentPointIndex < _spawnPoints.Length; _currentPointIndex++)
        {
            Transform currentSpawnPoint = _spawnPoints[_currentPointIndex];
            GameObject coin = Instantiate(_coin, currentSpawnPoint.position, Quaternion.identity);

            while(coin != null)
            {
                yield return null;
            }
        }
        Debug.Log("Все монеты собраны!");
    }

    private void ShufflePoints()
    {
        for (var i = 0; i <= _spawnPoints.Length - 1; i++)
        {
            int point = Random.Range(0, _spawn.childCount);
            Transform tempPoint = _spawnPoints[point];
            _spawnPoints[point] = _spawnPoints[i];
            _spawnPoints[i] = tempPoint;
        }
    }
}