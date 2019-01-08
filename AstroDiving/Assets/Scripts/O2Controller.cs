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

    private bool orbitingO2Planet;
    private bool outsideBoundaries;

    private void Awake()
    {
        currentO2 = startingO2;
        O2Slider.maxValue = startingO2;
        orbitingO2Planet = false;
    }

    // Use this for initialization
    void Start()
    {
        outsideBoundaries = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!orbitingO2Planet)
            currentO2 -= 1 * Time.deltaTime;
        else if (orbitingO2Planet && currentO2 < startingO2)
            currentO2 += 1 * Time.deltaTime;

        O2Slider.value = currentO2;

        if (currentO2 <= startingO2 / 4)
            fill.color = Color.red;
        else if (currentO2 <= startingO2 / 2)
            fill.color = new Color(1f, 0.5f, 0f, 1f);
        else
            fill.color = Color.green;

        if (outsideBoundaries)
            currentO2 -= 5 * Time.deltaTime;
    }
    
    public void SetOrbitingO2Planet(bool orbitingO2Planet)
    {
        this.orbitingO2Planet = orbitingO2Planet;
    }

    public bool O2IsGone()
    {
        return currentO2 <= 0;
    }

    public void SetOutsideBoundaries(bool outsideBoundaries)
    {
        this.outsideBoundaries = outsideBoundaries;
    }

    public bool GetOutsideBoundaries()
    {
        return outsideBoundaries;
    }
}

