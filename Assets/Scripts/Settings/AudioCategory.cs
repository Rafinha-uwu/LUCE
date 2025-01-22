using UnityEngine;

public class AudioCategory : SettingsCategory
{
    [SerializeField] private TemplateAudioSettings _template;
    [SerializeField] private UnityEngine.UI.Button _applyButton;


    protected override void Awake()
    {
        base.Awake();

        if (_template == null) throw new System.Exception("Template is not set in the inspector");
        if (_applyButton == null) throw new System.Exception("ApplyButton is not set in the inspector");
    }

    private void Start()
    {
        SetupOptions();
        _applyButton.onClick.AddListener(Apply);
    }


    private void SetupOptions()
    {
        // Get buses from FMODBusDatabase

        // Create a text and slider for each bus (according to the template given)
    }


    public override void Open()
    {
        base.Open();

        // Set the values of the sliders
    }

    private void Apply()
    {
        // Set the values of the buses
    }
}
