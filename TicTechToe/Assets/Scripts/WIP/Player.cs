﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : Character
{
    [SerializeField]
    private int money;
    public Inventory inventory;
    public static GameObject LocalPlayerInstance;

    //public delegate void MoneyChange();
    //public event Player MoneyChange;

    public void Awake()
    {
        money = 100;

        //keep track of the localPlayer instance to prevent instanciation when levels are synchronized
        if (photonView.IsMine)
        {
            LocalPlayerInstance = gameObject;
        }
    }

    public int getMoney()
    {
        return money;
    }

    public void setMoney(int m)
    {
        money = m;

        //MoneyChange();
    }
}
