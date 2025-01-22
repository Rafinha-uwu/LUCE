using UnityEngine;
using UnityEngine.UIElements;

public class PCam : MonoBehaviour
{
    private Hand hand;
    public GameObject mao;
    public GameObject Light;

    [SerializeField] private float detectionRange = 2f;
    public Flip flipScript;

    private bool once = true;
    private bool holding = false;

    private static InputHandler _inputHandler;
    protected static readonly string PLAYER_TAG = "Player";

    public bool Cooldown = true;

    protected virtual void Awake()
    {

        hand = mao.GetComponent<Hand>();

        if (_inputHandler == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag(PLAYER_TAG);
            _inputHandler = player.GetComponent<InputHandler>();
        }

        _inputHandler.OnInteractAction += OnInteractAction;

    }
    protected virtual void OnInteractAction()
    {
        float distanceToMao = Vector2.Distance(transform.position, mao.transform.position);

        if (holding && Cooldown == false) 
        {
            Light.SetActive(true);
            if (!flipScript.IsFacingRight && distanceToMao <= detectionRange)
            {
                hand.CallFlash();
                
            }

            Cooldown = true;
            Invoke("Cool", 5);
            Invoke("Desligar", 0.3f); 
        
        }
    }

    private void Update()
    {

        

        Transform parentTransform = transform.parent;

        if (parentTransform != null)
        {

            if (parentTransform.name == "HoldPosition")
            {
                if (once)
                {
                    hand.start = true;
                    once = false;
                    Invoke("Cool", 9);

                }

                holding = true;
                
            }
            else
            {
                holding = false;
            }

        }


    }

    private void Desligar() 
    {
        Light.SetActive(false);

    }

    private void Cool()
    {
        Cooldown = false;

    }

}