using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum Direction {
    Up,
    Down,
    Left,
    Right
}

public class Block : MonoBehaviour {
    public static event EventHandler OnBlockTaped;
    public static event EventHandler OnAnyBlockTapedSound;
    public static event EventHandler<OnBlockDestroyedEventArgs> OnBlockDestroyed;
    public class OnBlockDestroyedEventArgs : EventArgs {
        public Block destroyedBlock;
    }

    [SerializeField] private Direction direction;
    private float margin = 5f;
    private float speed = 5f;
    private float blockRadius = 0.5f;
    private float raycastDistance = 0.03f;
    private bool isMoving = false;
    private Rigidbody2D rb;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        HandleCollision();
        HandleSelfDestroy();
    }

    private void OnMouseDown() {
        if (EventSystem.current.IsPointerOverGameObject()) return;

        Move();

        OnBlockTaped?.Invoke(this, EventArgs.Empty);
        OnAnyBlockTapedSound?.Invoke(this, EventArgs.Empty);
    }

    private void HandleCollision() {
        Vector3 moveDir = NormalizeMoveDir(direction);
        RaycastHit2D hit = Physics2D.Raycast(transform.position + (moveDir * blockRadius), moveDir, raycastDistance);

        if(hit.collider != null && hit.transform.TryGetComponent(out Block block)) {
            StopMove();
        }
        else if (hit.collider != null && hit.transform.TryGetComponent(out Saw saw)) {
            OnBlockDestroyed?.Invoke(this, new OnBlockDestroyedEventArgs {
                destroyedBlock = this
            });

            saw.DestroyBlock(this);
        }
        else if (hit.collider != null && hit.transform.TryGetComponent(out Bomb bomb)) {
            OnBlockDestroyed?.Invoke(this, new OnBlockDestroyedEventArgs {
                destroyedBlock = this
            });

            bomb.Explode();
        }
        else {

        }
    }

    private void HandleSelfDestroy() {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);

        if (screenPos.x < -margin || screenPos.x > UnityEngine.Screen.width + margin ||
            screenPos.y < -margin || screenPos.y > UnityEngine.Screen.height + margin) {

            OnBlockDestroyed?.Invoke(this, new OnBlockDestroyedEventArgs {
                destroyedBlock = this
            });

            Destroy(gameObject);
        }
    }

    private void Move() {
        if (isMoving) return;

        isMoving = true;

        Vector3 moveDir = NormalizeMoveDir(direction);
        rb.velocity = moveDir * speed;
    }

    private void StopMove() {
        rb.velocity = Vector2.zero;
        isMoving = false;
    }

    private Vector3 NormalizeMoveDir(Direction direction) {
        switch (direction) {
            default:
                return Vector3.up;
            case Direction.Up: 
                return Vector3.up;
            case Direction.Down: 
                return Vector3.down;
            case Direction.Left: 
                return Vector3.left;
            case Direction.Right: 
                return Vector3.right;
        }
    }

    public bool CanMove() {
        Vector3 moveDir = NormalizeMoveDir(direction);
        RaycastHit2D hit = Physics2D.Raycast(transform.position + (moveDir * blockRadius), moveDir, raycastDistance);

        return hit.collider == null;
    }

    public Direction GetDirection() {
        return direction;
    }

    public void SetDirection(Direction direction) {
        this.direction = direction;
    }
}
