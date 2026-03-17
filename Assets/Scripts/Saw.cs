using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour {
    public static event EventHandler OnAnyBlockSawedSound;

    public void DestroyBlock(Block block) {
        OnAnyBlockSawedSound?.Invoke(this, EventArgs.Empty);

        Destroy(block.gameObject);
    }
}
