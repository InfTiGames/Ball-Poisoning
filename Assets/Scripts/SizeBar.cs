using UnityEngine;
using UnityEngine.UI;

public class SizeBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Gradient gradient;
    [SerializeField] private Image fill;

    public void SetMaxSize(float size)
    {
        slider.maxValue = size;
        slider.value = size;
        fill.color = gradient.Evaluate(1f);
    }

    public void SetSize(float size)
    {
        slider.value = size;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}