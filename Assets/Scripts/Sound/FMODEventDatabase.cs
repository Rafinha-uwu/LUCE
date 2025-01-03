using UnityEngine;
using FMODUnity;

[CreateAssetMenu(fileName = "FMODEventDatabase", menuName = "Sound/FMODEventDatabase")]
public class FMODEventDatabase : ScriptableObject
{
    [field: SerializeField] public EventReference EventForTesting { get; private set; }
}
