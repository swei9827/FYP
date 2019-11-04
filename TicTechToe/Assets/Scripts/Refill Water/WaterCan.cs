using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterCan : MonoBehaviour
{
    public Image waterBar;

    public static float maxFill = 3f;
    public static float curFill;
    public static bool isFull;
    float calculateFill;

    private void Start()
    {
        curFill = maxFill;
    }

    // Update is called once per frame
    void Update()
    {
        totalFill();
        if(curFill == maxFill)
        {
            isFull = true;
        }
        else if(curFill != maxFill)
        {
            isFull = false;
        }
    }

    void totalFill()
    {
        calculateFill = curFill / maxFill;
        SetFill(calculateFill);
    }

    void SetFill(float fillUp)
    {
        waterBar.fillAmount = fillUp;
    }
}
