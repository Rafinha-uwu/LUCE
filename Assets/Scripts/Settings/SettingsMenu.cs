using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    public event System.Action OnClose;

    [SerializeField] private Canvas _canvas;
    [SerializeField] private SettingsCategory[] _categories;


    private void Awake()
    {
        if (_canvas == null) throw new System.Exception("Canvas is not set in the inspector");

        foreach (var category in _categories) category.OnCategorySelected += () => OpenSettingsGroup(category);
    }

    private void Start() => Close();


    public void Open()
    {
        _canvas.enabled = true;

        // Open the first category by default
        if (_categories.Length > 0) OpenSettingsGroup(_categories[0]);
    }

    public void Close()
    {
        // Close all settings groups before closing the menu
        CloseGroups();
        _canvas.enabled = false;

        OnClose?.Invoke();
    }


    private void OpenSettingsGroup(SettingsCategory category)
    {
        // Close all groups before opening the selected one
        CloseGroups();
        category.Open();
    }

    private void CloseGroups()
    {
        // Close all settings groups
        foreach (var category in _categories) category.Close();
    }
}
