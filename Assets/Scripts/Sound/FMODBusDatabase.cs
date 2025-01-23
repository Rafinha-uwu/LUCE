using UnityEngine;
using FMODUnity;
using FMOD.Studio;

[CreateAssetMenu(fileName = "FMODBusDatabase", menuName = "Sound/FMODBusDatabase")]
public class FMODBusDatabase : ScriptableObject
{
    [field: SerializeField] public NamedBus[] Buses { get; private set; }

    public void Initialize()
    {
        foreach (var bus in Buses)
        {
            if (bus == null) throw new System.Exception("Bus is not set in the inspector");
            bus.Initialize();
        }
    }


    [System.Serializable]
    public class NamedBus
    {
        [field: SerializeField] public string BusName { get; private set; }
        [field: SerializeField] public string BusPath { get; private set; }
        public Bus Bus { get; private set; }

        public void Initialize()
        {
            // Throws an exception the bus doesn't exist
            Bus = RuntimeManager.GetBus(BusPath);
        }
    }
}
