using FMODUnity;
using Newtonsoft.Json;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PuzzleEye : MonoBehaviour, ISavable
{
    private static readonly string PLAYER_TAG = "Player";

    // References to the colliders
    public Collider2D collider1;
    public Collider2D collider2;
    public Collider2D collider3;

    public float Phase = 0;

    public Light2D L1;
    public Light2D L11;

    public Light2D L2;
    public Light2D L22;

    public Light2D L3;
    public Light2D L33;

    private StudioEventEmitter _l1Sound;
    private StudioEventEmitter _l2Sound;
    private StudioEventEmitter _l3Sound;
    [SerializeField] private float _soundMinDistance = 0f;
    [SerializeField] private float _soundMaxDistance = 10f;

    [SerializeField] private PuzzleEyeDoors _doors;

    public GameObject Eye1;
    public GameObject Eye2;
    private Animator _eye1Animator;
    private Animator _eye2Animator;

    private Color NormalColor;


    private void Awake()
    {
        // Ensure all colliders are trigger colliders
        collider1.isTrigger = true;
        collider2.isTrigger = true;
        collider3.isTrigger = true;

        NormalColor = L1.color;

        _eye1Animator = Eye1.GetComponent<Animator>();
        _eye2Animator = Eye2.GetComponent<Animator>();
    }


    public void StartPuzzle()
    {
        CancelInvoke();
        StopAllCoroutines();

        L1.color = NormalColor;
        L11.color = NormalColor;
        L2.color = NormalColor;
        L22.color = NormalColor;
        L3.color = NormalColor;
        L33.color = NormalColor;

        _doors.DoorDown();
        EyeStart();

        Invoke(nameof(FOff1), 0);
        Invoke(nameof(FOn2), 0);
        Invoke(nameof(FOff3), 0);
        Invoke(nameof(FOff2), 5);
        Invoke(nameof(FOn1), 5);
        Invoke(nameof(EyeL), 5);
        Phase = 1.1f;
    }

    public void ResetPuzzle()
    {
        L1.color = NormalColor;
        L11.color = NormalColor;
        L2.color = NormalColor;
        L22.color = NormalColor;
        L3.color = NormalColor;
        L33.color = NormalColor;

        EyeNone();
        _doors.DoorStart();

        Invoke(nameof(FOff1), 0);
        Invoke(nameof(FOn2), 0);
        Invoke(nameof(FOff3), 0);
    }

    private void EndPuzzle()
    {
        L1.color = Color.green;
        L11.color = Color.green;
        L2.color = Color.green;
        L22.color = Color.green;
        L3.color = Color.green;
        L33.color = Color.green;

        Invoke(nameof(FOn1), 0);
        Invoke(nameof(FOn2), 0);
        Invoke(nameof(FOn3), 0);
        Invoke(nameof(DoorUp), 1.5f);
        Phase = 4f;
    }


    #region Phases Logic
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag(PLAYER_TAG)) return;

        // Check which collider was triggered
        if (collision.IsTouching(collider1)) HandleCollider1Trigger(collision);
        else if (collision.IsTouching(collider2)) HandleCollider2Trigger(collision);
        else if (collision.IsTouching(collider3)) HandleCollider3Trigger(collision);
    }

    private void HandleCollider1Trigger(Collider2D _)
    {
        switch (Phase)
        {
            case 1.1f:
                Invoke(nameof(FOff1), 1);
                Invoke(nameof(FOn3), 1);
                Invoke(nameof(EyeR), 1);
                Phase = 1.2f;
                break;

            case 2.1f:
                Invoke(nameof(FOn1), 0);
                Invoke(nameof(FOff1), 2);
                Invoke(nameof(EyeR), 0);
                Phase = 2.2f;
                break;

            case 2.3f:
                Invoke(nameof(FOn1), 0);
                Invoke(nameof(FOff1), 1);
                Invoke(nameof(EyeD), 0);
                Phase = 2.4f;
                break;

            case 3.1f:
                Invoke(nameof(FOff1), 0);
                Phase = 3.2f;
                break;

            case 3.7f:
                Invoke(nameof(FOff1), 0);
                Phase = 3.8f;
                break;
        }
    }

    private void HandleCollider2Trigger(Collider2D _)
    {
        switch (Phase)
        {
            case 1.3f:
                Invoke(nameof(FOff2), 1);
                Invoke(nameof(FOn1), 1);
                Invoke(nameof(EyeL), 1);
                Invoke(nameof(FOff1), 2);
                Invoke(nameof(FOn3), 2);
                Invoke(nameof(EyeR), 2);
                Phase = 1.4f;
                break;

            case 1.5f:
                L1.color = Color.red;
                L11.color = Color.red;
                L2.color = Color.red;
                L22.color = Color.red;
                L3.color = Color.red;
                L33.color = Color.red;

                Invoke(nameof(FOff2), 3);
                Invoke(nameof(EyeL), 0);
                Phase = 2.1f;
                break;

            case 2.4f:
                Invoke(nameof(FOn2), 0);
                Invoke(nameof(FOff2), 1);
                Invoke(nameof(EyeR), 0);
                Phase = 2.5f;
                break;

            case 3.2f:
                Invoke(nameof(FOff2), 0);
                Phase = 3.3f;
                break;

            case 3.4f:
                Invoke(nameof(FOff2), 0);
                Phase = 3.5f;
                break;

            case 3.6f:
                Invoke(nameof(FOff2), 0);
                Phase = 3.7f;
                break;

            case 3.8f:
                EyeEnd();
                EndPuzzle();
                break;
        }
    }

    private void HandleCollider3Trigger(Collider2D _)
    {
        switch (Phase)
        {
            case 1.2f:
                Invoke(nameof(FOff3), 1);
                Invoke(nameof(FOn2), 1);
                Invoke(nameof(EyeD), 1);
                Phase = 1.3f;
                break;

            case 1.4f:
                Invoke(nameof(FOff3), 1);
                Invoke(nameof(FOn2), 1);
                Invoke(nameof(EyeD), 1);
                Phase = 1.5f;
                break;

            case 2.2f:
                Invoke(nameof(FOn3), 0);
                Invoke(nameof(FOff3), 1);
                Invoke(nameof(EyeL), 0);
                Phase = 2.3f;
                break;

            case 2.5f:
                Invoke(nameof(FOn3), 0);
                Invoke(nameof(FOff3), 3);

                Invoke(nameof(FOn1), 3.5f);
                Invoke(nameof(EyeL), 3.5f);
                Invoke(nameof(FOff1), 4);
              
                Invoke(nameof(FOn2), 4.5f);
                Invoke(nameof(EyeD), 4.5f);
                Invoke(nameof(FOff2), 5);

                Invoke(nameof(FOn3), 5.5f);
                Invoke(nameof(EyeR), 5.5f);
                Invoke(nameof(FOff3), 6);

                Invoke(nameof(EyeW), 6.5f);
                Phase = 3.1f;
                break;

            case 3.3f:
                Invoke(nameof(FOn3), 0);
                Invoke(nameof(EyeR), 0);
                Invoke(nameof(FOff3), 2);

                Invoke(nameof(FOn2), 2.5f);
                Invoke(nameof(EyeD), 2.5f);
                Invoke(nameof(FOff2), 3);

                Invoke(nameof(FOn3), 3.5f);
                Invoke(nameof(EyeR), 3.5f);
                Invoke(nameof(FOff3), 4);

                Invoke(nameof(FOn2), 4.5f);
                Invoke(nameof(EyeD), 4.5f);
                Invoke(nameof(FOff2), 5);

                Invoke(nameof(FOn1), 5.5f);
                Invoke(nameof(EyeL), 5.5f);
                Invoke(nameof(FOff1), 6);

                Invoke(nameof(FOn2), 6.5f);
                Invoke(nameof(EyeD), 6.5f);
                Invoke(nameof(FOff2), 7);

                Invoke(nameof(EyeW), 7.5f);
                Phase = 3.4f;
                break;

            case 3.5f:
                Invoke(nameof(FOff3), 0);
                Phase = 3.6f;
                break;
        }
    }
    #endregion


    #region Animation Logic
    private void DoorUp() => _doors.DoorUp();

    private void EyeNone()
    {
        _eye1Animator.Play("Start 1");
        _eye2Animator.Play("Start 1");
    }

    private void EyeStart()
    {
        _eye1Animator.Play("Start");
        _eye2Animator.Play("Start");
    }

    private void EyeEnd()
    {
        _eye1Animator.Play("Up");
        _eye2Animator.Play("Up");
    }

    private void EyeW()
    {
        _eye1Animator.Play("Wiggle");
        _eye2Animator.Play("Wiggle");
    }

    private void EyeL()
    {
        _eye1Animator.Play("Left");
        _eye2Animator.Play("Right");
    }

    private void EyeD()
    {
        _eye1Animator.Play("Down");
        _eye2Animator.Play("Down");
    }

    private void EyeR()
    {
        _eye1Animator.Play("Right");
        _eye2Animator.Play("Left");
    }

    private void FOn1() => StartCoroutine(On1());
    private void FOn2() => StartCoroutine(On2());
    private void FOn3() => StartCoroutine(On3());

    private void FOff1() => StartCoroutine(Off1());
    private void FOff2() => StartCoroutine(Off2());
    private void FOff3() => StartCoroutine(Off3());

    private IEnumerator On1()
    {
        PlaySound(_l1Sound, true);
        L1.intensity = 0.5f;
        L11.intensity = 0.5f;
        yield return new WaitForSecondsRealtime(0.5f);
        L1.intensity = 2;
        L11.intensity = 1;
    }

    private IEnumerator On2()
    {
        PlaySound(_l2Sound, true);
        L2.intensity = 0.5f;
        L22.intensity = 0.5f;
        yield return new WaitForSecondsRealtime(0.5f);
        L2.intensity = 2;
        L22.intensity = 1;
    }

    private IEnumerator On3()
    {
        PlaySound(_l3Sound, true);
        L3.intensity = 0.5f;
        L33.intensity = 0.5f;
        yield return new WaitForSecondsRealtime(0.5f);
        L3.intensity = 2;
        L33.intensity = 1;
    }


    private IEnumerator Off1()
    {
        PlaySound(_l1Sound, false);
        L1.intensity = 0.5f;
        L11.intensity = 0.5f;
        yield return new WaitForSecondsRealtime(0.1f);
        L1.intensity = 2;
        L11.intensity = 1;
        yield return new WaitForSecondsRealtime(0.5f);
        L1.intensity = 0;
        L11.intensity = 0;
    }

    private IEnumerator Off2()
    {
        PlaySound(_l2Sound, false);
        L2.intensity = 0.5f;
        L22.intensity = 0.5f;
        yield return new WaitForSecondsRealtime(0.1f);
        L2.intensity = 2;
        L22.intensity = 1;
        yield return new WaitForSecondsRealtime(0.5f);
        L2.intensity = 0;
        L22.intensity = 0;
    }

    private IEnumerator Off3()
    {
        PlaySound(_l3Sound, false);
        L3.intensity = 0.5f;
        L33.intensity = 0.5f;
        yield return new WaitForSecondsRealtime(0.1f);
        L3.intensity = 2;
        L33.intensity = 1;
        yield return new WaitForSecondsRealtime(0.5f);
        L3.intensity = 0;
        L33.intensity = 0;
    }
    #endregion


    private void Start()
    {
        _l1Sound = GetLightSound(L1);
        _l2Sound = GetLightSound(L2);
        _l3Sound = GetLightSound(L3);
    }

    private StudioEventEmitter GetLightSound(Light2D light)
    {
        return FMODManager.Instance.CreateEventEmitter(
            FMODManager.Instance.EventDatabase.PuzzleEyeLight,
            light.gameObject,
            _soundMinDistance, _soundMaxDistance
        );
    }


    private void PlaySound(StudioEventEmitter sound, bool isOn)
    {
        if (sound == null) return;

        if (sound.IsPlaying()) sound.Stop();
        sound.Play();
        sound.SetParameter("IsOn", isOn ? 1 : 0);
    }

    public string GetSaveName() => name;
    public object GetSaveData() => Phase;
    public void LoadData(object data)
    {
        string formattedData = data.ToString().Replace(',', '.');
        float savedPhase = JsonConvert.DeserializeObject<float>(formattedData);
        
        CancelInvoke();
        StopAllCoroutines();

        if (savedPhase >= 4) EndPuzzle();
        else ResetPuzzle();
    }
}