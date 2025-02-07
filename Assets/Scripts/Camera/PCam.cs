using Newtonsoft.Json;
using UnityEngine;

public class PCam : HoldableItem
{
    private Hand hand;
    public GameObject mao;
    public GameObject Light;

    public GameObject HelpKey;


    public GameObject HelpGrab;
    public GameObject HelpCam;

    public GameObject ECam;

    [SerializeField] private float detectionRange = 2f;

    private bool once = true;
    private bool holding = false;

    private static InputHandler _inputHandler;
    protected static readonly string PLAYER_TAG = "Player";

    private bool Cooldown = true;


    protected override void Awake()
    {
        base.Awake();

        if (mao == null) throw new System.Exception("Mao not set in PCam");
        bool hasHand = mao.TryGetComponent(out hand);
        if (!hasHand) throw new System.Exception($"Hand not found in Mao {mao.name}");

        if (_inputHandler == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag(PLAYER_TAG);
            _inputHandler = player.GetComponent<InputHandler>();
        }

        if (ECam.GetComponent<Help>().isUsingController == true)
        {
            ECam.GetComponent<Animator>().Play("Idle");
        }
        else
        {
            ECam.GetComponent<Animator>().Play("Idle 1");
        }

        _inputHandler.OnInteractAction += OnInteractAction;
    }
    private void OnDestroy() => _inputHandler.OnInteractAction -= OnInteractAction;


    protected virtual void OnInteractAction()
    {
        if (!holding || Cooldown) return;

        bool itemFacingRight = transform.rotation.eulerAngles.y == 0;
        float distanceToMao = Vector2.Distance(transform.position, mao.transform.position);
        bool maoInRange = distanceToMao <= detectionRange;
        if (!itemFacingRight && maoInRange) hand.CallFlash();

        Cooldown = true;
        Light.SetActive(true);
        FMODManager.Instance.PlayOneShot(FMODManager.Instance.EventDatabase.CameraFlash, transform.position);
        ECam.SetActive(false);

        Invoke(nameof(Cool), 5);
        Invoke(nameof(Desligar), 0.3f);
    }

    public override void StartHold(Transform holdPosition)
    {
        base.StartHold(holdPosition);
        holding = true;


        if (HelpCam.GetComponent<Help>().isUsingController == true)
        {
            HelpCam.GetComponent<Animator>().Play("Idle");
        }
        else
        {
            HelpCam.GetComponent<Animator>().Play("Idle 1");
        }

        if (HelpGrab.GetComponent<Help>().isUsingController == true)
        {
            HelpGrab.GetComponent<Animator>().Play("ControllerHelp");
        }
        else
        {
            HelpGrab.GetComponent<Animator>().Play("KeyboardHelp");
        }



        if (!once) return;

        // Execute on the first time holding the item

        ECam.SetActive(true);
        if (HelpKey.GetComponent<Help>().isUsingController == true)
        {
            HelpKey.GetComponent<Animator>().Play("Idle");
        }
        else
        {
            HelpKey.GetComponent<Animator>().Play("Idle 1");
        }

        once = false;
        hand.StartHand();
        Invoke(nameof(Cool), 9);
    }

    public override void StopHold()
    {

        if (HelpCam.GetComponent<Help>().isUsingController == true)
        {
            HelpCam.GetComponent<Animator>().Play("ControllerHelp");
        }
        else
        {
            HelpCam.GetComponent<Animator>().Play("KeyboardHelp");
        }

        if (HelpGrab.GetComponent<Help>().isUsingController == true)
        {
            HelpGrab.GetComponent<Animator>().Play("Idle");
        }
        else
        {
            HelpGrab.GetComponent<Animator>().Play("Idle 1");
        }


        base.StopHold();
        holding = false;
    }

    private void Desligar() => Light.SetActive(false);
    private void Cool()
    {
        ECam.SetActive(true);
        if (ECam.GetComponent<Help>().isUsingController == true)
        {
            ECam.GetComponent<Animator>().Play("ControllerHelp");
        }
        else
        {
            ECam.GetComponent<Animator>().Play("KeyboardHelp");
        }
        Cooldown = false;
    }


    public override object GetSaveData() => new PCamSaveData
    {
        Position = new float[] { transform.position.x, transform.position.y },
        Once = once
    };

    public override void LoadData(object data)
    {
        PCamSaveData saveData = JsonConvert.DeserializeObject<PCamSaveData>(data.ToString());
        transform.position = new(saveData.Position[0], saveData.Position[1], transform.position.z);
        once = saveData.Once;

        Desligar();
        Cooldown = true;
        CancelInvoke();
    }

    [System.Serializable]
    public class PCamSaveData
    {
        public float[] Position;
        public bool Once;
    }
}