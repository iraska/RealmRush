using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int goldReward = 25;
    [SerializeField] int goldPenalty = 25;

    Bank bank;

    void Start() 
    {
        bank = FindObjectOfType<Bank>();
    }

    // When kill enemy you earn 25
    public void RewardGold()
    {
        if (bank == null) { return; }
        bank.Deposit(goldReward);
    }

    // When enemy arrive the end point you lost 25
    public void StealGold()
    {
        if (bank == null) { return; }
        bank.Withdraw(goldPenalty);
    }
}
