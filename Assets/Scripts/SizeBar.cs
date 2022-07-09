using UnityEngine;
using UnityEngine.UI;

public class SizeBar : MonoBehaviour
{
    [SerializeField] Slider _slider;
    [SerializeField] Gradient _gradient;
    [SerializeField] Image _fill;

    public void SetMaxSize(float size)
    {
        _slider.maxValue = size;
        _slider.value = size;
        _fill.color = _gradient.Evaluate(1f);
    }

    public void SetSize(float size)
    {
        _slider.value = size;
        _fill.color = _gradient.Evaluate(_slider.normalizedValue);
    }
}