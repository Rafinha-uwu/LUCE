using UnityEngine;
using System.Linq;
using Newtonsoft.Json;

public class Code : SwitchWithRequirements
{
    [SerializeField] protected CodeSprite[] _codeSprites;
    [SerializeField] protected int _minimumChanges;
    [SerializeField] protected int _maximumChanges;

    protected override void Start()
    {
        // Check if we have the same amount of requirements and code sprites
        if (_requirements.Length != _codeSprites.Length) throw new System.Exception("Requirements and Code sprites must have the same length");

        // Min and max changes must be between 0 and the amount of requirements
        _minimumChanges = Mathf.Clamp(_minimumChanges, 0, _requirements.Length);
        _maximumChanges = Mathf.Clamp(_maximumChanges, _minimumChanges, _requirements.Length);
        Randomizer();

        base.Start();
    }

    protected virtual void UpdateLevelSprites(Requirement requirement, CodeSprite codeSprite)
    {
        codeSprite.Down.enabled = !requirement.RequiredState;
        codeSprite.Up.enabled = requirement.RequiredState;
    }

    public void Randomizer()
    {
        // Randomize the number of changes and the requirements to change
        int changes = Random.Range(_minimumChanges, _maximumChanges + 1);
        Requirement[] requirementsToChange = _requirements.OrderBy(x => Random.value).Take(changes).ToArray();

        // Change the requirements and force a state change
        foreach (var req in _requirements) req.RequiredState = requirementsToChange.Contains(req);
        OnSwitchStateChange(null, false);

        // Update the sprites
        for (int i = 0; i < _requirements.Length; i++) UpdateLevelSprites(_requirements[i], _codeSprites[i]);
    }


    [System.Serializable]
    public class CodeSprite
    {
        public SpriteRenderer Up;
        public SpriteRenderer Down;
    }


    public override object GetSaveData() => new CodeSaveData {
        IsOn = IsOn,
        RequiredStates = _requirements.Select(x => x.RequiredState).ToArray()
    };

    public override void LoadData(object data)
    {
        CodeSaveData saveData = JsonConvert.DeserializeObject<CodeSaveData>(data.ToString());

        // Get the current and saved required states
        bool[] savedRequiredStates = saveData.RequiredStates;
        bool[] currentRequiredStates = _requirements.Select(x => x.RequiredState).ToArray();

        // If the lengths are different, we can't load the data - force the switch to be off
        if (savedRequiredStates.Length != currentRequiredStates.Length)
        {
            SwitchTo(false);
            return;
        }

        // If the saved state is on, set the current required states to the saved required states
        if (saveData.IsOn)
        {
            for (int i = 0; i < _requirements.Length; i++)
            {
                _requirements[i].RequiredState = savedRequiredStates[i];
                UpdateLevelSprites(_requirements[i], _codeSprites[i]);
            }
            OnSwitchStateChange(null, false);
        }
        else Randomizer();

        // Turn switch to the saved state
        SwitchTo(saveData.IsOn);
    }


    [System.Serializable]
    public class CodeSaveData
    {
        public bool IsOn;
        public bool[] RequiredStates;
    }
}
