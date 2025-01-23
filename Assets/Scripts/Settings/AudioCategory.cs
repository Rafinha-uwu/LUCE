using System.Collections.Generic;
using UnityEngine;

public class AudioCategory : SettingsCategory
{
    [SerializeField] private TemplateAudioSettings _template;
    [SerializeField] private RectTransform _content;

    private FMODBusDatabase.NamedBus[] _namedBuses;
    private readonly List<TemplateAudioSettings> _options = new();

    [SerializeField] private UnityEngine.UI.Button _applyButton;


    protected override void Awake()
    {
        base.Awake();

        if (_content == null) throw new System.Exception("Content is not set in the inspector");
        if (_template == null) throw new System.Exception("Template is not set in the inspector");
        if (_applyButton == null) throw new System.Exception("ApplyButton is not set in the inspector");

        _template.Initialize();
    }

    private void Start()
    {
        SetupOptions();
        _applyButton.onClick.AddListener(Apply);
    }


    private void SetupOptions()
    {
        // Get buses from FMODBusDatabase
        _namedBuses = FMODManager.Instance.BusDatabase.Buses;

        // Create a text and slider for each bus (according to the template given)
        int i = 0;
        foreach (var namedBus in _namedBuses)
        {
            TemplateAudioSettings optionTemplate = Instantiate(_template, _content);
            
            optionTemplate.Initialize();
            optionTemplate.SetLabel(namedBus.BusName);
            optionTemplate.SetToNthPosition(i);
            optionTemplate.gameObject.SetActive(true);

            _options.Add(optionTemplate);
            i++;
        }

        // Set the content height according to the number of options
        float contentNewHeight = _template.GetHeight() * _namedBuses.Length;
        _content.sizeDelta = new(_content.sizeDelta.x, contentNewHeight);
    }


    public override void Open()
    {
        base.Open();

        // Set the values of the sliders
        for (int i = 0; i < _options.Count; i++)
        {
            TemplateAudioSettings option = _options[i];
            FMODBusDatabase.NamedBus namedBus = _namedBuses[i];

            int volume = namedBus.GetBusVolume();
            option.SetSliderValue(volume);
        }
    }

    private void Apply()
    {
        // Set the values of the buses
        for (int i = 0; i < _options.Count; i++)
        {
            TemplateAudioSettings option = _options[i];
            FMODBusDatabase.NamedBus namedBus = _namedBuses[i];

            int volume = option.GetSliderValue();
            namedBus.SetBusVolume(volume);
        }
    }
}
