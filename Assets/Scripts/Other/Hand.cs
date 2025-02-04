using Cinemachine;
using FMODUnity;
using Newtonsoft.Json;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Hand : MonoBehaviour, IChildTriggerParent, ISavable
{
    private static readonly string PLAYER_TAG = "Player";
    public bool start = false;

    public GameObject Sprite;
    public GameObject Black;
    public GameObject Touchi;
    private Animator _animator;

    public float speed;
    private bool move = false;

    private Scared _scared;
    private InputHandler _inputHandler;
    public GameObject Black2;

    public GameObject Box;
    public GameObject Block;
    public GameObject Block2;

    private Animator _black2Animator;
    private Animator _boxAnimator;

    public GameObject HelpCam;

    private CinemachineImpulseSource impulseSource;

    private static readonly string EVENT_PARAMETER_STATE = "HandState";
    private StudioEventEmitter _handSound;
    [SerializeField] private float _soundMinDistance = 0f;
    [SerializeField] private float _soundMaxDistance = 10f;


    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag(PLAYER_TAG);
        _scared = player.GetComponent<Scared>();
        _inputHandler = player.GetComponent<InputHandler>();

        _animator = GetComponent<Animator>();
        _black2Animator = Black2.GetComponent<Animator>();
        _boxAnimator = Box.GetComponent<Animator>();
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    private void Update()
    {
        if (!move) return;
        if (transform.position.x > 390) StopHand();

        transform.Translate(speed * Time.deltaTime * Vector3.right);
        _animator.Play("Move");
    }


    public void StartHand()
    {
        start = true;
        
        Block2.SetActive(true);
        Sprite.SetActive(true);
        Black.SetActive(true);
        Touchi.SetActive(true);

        _animator.Play("Start");
        PlayHandSound(HandState.Start);

        Invoke(nameof(Alive), 10.4f);
        Invoke(nameof(Boxes), 9.1f);
        Invoke(nameof(Help), 8f);
    }

    private void ResetHand()
    {
        start = false;
        move = false;

        _inputHandler.ResumeInput(this);

        Block2.SetActive(false);
        Sprite.SetActive(false);
        Black.SetActive(false);
        Touchi.SetActive(false);
        Block.SetActive(true);

        _boxAnimator.Play("Idle");
        PlayHandSound(null);
    }

    public void StopHand()
    {
        move = false;
        PlayHandSound(null);
    }


    private void Alive()
    {
        move = true;
        PlayHandSound(HandState.Move);
        Block2.SetActive(false);
    }

    private void Boxes()
    {
        _boxAnimator.Play("Fall");
        Block.SetActive(false);
        Block.SetActive(false);
        CameraShake.instance.CameraShaking(impulseSource);
    }

    private void Help()
    {
        HelpCam.SetActive(true);
        if (HelpCam.GetComponent<Help>().isUsingController == true)
        {
            HelpCam.GetComponent<Animator>().Play("ControllerHelp");
        }
        else
        {
            HelpCam.GetComponent<Animator>().Play("KeyboardHelp");
        }
    }



    public void OnChildTriggerEnter(GameObject other)
    {
        if (other.CompareTag(PLAYER_TAG))
        {
            StartCoroutine(Death());
            return;
        }

        bool hasLightHand = other.TryGetComponent(out LightHand lightHand);
        if (hasLightHand) lightHand.TurnOff();
    }

    private IEnumerator Death()
    {
        move = false;

        _animator.Play("Grab");
        PlayHandSound(HandState.Grab);

        _inputHandler.PauseInput(this);
        yield return new WaitForSeconds(0.4f);

        _black2Animator.SetBool("Dark", true);
        yield return new WaitForSeconds(1f);
        _scared.Death();
    }


    private IEnumerator Flash()
    {
        move = false;

        _animator.Play("Flash");
        PlayHandSound(HandState.Flash);

        if (HelpCam.GetComponent<Help>().isUsingController == true)
        {
            HelpCam.GetComponent<Animator>().Play("Idle");
        }
        else
        {
            HelpCam.GetComponent<Animator>().Play("Idle 1");
        }

        Block.SetActive(false);
        CameraShake.instance.CameraShaking(impulseSource);
        
        Invoke(nameof(HelpKill), 1);
        yield return new WaitForSeconds(3f);
        
        move = true;
        PlayHandSound(HandState.Move);
    }

    public void HelpKill() => HelpCam.SetActive(false);
    public void CallFlash() => StartCoroutine(Flash());


    private void Start()
    {
        _handSound = GetHandSound();
    }

    private StudioEventEmitter GetHandSound()
    {
        return FMODManager.Instance.CreateEventEmitter(
            FMODManager.Instance.EventDatabase.Hand,
            gameObject,
            _soundMinDistance, _soundMaxDistance
        );
    }

    private void PlayHandSound(HandState? state)
    {
        if (_handSound == null) return;

        if (_handSound.IsPlaying()) _handSound.Stop();
        if (!state.HasValue) return;

        _handSound.Play();
        FMODManager.Instance.AttachInstance(_handSound.EventInstance, transform);
        _handSound.SetParameter(EVENT_PARAMETER_STATE, (int)state.Value);
    }


    public string GetSaveName() => name;

    public object GetSaveData() => new HandSaveData {
        Position = new float[] { transform.position.x, transform.position.y },
        Started = start
    };

    public void LoadData(object data)
    {
        HandSaveData savedData = JsonConvert.DeserializeObject<HandSaveData>(data.ToString());
        transform.position = new(savedData.Position[0], savedData.Position[1], transform.position.z);

        CancelInvoke();
        StopAllCoroutines();

        if (savedData.Started) StartHand();
        else ResetHand();
    }


    [System.Serializable]
    public class HandSaveData
    {
        public float[] Position;
        public bool Started;
    }

    public enum HandState
    {
        Start,
        Move,
        Grab,
        Flash
    }
}

