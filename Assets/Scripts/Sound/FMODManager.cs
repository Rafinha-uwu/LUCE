using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using System.Collections.Generic;

public class FMODManager : MonoBehaviour
{
    public static FMODManager Instance { get; private set; }

    [field: SerializeField] public FMODEventDatabase EventDatabase { get; private set; }
    [field: SerializeField] public FMODBusDatabase BusDatabase { get; private set; }

    private readonly List<EventInstance> _eventInstances = new();
    private readonly List<StudioEventEmitter> _eventEmitters = new();


    private void Awake()
    {
        // Singleton pattern
        if (Instance != null) Destroy(Instance.gameObject);
        Instance = this;

        // Check if the databases are assigned
        if (EventDatabase == null) throw new System.Exception("EventDatabase is not assigned");
        else if (BusDatabase == null) throw new System.Exception("BusDatabase is not assigned");

        // Initialize the databases
        BusDatabase.Initialize();
    }


    public void PlayOneShot(EventReference eventReference, Vector3 position = default)
    {
        RuntimeManager.PlayOneShot(eventReference, position);
    }

    public EventInstance CreateEventInstance(EventReference eventReference)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);

        _eventInstances.Add(eventInstance);
        return eventInstance;
    }

    public StudioEventEmitter CreateEventEmitter(EventReference eventReference, GameObject gameObject, float minDistance, float maxDistance)
    {
        // Get/Create the StudioEventEmitter component
        bool emitterExists = gameObject.TryGetComponent(out StudioEventEmitter eventEmitter);
        if (!emitterExists) eventEmitter = gameObject.AddComponent<StudioEventEmitter>();

        // Set the event reference and distance overrides
        eventEmitter.EventReference = eventReference;
        eventEmitter.OverrideMinDistance = minDistance;
        eventEmitter.OverrideMaxDistance = maxDistance;

        _eventEmitters.Add(eventEmitter);
        return eventEmitter;
    }


    private void OnDestroy()
    {
        // Stop and release all event instances
        foreach (EventInstance eventInstance in _eventInstances)
        {
            eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            eventInstance.release();
        }

        // Stop all event emitters
        foreach (StudioEventEmitter eventEmitter in _eventEmitters)
        {
            eventEmitter.Stop();
        }
    }
}
