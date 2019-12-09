using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BrightnessSetting : MonoBehaviour
{
    public static BrightnessSetting instance;

    public Image brightnessImage;
    private Color tempColor;
    private GameObject temp;

    private Slider menuBrightnessSlider;
    private Slider gameBrightnessSlider;

    private GameObject Canvas;

    private Scene currentScene;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        brightnessImage = this.GetComponentInChildren<Image>();
        tempColor = brightnessImage.color;
    }

    public void setBrightness(float value)
    {
        tempColor = brightnessImage.color;
        tempColor.a = 1- value;
        gameObject.transform.GetChild(0).GetComponent<Image>().color = tempColor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
