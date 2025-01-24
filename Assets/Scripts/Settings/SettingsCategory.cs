using UnityEngine;

public class SettingsCategory : MonoBehaviour
{
    public event System.Action OnCategorySelected;

    [SerializeField] protected UnityEngine.UI.Button _categoryButton;
    [SerializeField] protected GameObject _settingsGroup;


    protected virtual void Awake()
    {
        if (_categoryButton == null) throw new System.Exception("Category button is not set in the inspector");
        if (_settingsGroup == null) throw new System.Exception("Settings group is not set in the inspector");

        _categoryButton.onClick.AddListener(() => OnCategorySelected?.Invoke());
    }


    public virtual void Open()
    {
        _settingsGroup.SetActive(true);
        _categoryButton.interactable = false;
    }

    public virtual void Close()
    {
        _settingsGroup.SetActive(false);
        _categoryButton.interactable = true;
    }
}
