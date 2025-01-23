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

        private string PlayerPrefsKey => $"{BusName}Volume";


        public void Initialize()
        {
            // Throws an exception the bus doesn't exist
            Bus = RuntimeManager.GetBus(BusPath);

            // Set the volume to the value stored in PlayerPrefs
            if (PlayerPrefs.HasKey(PlayerPrefsKey))
            {
                float volume = PlayerPrefs.GetFloat(PlayerPrefsKey);
                Bus.setVolume(volume);
            }
            // Set the value stored in PlayerPrefs to the volume
            else
            {
                Bus.getVolume(out float volume);
                PlayerPrefs.SetFloat(PlayerPrefsKey, volume);
            }
        }


        public void SetBusVolume(int volume)
        {
            float normalizedVolume = Mathf.InverseLerp(0, 100, volume);

            // Store the volume in PlayerPrefs + Set the volume
            PlayerPrefs.SetFloat(PlayerPrefsKey, normalizedVolume);
            Bus.setVolume(normalizedVolume);
        }

        public int GetBusVolume()
        {
            // Get the volume from PlayerPrefs
            float normalizedVolume = PlayerPrefs.GetFloat(PlayerPrefsKey, 1f);

            float volume = Mathf.Lerp(0, 100, normalizedVolume);
            return Mathf.RoundToInt(volume);
        }
    }
}
