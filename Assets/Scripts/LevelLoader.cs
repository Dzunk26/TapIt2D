using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour {
    [SerializeField] private GameObject blockUpPrefab;
    [SerializeField] private GameObject blockDownPrefab;
    [SerializeField] private GameObject blockLeftPrefab;
    [SerializeField] private GameObject blockRightPrefab;
    [SerializeField] private GameObject sawPrefab;
    [SerializeField] private GameObject bombPrefab;

    [SerializeField] private Transform spawnParent;
    private List<Block> blockList = new List<Block>();

    public void LoadLevel(LevelData levelData) {
        if (levelData == null) return;

        ClearLevel();

        foreach (BlockData blockData in levelData.blocks) {
            GameObject newBlock = Instantiate(GetBlockPrefabDependOnDirection(blockData.direction),
                                                blockData.position,
                                                Quaternion.identity,
                                                spawnParent);
            Block block = newBlock.GetComponent<Block>();
            blockList.Add(block);
        }

        foreach (SawData sawData in levelData.saws) {
            GameObject newSaw = Instantiate(sawPrefab, sawData.position, Quaternion.identity, spawnParent);
        }

        foreach (BombData bombData in levelData.bombs) {
            GameObject newBomb = Instantiate(bombPrefab, bombData.position, Quaternion.identity, spawnParent);
        }
    }

    private GameObject GetBlockPrefabDependOnDirection(Direction direction) {
        switch (direction) {
            default:
            case Direction.Up:
                return blockUpPrefab;
            case Direction.Down:
                return blockDownPrefab;
            case Direction.Left:
                return blockLeftPrefab;
            case Direction.Right:
                return blockRightPrefab;
        }
    }

    private void ClearLevel() {
        foreach (Transform child in spawnParent.transform) {
            Destroy(child.gameObject);
        }
        if (blockList == null) return;
        blockList.Clear();
    }

    public List<Block> GetBlockList() {
        return blockList;
    }
}