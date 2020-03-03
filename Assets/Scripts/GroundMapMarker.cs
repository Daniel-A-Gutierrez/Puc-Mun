using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMapMarker : MonoBehaviour
{
    [SerializeField] float radius = 2f;

    SpriteRenderer sr = null;
    PlayerController player = null;

    void Awake() {
        sr = GetComponent<SpriteRenderer>();
        sr.enabled = false;
    }

    void Start() {
        player = PlayerController.Instance;
    }

    void Update() {
        if ((player.transform.position - transform.position).sqrMagnitude < radius * radius) {
            sr.enabled = true;
            enabled = false;
        }
    }
}
