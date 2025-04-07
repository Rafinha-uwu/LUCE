using UnityEngine;
using TMPro;

public class CountColect : MonoBehaviour
{
    [field: SerializeField] public int MaxColect { get; private set; } = 6;
    public int nColect = 0;

    private TextMeshProUGUI tmpText;
    public TextMeshProUGUI pauseText;


    private void Awake()
    {
        tmpText = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Update()
    {
        string text = $"{nColect}/{MaxColect}";
        tmpText.text = text;
        pauseText.text = text;
    }
}
