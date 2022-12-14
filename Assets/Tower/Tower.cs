using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] int cost = 75;
    [SerializeField] float buildDelay = 1f;

    void Start() 
    {
        StartCoroutine(Build());
    }

    public bool CreateTower(Tower tower, Vector3 position)
    {
        Bank bank = FindObjectOfType<Bank>();

        if (bank == null)
        {
            return false;
        }

        if (bank.CurrentBalance >= cost)
        {
            Instantiate(tower, position, Quaternion.identity);
            bank.Withdraw(cost);
            return true;
        }

        return false;
    }

    IEnumerator Build()
    {
        //turn off all children and grandchilderen in hierarchy
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);

            foreach (Transform grandchild in transform)
            {
                grandchild.gameObject.SetActive(false);
            }
        }

        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);   //first child to be active: tower; second: tower top
            yield return new WaitForSeconds(buildDelay);    //then wait buildDelay time
            foreach (Transform grandchild in transform)     //gets skip (first time); second: particle system
            {
                grandchild.gameObject.SetActive(true);
            }
        }
    }
}
