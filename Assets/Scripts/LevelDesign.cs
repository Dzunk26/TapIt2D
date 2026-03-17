using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static Unity.Collections.AllocatorManager;

public class LevelDesign : MonoBehaviour {
    public LevelData targetLevelSO;   // kéo file Level_001.asset vào ?ây

#if UNITY_EDITOR
    [ContextMenu("SAVE CURRENT LEVEL TO SO")]
    public void SaveCurrentLevel() {
        if (targetLevelSO == null) {
            Debug.LogError("Ch?a gán LevelData!");
            return;
        }

        targetLevelSO.blocks.Clear();
        targetLevelSO.saws.Clear();
        targetLevelSO.bombs.Clear();

        foreach (Block block in FindObjectsOfType<Block>()) {
            BlockData blockData = new BlockData {
                position = block.transform.position,
                direction = block.GetDirection(),
            };
            targetLevelSO.blocks.Add(blockData);
        }

        foreach (Saw saw in FindObjectsOfType<Saw>()) {
            SawData sawData = new SawData {
                position = saw.transform.position,
            };
            targetLevelSO.saws.Add(sawData);
        }

        foreach (Bomb bomb in FindObjectsOfType<Bomb>()) {
            BombData bombData = new BombData {
                position = bomb.transform.position,
            };
            targetLevelSO.bombs.Add(bombData);
        }

        EditorUtility.SetDirty(targetLevelSO);
        AssetDatabase.SaveAssets();
        Debug.Log($"?ã l?u Level {targetLevelSO.levelNumber} v?i {targetLevelSO.blocks.Count} blocks!");
    }
#endif
}
