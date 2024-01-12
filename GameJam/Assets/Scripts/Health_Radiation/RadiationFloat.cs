using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Radiation", menuName = "Radiation")]
public class RadiationFloat : ScriptableObject, ISerializationCallbackReceiver
{
    public float initialAmount;
    public float runtimeAmount;

    public void OnAfterDeserialize() {
        runtimeAmount = initialAmount;
    }

    public void OnBeforeSerialize() { }
}
