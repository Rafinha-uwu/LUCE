using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TemplateAudioSettings : MonoBehaviour
{
    [SerializeField] private TMP_Text _labelText;
    [SerializeField] private Slider _slider;

    private RectTransform _rectTransform;
    private float _initialYPosition;


    public void Initialize()
    {
        if (_labelText == null) throw new System.Exception("Label Text is not assigned in the inspector");
        if (_slider == null) throw new System.Exception("Slider is not assigned in the inspector");

        // Get the RectTransform + Set initial Y position
        bool hasRectTransform = TryGetComponent(out _rectTransform);
        if (!hasRectTransform) throw new System.Exception("RectTransform is not found in the GameObject");

        _initialYPosition = _rectTransform.anchoredPosition.y;

        // Set slider between 0% and 100% without decimals
        _slider.minValue = 0;
        _slider.maxValue = 100;
        _slider.wholeNumbers = true;
    }


    public string GetLabel() => _labelText.text;
    public int GetSliderValue() => Mathf.RoundToInt(_slider.value);

    public void SetLabel(string text) => _labelText.text = text;
    public void SetSliderValue(int value) => _slider.value = value;


    public float GetHeight() => _rectTransform.rect.height;
    public void SetToNthPosition(int n)
    {
        float y = _initialYPosition - n * GetHeight();
        _rectTransform.anchoredPosition = new Vector2(_rectTransform.anchoredPosition.x, y);
    }
}
