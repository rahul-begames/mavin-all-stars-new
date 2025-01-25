using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class RandomiseGO : MonoBehaviour
{
    [SerializeField] private GameObject[] _gameObjects;
    [SerializeField] private bool isRandom;

    public bool isCar,isTree,isStatic,isSpike,isRolling,isFalling,isLeaf,isRamp,isJapanTree;
    public Transform rollingTransform;

    private void OnEnable()
    {
        if (isCar)
        {
            transform.GetChild(0).transform.localPosition = Vector3.zero;
            if (isRandom)
            {
                foreach (var GO in _gameObjects)
                {
                    if(GO != null)
                        GO.SetActive(false);
                }

                int x = UnityEngine.Random.Range(0, _gameObjects.Length);
            
                if(_gameObjects[x] != null)
                    _gameObjects[x].SetActive(true);
            }
        }

        if (isTree)
        {
            transform.localRotation = Quaternion.Euler(0f,0,0f);
            if (isRandom)
            {
                foreach (var GO in _gameObjects)
                {
                    if(GO != null)
                        GO.SetActive(false);
                }

                int x = UnityEngine.Random.Range(0, _gameObjects.Length);
            
                if(_gameObjects[x] != null)
                    _gameObjects[x].SetActive(true);
            }
        }
        
        if (isJapanTree)
        {
            transform.localRotation = Quaternion.Euler(0f,0,0f);
            if (isRandom)
            {
                foreach (var GO in _gameObjects)
                {
                    if(GO != null)
                        GO.SetActive(false);
                }

                int x = UnityEngine.Random.Range(0,2);
            
                if(_gameObjects[x] != null)
                    _gameObjects[x].SetActive(true);
            }
        }

        if (isStatic)
        {
            if (isRandom)
            {
                foreach (var GO in _gameObjects)
                {
                    
                        GO.SetActive(false);
                }

                int x = UnityEngine.Random.Range(0, _gameObjects.Length);
            
                if(_gameObjects[x] != null)
                    _gameObjects[x].SetActive(true);
            }
        }

        if (isSpike)
        {
            foreach (GameObject spike in _gameObjects)
            {
                spike.SetActive(false);
                spike.transform.GetComponent<ParticleSystem>().Stop();
            }
        }

        if (isRolling)
        {
            transform.GetChild(0).transform.localPosition = new Vector3(0f, 0.8f, 0);
            
            if (isRandom)
            {
                foreach (var GO in _gameObjects)
                {
                    if(GO != null)
                        GO.SetActive(false);
                }

                int x = UnityEngine.Random.Range(0, _gameObjects.Length);
            
                if(_gameObjects[x] != null)
                    _gameObjects[x].SetActive(true);
            }
        }

        if (isFalling)
        {
            transform.localPosition = new Vector3(0f,200f,0);
            if (isRandom)
            {
                foreach (var GO in _gameObjects)
                {
                    if(GO != null)
                        GO.SetActive(false);
                }

                int x = UnityEngine.Random.Range(0, _gameObjects.Length);
            
                if(_gameObjects[x] != null)
                    _gameObjects[x].SetActive(true);
            }
        }
        
        if (isLeaf)
        {
            if (isRandom)
            {
                foreach (var GO in _gameObjects)
                {
                    if(GO != null)
                        GO.SetActive(false);
                }

                int x = UnityEngine.Random.Range(0, _gameObjects.Length);
            
                if(_gameObjects[x] != null)
                    _gameObjects[x].SetActive(true);
            }
        }

        if (isRamp)
        {
            foreach (var GO in _gameObjects)
            {
                if(GO != null)
                    GO.SetActive(false);
            }

            int x = UnityEngine.Random.Range(0, _gameObjects.Length);
            
            if(_gameObjects[x] != null)
                _gameObjects[x].SetActive(true);
        }
    }

    private void Start()
    {
        
        
        /*if (isRandom)
        {
            foreach (var GO in _gameObjects)
            {
                if(GO != null)
                    GO.SetActive(false);
            }

            int x = UnityEngine.Random.Range(0, _gameObjects.Length);
            
            if(_gameObjects[x] != null)
                _gameObjects[x].SetActive(true);
        }*/
    }

    public void RandomObject()
    {
       
    }
}
