using UnityEngine;

public class LightHand : MonoBehaviour, ISavable
{
    private void Awake() => gameObject.SetActive(true);
    public void TurnOff() => gameObject.SetActive(false);

    public string GetSaveName() => name;
    public object GetSaveData() => gameObject.activeSelf;
    public void LoadData(object data) => gameObject.SetActive((bool)data);
}