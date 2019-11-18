using System.Collections;
using System.Collections.Generic;

public class Player : Character
{
    private int money;
    public Inventory inventory;

    public void Awake()
    {
        money = 50;
    }

    public int getMoney()
    {
        return money;
    }

    public void setMoney(int m)
    {
        money = m;
    }
}
