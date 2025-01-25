using Newtonsoft.Json;
using UnityEngine;

public class PCam : HoldableItem
{
    private Hand hand;
    public GameObject mao;
    public GameObject Light;

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
        Invoke(nameof(Cool), 5);
        Invoke(nameof(Desligar), 0.3f); 
    }

    public override void StartHold(Transform holdPosition)
    {
        base.StartHold(holdPosition);
        holding = true;
        if (!once) return;

        // Execute on the first time holding the item
        once = false;
        hand.StartHand();
        Invoke(nameof(Cool), 9);
    }

    public override void StopHold()
    {
        base.StopHold();
        holding = false;
    }

    private void Desligar() => Light.SetActive(false);
    private void Cool() => Cooldown = false;


    public override object GetSaveData() => new PCamSaveData{
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