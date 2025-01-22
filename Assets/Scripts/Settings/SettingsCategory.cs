using UnityEngine;

public class SettingsCategory : MonoBehaviour
{
    public event System.Action OnCategorySelected;

    [SerializeField] private UnityEngine.UI.Button _categoryButton;
    [SerializeField] private GameObject _settingsGroup;


    private void Awake()
    {
        if (_categoryButton == null) throw new System.Exception("Category button is not set in the inspector");
        if (_settingsGroup == null) throw new System.Exception("Settings group is not set in the inspector");

        _categoryButton.onClick.AddListener(() => OnCategorySelected?.Invoke());
    }


    public void Open()
    {
        _settingsGroup.SetActive(true);
        _categoryButton.interactable = false;
    }

    public void Close()
    {
        _settingsGroup.SetActive(false);
        _categoryButton.interactable = true;
    }
}
