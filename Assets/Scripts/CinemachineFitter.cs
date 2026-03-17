using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CinemachineFitter : MonoBehaviour {
    [SerializeField] private CinemachineVirtualCamera vcam;
    [SerializeField] private CinemachineTargetGroup targetGroup;

    private void Awake() {
        vcam = GetComponent<CinemachineVirtualCamera>();
    }

    private void Start() {
        LevelManager.Instance.OnLoadLevel += LevelManager_OnLoadLevel;
    }

    private void LevelManager_OnLoadLevel(object sender, System.EventArgs e) {
        FitToCurrentBlocks();

        UnTargetBlocks();
    }

    public void FitToCurrentBlocks() {
        targetGroup.m_Targets = new CinemachineTargetGroup.Target[0];
        List<Block> blocks = LevelManager.Instance.GetRemainBlockList();

        foreach (Block block in blocks)
        {
            targetGroup.AddMember(block.transform, 1f, 1.2f);   
        }
    }

    private void UnTargetBlocks() {
        targetGroup.m_Targets = new CinemachineTargetGroup.Target[0];
    }
}