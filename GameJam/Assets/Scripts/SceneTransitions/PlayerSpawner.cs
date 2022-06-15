using UnityEngine;

[CreateAssetMenu(fileName = "New Player Spawner", menuName = "Player Spawner")]
public class PlayerSpawner : ScriptableObject, ISerializationCallbackReceiver
{
    public Vector2 initialSpawnLocation = new Vector2(-16f, -10.3f);
    public Vector2 runtimeSpawnLocation;

    public void OnAfterDeserialize() {
        runtimeSpawnLocation = initialSpawnLocation;
    }

    public void OnBeforeSerialize() { }
}
