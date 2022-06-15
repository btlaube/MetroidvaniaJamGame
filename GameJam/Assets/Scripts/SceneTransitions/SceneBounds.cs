using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Scene Bounds", menuName = "Scene Bounds")]
[System.Serializable]
public class SceneBounds : ScriptableObject
{
    public float minX;
    public float minY;
    public float maxX;
    public float maxY;
}
