using FMODUnity;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightEffects : MonoBehaviour
{
    [Header("Flicker Settings")]
    public bool flicker = true;
    [SerializeField] private float LightTimer = 10;
    [SerializeField] private float MaxItensity = 3f;
    [SerializeField] private float DarknessTimer = 1f;

    private const float DIM_INTENSITY = 1f;
    private const float OFF_INTENSITY = 0f;

    private const float SHORT_FLICKER_DURATION = 0.2f;
    private const float QUICK_FLICKER_DURATION = 0.1f;
    private const float POST_FLICKER_DELAY = 0.5f;


    [Header("Flicker Randomizer")]
    public bool RandomTime = true;
    [SerializeField] private float Min = 7;
    [SerializeField] private float Max = 15;


    [Header("Wigle Settings")]
    public bool wigle = true;
    [SerializeField] private float speed = 1;
    private bool wigle2 = true;
    private bool dir = true;

    private const float WIGGLE_DURATION = 4f;
    private const float WIGGLE_HALF_DURATION = WIGGLE_DURATION / 2;


    // Light + Sound
    private Light2D _light2D;
    private static readonly string EVENT_PARAMETER_IS_ON = "IsOn";
    private StudioEventEmitter _lightSound;
    [SerializeField] private float _soundMinDistance = 0f;
    [SerializeField] private float _soundMaxDistance = 10f;


    private void Awake()
    {
        _light2D = GetComponentInChildren<Light2D>();
    }

    private void Update()
    {
        if (Time.timeScale == 0) return;

        if (flicker)
        {
            StartCoroutine(Flick());
            if (RandomTime) LightTimer = Random.Range(Min, Max);
        }

        if (wigle2) StartCoroutine(Wig());
        if (wigle)
        {
            int leftOrRight = dir ? 1 : -1;
            transform.Rotate(0, 0, speed * leftOrRight);
        }
    }


    private IEnumerator Flick()
    {
        flicker = false;
        _light2D.intensity = MaxItensity;

        yield return new WaitForSeconds(LightTimer);
        PlayLightSound(false);
        _light2D.intensity = DIM_INTENSITY;

        yield return new WaitForSeconds(SHORT_FLICKER_DURATION);
        _light2D.intensity = MaxItensity;

        yield return new WaitForSeconds(QUICK_FLICKER_DURATION);
        _light2D.intensity = OFF_INTENSITY;

        yield return new WaitForSeconds(DarknessTimer);
        PlayLightSound(true);
        _light2D.intensity = DIM_INTENSITY;

        yield return new WaitForSeconds(POST_FLICKER_DELAY);
        _light2D.intensity = MaxItensity;
        flicker = true;
    }

    private IEnumerator Wig()
    {
        wigle2 = false;

        dir = true;
        yield return new WaitForSeconds(WIGGLE_HALF_DURATION);

        dir = false;
        yield return new WaitForSeconds(WIGGLE_DURATION);

        dir = true;
        yield return new WaitForSeconds(WIGGLE_HALF_DURATION);

        wigle2 = true;
    }


    private void Start()
    {
        _lightSound = GetLightSound();
    }

    private StudioEventEmitter GetLightSound()
    {
        return FMODManager.Instance.CreateEventEmitter(
            FMODManager.Instance.EventDatabase.Light,
            gameObject,
            _soundMinDistance, _soundMaxDistance
        );
    }

    public void PlayLightSound(bool isOn)
    {
        _lightSound.Play();
        FMODManager.Instance.AttachInstance(_lightSound.EventInstance, transform);
        _lightSound.SetParameter(EVENT_PARAMETER_IS_ON, isOn ? 1 : 0);
    }
}
