using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public event System.Action OnClose;

    [SerializeField] private Canvas _canvas;
    [SerializeField] private SettingsCategory[] _categories;
    [SerializeField] private UnityEngine.UI.Button _closeButton;


    private void Awake()
    {
        if (_canvas == null) throw new System.Exception("Canvas is not set in the inspector");
        if (_closeButton == null) throw new System.Exception("CloseButton is not set in the inspector");

        foreach (var category in _categories)
        {
            if (category == null) throw new System.Exception("Category is not set in the inspector");
            category.OnCategorySelected += () => OpenSettingsGroup(category);
        }
    }

    private void Start() => Close();


    public void Open()
    {
        _canvas.enabled = true;

        // Open the first category by default
        if (_categories.Length > 0) OpenSettingsGroup(_categories[0]);
        else _closeButton.Select();
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

        // Update the close button navigation
        UpdateCloseButtonNavigation(category);
    }

    private void CloseGroups()
    {
        // Close all settings groups
        foreach (var category in _categories) category.Close();
    }


    private void UpdateCloseButtonNavigation(SettingsCategory category)
    {
        UnityEngine.UI.Button buttonOnRight = category.GetRightCloseButton();

        Navigation nav = new()
        {
            mode = Navigation.Mode.Explicit,
            selectOnUp = _categories.Last(sc => !sc.IsOpen).CategoryButton,
            selectOnRight = buttonOnRight != null ? buttonOnRight : _closeButton
        };

        _closeButton.navigation = nav;
    }
}
