using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class O2Controller : MonoBehaviour
{
    public float startingO2;
    public float currentO2;
    public Slider O2Slider;
    public Image fill;
    
    private void Awake()
    {
        currentO2 = startingO2;
        O2Slider.maxValue = startingO2;
    }

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        currentO2 -= 1 * Time.deltaTime;
        O2Slider.value = currentO2;

        if (currentO2 <= startingO2 / 2)
            fill.color = new Color(1f, 0.5f, 0f, 1f);
        if (currentO2 <= startingO2 / 4)
            fill.color = Color.red;
    }
    
}

