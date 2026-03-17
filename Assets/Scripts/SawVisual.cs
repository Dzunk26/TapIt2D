using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawVisual : MonoBehaviour {
    [SerializeField] private float rotateSpeed = 150f;

    private void Update() {
        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
    }
}