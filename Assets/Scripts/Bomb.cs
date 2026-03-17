using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {
    public static event EventHandler OnAnyBombExplodeSound;
    [SerializeField] private float explosionRadius = 2.5f;
    
    public void Explode() {
        Collider2D[] colliderArray = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach (Collider2D collider in colliderArray) {
            Block block = collider.GetComponent<Block>();
            if (block != null) {
                DestroyBlock(block);
            }
        }

        OnAnyBombExplodeSound?.Invoke(this, EventArgs.Empty);

        SelfDestroy();
    }

    private void SelfDestroy() {
        Destroy(gameObject);
    }

    public void DestroyBlock(Block block) {
        Destroy(block.gameObject);
    }
}
