public interface ISavable
{
    string GetSaveName();
    object GetSaveData();
    void LoadData(object data);
}
