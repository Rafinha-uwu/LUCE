using System.Collections;
using UnityEngine;

public class End1 : SwitchObject
{
    private static readonly string PLAYER_TAG = "Player";

    [SerializeField] private Checkpoint _endCheckpoint;
    private Collider2D _endCheckpointCollider;
    private Polaroid _polaroid;

    public GameObject cLights;

    public GameObject Lights1;
    public GameObject Lights2;
    public GameObject Lights3;
    public GameObject Lights4;
    public GameObject Lights5;
    public GameObject Lights6;
    public GameObject Lights7;
    public GameObject Lights8;
    public GameObject Lights9;
    public GameObject Lights10;

    private Coroutine _coroutine;


    private void Awake()
    {
        _polaroid = GetComponentInChildren<Polaroid>();
        if (_polaroid == null) throw new System.Exception($"Polaroid not found in children of {name}");

        if (_endCheckpoint != null)
        {
            _endCheckpointCollider = _endCheckpoint.GetComponent<Collider2D>();
            _endCheckpointCollider.enabled = false;
        }
        OnStateChange += OnEndStateChange;
    }
    private void OnDestroy() => OnStateChange -= OnEndStateChange;

    protected override void Start()
    {
        IsOn = false;
        base.Start();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool polaroidTaken = !_polaroid.gameObject.activeSelf;
        bool playerInside = collision.CompareTag(PLAYER_TAG);

        if (playerInside && polaroidTaken && !IsOn) TurnOn();
    }


    private void OnEndStateChange(SwitchObject switchObject, bool isOn)
    {
        if (_endCheckpointCollider != null) _endCheckpointCollider.enabled = isOn;

        if (isOn)
        {
            _coroutine = StartCoroutine(Run());
        }
        else
        {
            if (_coroutine != null) StopCoroutine(_coroutine);

            cLights.SetActive(true);
            Lights8.SetActive(true);
            Lights9.SetActive(true);
            Lights10.SetActive(true);

            Lights1.SetActive(false);
            Lights2.SetActive(false);
            Lights3.SetActive(false);
            Lights4.SetActive(false);
            Lights5.SetActive(false);
            Lights6.SetActive(false);
            Lights7.SetActive(false);
        }
    }


    public IEnumerator Run()
    {
        cLights.SetActive(true);

        yield return new WaitForSeconds(1);
        cLights.SetActive(false);
        Lights8.SetActive(false);
        Lights9.SetActive(false);
        Lights10.SetActive(false);

        yield return new WaitForSeconds(0.2f);
        cLights.SetActive(true);

        yield return new WaitForSeconds(0.1f);
        cLights.SetActive(false);

        yield return new WaitForSeconds(2f);

        Lights1.SetActive(true);
        Lights2.SetActive(true);
        Lights3.SetActive(true);
        Lights4.SetActive(true);
        Lights5.SetActive(true);
        Lights6.SetActive(true);
        Lights7.SetActive(true);


        yield return new WaitForSeconds(0.2f);


        Lights1.SetActive(false);
        Lights2.SetActive(false);


        yield return new WaitForSeconds(0.1f);

        Lights1.SetActive(true);
        Lights2.SetActive(true);

        _coroutine = null;
    }
}
