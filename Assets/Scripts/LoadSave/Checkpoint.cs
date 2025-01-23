using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Checkpoint : MonoBehaviour, ISavable
{
    protected static readonly string PLAYER_TAG = "Player";

    [SerializeField] protected Checkpoint[] _dependencies;
    protected bool _checkpointReached = false;

    [SerializeField] protected GameObject[] _objects;
    protected readonly List<ISavable> _savables = new();

    protected Dictionary<string, object> _savedData;


    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        // If the checkpoint has already been reached or the collision isn't with the player, return
        if (_checkpointReached || !collision.CompareTag(PLAYER_TAG)) return;

        // Check if all dependencies have been reached - if so, call OnReached
        bool allDependenciesReached = _dependencies.Length == 0 || _dependencies.All(dependency => dependency._checkpointReached);
        if (allDependenciesReached) OnReached();
    }

    protected virtual void Awake()
    {
        // Get all savables (from the objects array)
        foreach (GameObject obj in _objects)
        {
            if (obj == null) throw new System.Exception("Object is set to null in the Checkpoint component");

            bool hasSavable = obj.TryGetComponent(out ISavable savable);
            if (hasSavable) _savables.Add(savable);
            else Debug.LogWarning($"Object {obj.name} in Checkpoint {name} does not have a savable component");
        }

        // Add this checkpoint to the list of savables
        if (!_savables.Contains(this)) _savables.Add(this);

        // Check if this checkpoint is the last one reached
        if (SaveManager.Instance.LastCheckpointName == GetSaveName()) SaveManager.Instance.SetLastCheckpoint(this);

        // Load the data for this checkpoint
        Load(false);
    }

    protected virtual void OnReached()
    {
        // Set the last checkpoint to this one + save the data
        _checkpointReached = true;
        SaveManager.Instance.SetLastCheckpoint(this);
        Save();
    }


    public virtual void Load(bool loadDependencies = true)
    {
        // Use the saved data if it's already loaded, otherwise load it from the file
        _savedData ??= SaveManager.Instance.LoadFromFile(GetSaveName());
        if (_savedData == null) return;

        // Load the data for each savable
        _savables.ForEach(obj => {
            string saveName = obj.GetSaveName();
            if (_savedData.TryGetValue(saveName, out object data)) obj.LoadData(data);
        });

        // Load the dependencies (recursively) if loadDependencies is true
        if (loadDependencies) foreach (var dependency in _dependencies) dependency.Load();
    }

    public virtual void Save(bool saveDependencies = true)
    {
        // Get the save data for each savable + save it to the file
        _savedData = _savables.ToDictionary(obj => obj.GetSaveName(), obj => obj.GetSaveData());
        SaveManager.Instance.SaveToFile(GetSaveName(), _savedData);

        // Only save the dependencies (and only them) if saveDependencies is true
        foreach (var dependency in _dependencies) dependency.Save(false);
    }


    // ISavable implementation
    public virtual string GetSaveName() => name;
    public virtual object GetSaveData() => _checkpointReached;
    public virtual void LoadData(object data) => _checkpointReached = (bool)data;
}
