using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private int money;
    public Animator anm;
    public SpriteRenderer spriteRdr;
    public Rigidbody2D rb;

    public int getMoney()
    {
        return money;
    }

    public void setMoney(int m)
    {
        money = m;
    }
}
