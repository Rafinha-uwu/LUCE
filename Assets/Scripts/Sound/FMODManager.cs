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

    private bool _isPaused = false;


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
        if (_isPaused) return;
        RuntimeManager.PlayOneShot(eventReference, position);
    }

    public EventInstance? CreateEventInstance(EventReference eventReference)
    {
        try
        {
            EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
            eventInstance.setPaused(_isPaused);

            _eventInstances.Add(eventInstance);
            return eventInstance;
        }
        catch (System.Exception) {}
        return null;
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

        eventEmitter.EventInstance.setPaused(_isPaused);

        _eventEmitters.Add(eventEmitter);
        return eventEmitter;
    }

    public void PlayOneShotAttached(EventReference eventReference, GameObject gameObject)
    {
        if (_isPaused) return;
        RuntimeManager.PlayOneShotAttached(eventReference, gameObject);
    }

    public void AttachInstance(EventInstance eventInstance, Transform transform, Rigidbody2D rb = null)
    {
        RuntimeManager.AttachInstanceToGameObject(eventInstance, transform, rb);
    }


    private void OnDestroy()
    {
        // Stop and release all event instances
        _eventInstances.ForEach(e => {
            e.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            e.release();
        });

        // Stop all event emitters
        _eventEmitters.ForEach(e => e.Stop());
    }


    public void PauseSounds() => SetPaused(true);
    public void ResumeSounds() => SetPaused(false);

    private void SetPaused(bool paused)
    {
        _isPaused = paused;
        _eventInstances.ForEach(e => e.setPaused(_isPaused));
        _eventEmitters.ForEach(e => e.EventInstance.setPaused(_isPaused));
    }
}
