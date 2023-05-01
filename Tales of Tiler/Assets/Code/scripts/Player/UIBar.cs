using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBar : MonoBehaviour
{
    public Slider barSlider;

    public void SetValue(int value)
    {
        barSlider.value = value;
    }

    public void SetMaxValue(int value)
    {
        barSlider.maxValue = value;
    }
}
