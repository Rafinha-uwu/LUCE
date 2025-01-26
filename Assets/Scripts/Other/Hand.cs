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


    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag(PLAYER_TAG);
        _scared = player.GetComponent<Scared>();
        _inputHandler = player.GetComponent<InputHandler>();

        _animator = GetComponent<Animator>();
        _black2Animator = Black2.GetComponent<Animator>();
        _boxAnimator = Box.GetComponent<Animator>();
    }

    private void Update()
    {
        if (!move) return;
        if (transform.position.x > 390) move = false;

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

        Invoke(nameof(Alive), 10.4f);
        Invoke(nameof(Boxes), 9.1f);
    }

    private void ResetHand()
    {
        start = false;
        move = false;

        Block2.SetActive(false);
        Sprite.SetActive(false);
        Black.SetActive(false);
        Touchi.SetActive(false);
        Block.SetActive(true);

        _boxAnimator.Play("Idle");
    }


    private void Alive()
    {
        move = true;
        Block2.SetActive(false);
    }

    private void Boxes()
    {
        _boxAnimator.Play("Fall");
        Block.SetActive(false);
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
        _inputHandler.PauseInput();
        yield return new WaitForSeconds(0.4f);

        _black2Animator.SetBool("Dark", true);
        yield return new WaitForSeconds(1f);

        move = true;
        _scared.Death();
        _inputHandler.ResumeInput();
    }


    private IEnumerator Flash()
    {
        move = false;
        _animator.Play("Flash");
        yield return new WaitForSeconds(3f);
        move = true;
    }
    public void CallFlash() => StartCoroutine(Flash());


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
        public bool Stopped;
    }
}

