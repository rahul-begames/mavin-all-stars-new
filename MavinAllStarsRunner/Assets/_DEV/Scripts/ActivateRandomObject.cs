

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateRandomObject : MonoBehaviour
{
    [SerializeField] private GameObject[] _gameObjects;
    [SerializeField] private bool isForLane;

    private void OnEnable()
    {
        if (_gameObjects != null && _gameObjects.Length > 0)
        {
            foreach (var gameObject in _gameObjects)
            {
                gameObject.SetActive(false);
            }
        
            _gameObjects[UnityEngine.Random.Range(0, _gameObjects.Length)].SetActive(true);
        }
    }
}

