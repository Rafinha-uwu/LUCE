using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "FMODBusDatabase", menuName = "Sound/FMODBusDatabase")]
public class FMODBusDatabase : ScriptableObject
{
    [SerializeField] private string[] _busPaths;

    // Dictionary to store the buses by their path
    public Dictionary<string, Bus> Buses = new();


    public void Initialize()
    {
        foreach (string path in _busPaths)
        {
            // Throws an exception the bus doesn't exist
            Bus bus = RuntimeManager.GetBus(path);
            Buses.Add(path, bus);
        }
    }
}
