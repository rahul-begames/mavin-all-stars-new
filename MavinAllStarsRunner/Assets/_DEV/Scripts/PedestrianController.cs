/*
using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PedestrianController : MonoBehaviour
{
    [SerializeField] private GameObject[] allPedestrians;
    [SerializeField]private bool isRun, isWalk,Japan,Generic;
    
    [SerializeField] private GameObject[] japanP,genericP;
    
    [SerializeField]private List<GameObject> selectedP = new List<GameObject>();
    private void OnEnable()
    {
        foreach (var allPedestrian in allPedestrians)
        {
            allPedestrian.SetActive(false);
        }

        if (Japan)
        {
            foreach (var jp in japanP)
            {
                selectedP.Add(jp);
            }
        }
        
        if (Generic)
        {
            foreach (var gp in genericP)
            {
                selectedP.Add(gp);
            }
        }

        if (selectedP != null)
        {
            int randomGirlNumber = UnityEngine.Random.Range(0, selectedP.Count);
        
            if(selectedP[randomGirlNumber]!=null)
                selectedP[randomGirlNumber].SetActive(true);
        }
        
       
        
        if (isRun)
        {
            Run();
        }
        
        if (isWalk)
        {
            Walk();
        }

      
    }
    
    void Walk()
    {
        transform.DOLocalMoveZ(-6.2f, 5f).SetEase(Ease.Linear).SetRelative(true).OnComplete(delegate
        {
            transform.DOLocalRotate(new Vector3(0f, 0f, 0f), 0f);
            transform.DOLocalMoveZ(6.2f, 5f).SetEase(Ease.Linear).SetRelative(true).OnComplete((() =>

                    transform.DOLocalRotate(new Vector3(0f, 180f, 0f), 0f).OnComplete(Walk)

                ));
        });
    }
    
    void Run()
    {
        transform.DOLocalMoveZ(-6.2f, 3f).SetEase(Ease.Linear).SetRelative(true).OnComplete(delegate
        {
            transform.DOLocalRotate(new Vector3(0f, 0f, 0f), 0f);
            transform.DOLocalMoveZ(6.2f, 3f).SetEase(Ease.Linear).SetRelative(true).OnComplete((() =>

                    transform.DOLocalRotate(new Vector3(0f, 180f, 0f), 0f).OnComplete(Run)

                ));
        });
    }
}
*/


using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PedestrianController : MonoBehaviour
{
    [SerializeField] private GameObject[] allPedestrians;
    [SerializeField] private bool isRun, isWalk, Generic;
    [SerializeField] private GameObject[] japanP, genericP;

    private List<GameObject> selectedP = new List<GameObject>();


    public float moveValue = 6.2f;
    private void OnEnable()
    {
        foreach (var allPedestrian in allPedestrians)
        {
            allPedestrian.SetActive(false);
        }
        
        if (Generic)
        {
            selectedP.AddRange(genericP);
        }

        if (selectedP.Count > 0)
        {
            int randomGirlNumber = Random.Range(0, selectedP.Count);

            if (selectedP[randomGirlNumber] != null)
                selectedP[randomGirlNumber].SetActive(true);
        }

        if (isRun)
        {
            Run();
        }

        if (isWalk)
        {
            Walk();
        }
    }

    void Walk()
    {
        transform.DOLocalMoveZ(-moveValue, 5f).SetEase(Ease.Linear).SetRelative(true).OnComplete(delegate
        {
            transform.DOLocalRotate(new Vector3(0f, 0f, 0f), 0f);
            transform.DOLocalMoveZ(moveValue, 5f).SetEase(Ease.Linear).SetRelative(true).OnComplete((() =>

                    transform.DOLocalRotate(new Vector3(0f, 180f, 0f), 0f).OnComplete(Walk)

                ));
        });
    }
    
    void Run()
    {
        transform.DOLocalMoveZ(-moveValue, 3f).SetEase(Ease.Linear).SetRelative(true).OnComplete(delegate
        {
            transform.DOLocalRotate(new Vector3(0f, 0f, 0f), 0f);
            transform.DOLocalMoveZ(moveValue, 3f).SetEase(Ease.Linear).SetRelative(true).OnComplete((() =>

                    transform.DOLocalRotate(new Vector3(0f, 180f, 0f), 0f).OnComplete(Run)

                ));
        });
    }
}

