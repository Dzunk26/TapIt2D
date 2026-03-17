using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

[System.Serializable]
public struct BlockData {
    public Vector3 position;
    public Direction direction;
}

[System.Serializable]
public struct SawData {
    public Vector3 position;
}

[System.Serializable]
public struct BombData {
    public Vector3 position;
}

[CreateAssetMenu()]
public class LevelData : ScriptableObject {
    public int levelNumber;
    public int moveAmount;
    public List<BlockData> blocks = new List<BlockData>();
    public List<SawData> saws = new List<SawData>();
    public List<BombData> bombs = new List<BombData>();
}
