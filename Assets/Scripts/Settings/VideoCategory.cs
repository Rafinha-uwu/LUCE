using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class VideoCategory : SettingsCategory
{
    [SerializeField] private TMP_Dropdown _modeDropdown;
    [SerializeField] private TMP_Dropdown _resolutionDropdown;
    [SerializeField] private UnityEngine.UI.Button _applyButton;

    private FullScreenMode[] _modes;
    private Resolution[] _resolutions;

    private FullScreenMode? _currentMode = null;
    private Resolution? _currentResolution = null;


    protected override void Awake()
    {
        base.Awake();

        if (_modeDropdown == null) throw new System.Exception("ModeDropdown is not set in the inspector");
        if (_resolutionDropdown == null) throw new System.Exception("ResolutionDropdown is not set in the inspector");
        if (_applyButton == null) throw new System.Exception("ApplyButton is not set in the inspector");

        SetupOptions();
        _applyButton.onClick.AddListener(Apply);
    }


    private void SetupOptions()
    {
        AddModes();
        AddResolutions();
    }

    private void AddModes()
    {
        _modes = (FullScreenMode[])System.Enum.GetValues(typeof(FullScreenMode));
        List<string> modeNames = _modes.Select(mode => mode.ToString()).ToList();

        _modeDropdown.ClearOptions();
        _modeDropdown.AddOptions(modeNames);
    }

    private void AddResolutions()
    {
        _resolutions = Screen.resolutions;
        List<string> resolutionNames = _resolutions.Select(resolution => resolution.ToString()).ToList();

        _resolutionDropdown.ClearOptions();
        _resolutionDropdown.AddOptions(resolutionNames);
    }


    public override void Open()
    {
        _currentMode ??= Screen.fullScreenMode;
        _currentResolution ??= Screen.currentResolution;

        _modeDropdown.value = (int)_currentMode.Value;
        _resolutionDropdown.value = _resolutions.ToList().IndexOf(_currentResolution.Value);

        base.Open();

        // Select the first dropdown
        _modeDropdown.Select();
    }

    private void Apply()
    {
        _currentMode = _modes[_modeDropdown.value];
        _currentResolution = _resolutions[_resolutionDropdown.value];

        Screen.SetResolution(
            _currentResolution.Value.width, _currentResolution.Value.height,
            _currentMode.Value, _currentResolution.Value.refreshRateRatio
        );
    }


    public override UnityEngine.UI.Button GetRightCloseButton() => _applyButton;
}
