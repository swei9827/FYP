using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterCan : MonoBehaviour
{
    public Image waterBar;

    public static float maxFill = 10f;
    public static float curFill;
    float calculateFill;

    private void Start()
    {
        curFill = maxFill;
    }

    // Update is called once per frame
    void Update()
    {
        totalFill();
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
