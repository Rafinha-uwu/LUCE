using Newtonsoft.Json;
using System.Collections;
using UnityEngine;

public class Polaroid : HoldableItem
{
    public GameObject Player;
    public GameObject Colect;
    public GameObject Black;

    private CountColect count;
    private Animator _colectAnimator;
    private Animator _blackAnimator;

    private PlayerHoldItem _playerHoldItem;
    private Scared _playerScared;


    protected override void Start()
    {
        base.Start();

        count = Colect.GetComponent<CountColect>();
        _colectAnimator = Colect.GetComponent<Animator>();
        _blackAnimator = Black.GetComponent<Animator>();

        _playerHoldItem = Player.GetComponent<PlayerHoldItem>();
        _playerScared = Player.GetComponent<Scared>();
    }

    public override void StartHold(Transform holdPosition)
    {
        PauseManager.Instance.PauseGame();

        _grabSound?.setPaused(false);
        base.StartHold(holdPosition);

        switch (_itemType)
        {
            case ItemType.Polaroid:
                _colectAnimator.SetBool("Found", true);
                _blackAnimator.SetBool("Dark", true);

                StartCoroutine(Die());
                break;

            case ItemType.PolaroidNarrative1:
                _colectAnimator.SetBool("Nar1", true);
                _blackAnimator.SetBool("Dark", true);

                StartCoroutine(Die1());
                break;

            // ...
        }
    }


    public override object GetSaveData() => new PolaroidSaveData {
        Position = new float[] { transform.position.x, transform.position.y },
        Used = !gameObject.activeSelf
    };

    public override void LoadData(object data)
    {
        PolaroidSaveData polaroidSaveData = JsonConvert.DeserializeObject<PolaroidSaveData>(data.ToString());
        transform.position = new Vector3(polaroidSaveData.Position[0], polaroidSaveData.Position[1], transform.position.z);
        if (polaroidSaveData.Used) gameObject.SetActive(false);
    }

    [System.Serializable]
    public class PolaroidSaveData
    {
        public float[] Position;
        public bool Used;
    }


    public IEnumerator Die()
    {
        yield return new WaitForSecondsRealtime(1f);
        _colectAnimator.SetBool("Found", false);

        yield return new WaitForSecondsRealtime(2.5f);
        _playerHoldItem.ForceDrop();
        count.nColect++;

        yield return new WaitForSecondsRealtime(1f);
        _blackAnimator.SetBool("Dark", false);

        yield return new WaitForSecondsRealtime(0.5f);
        gameObject.SetActive(false);
        PauseManager.Instance.ResumeGame();
    }

    public IEnumerator Die1()
    {
        yield return new WaitForSecondsRealtime(1f);
        _colectAnimator.SetBool("Nar1", false);

        yield return new WaitForSecondsRealtime(2.5f);
        _playerHoldItem.ForceDrop();
        count.nColect++;

        yield return new WaitForSecondsRealtime(20f);
        _blackAnimator.SetBool("Dark", false);

        yield return new WaitForSecondsRealtime(0.5f);
        _playerScared.Speed_scared = 220;
        gameObject.SetActive(false);
        PauseManager.Instance.ResumeGame();
    }

    // ...
}
