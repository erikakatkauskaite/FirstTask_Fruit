using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetProgressBar : MonoBehaviour
{
    private Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();
        slider.value = 0;
    }

    public void SetMaxBerries(int max)
    {
        slider.maxValue = max;
    }

    public void SetProgress(int berryCount)
    {
        slider.value = berryCount;
    }
}
