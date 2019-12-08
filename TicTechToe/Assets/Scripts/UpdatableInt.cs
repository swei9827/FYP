using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdatableInt : MonoBehaviour
{
    private Text textComponent;
    private string content;
    public int temp;
    public int max = 100;

    private void Start()
    {
        textComponent = this.GetComponent<Text>();
    }

    public void Plus()
    {
        temp = int.Parse(textComponent.text);
        if(temp < max)
        {
            temp++;
        }        
        textComponent.text = temp.ToString();
    }

    public void Minus()
    {
        temp = int.Parse(textComponent.text);
        if (temp > 1)
        {
            temp--;
        }
        textComponent.text = temp.ToString();
    }
}
