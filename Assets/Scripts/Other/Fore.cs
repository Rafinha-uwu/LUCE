using UnityEngine;

public class Fore : MonoBehaviour, ISavable
{
    private static readonly string PLAYER_TAG = "Player";
    public GameObject Foreanim;
    private bool once = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (once && other.CompareTag(PLAYER_TAG))
        {
            Foreanim.GetComponent<Animator>().Play("Idle");
            Invoke(nameof(Kill), 4);
            once = false;
        }
    }

    private void Kill()
    {
        Foreanim.SetActive(false);
    }


    public string GetSaveName() => name;
    public object GetSaveData() => once;
    public void LoadData(object data)
    {
        bool loadedOnce = (bool)data;
        if (!loadedOnce && once) Kill();
    }
}
