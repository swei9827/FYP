using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperPanel : MonoBehaviour
{
    public Canvas HelperCanvas;
    public GameObject instructions;
    public GameObject control;

    public void ActivateButton()
    {
        HelperCanvas.gameObject.SetActive(true);
    }

    public void NextButton()
    {
        instructions.SetActive(false);
        control.SetActive(true);
    }

    public void BackButton()
    {
        HelperCanvas.gameObject.SetActive(false);
        instructions.SetActive(true);
        control.SetActive(false);
    }

}
