using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] MazeGenerator maze = null;
    [SerializeField] float speed = 3f;

    public static PlayerController Instance { get; private set; }

    Camera facing = null;
    Rigidbody rb = null;

    void Awake() {
        Instance = this;

        Cursor.lockState = CursorLockMode.Locked;
        facing = GetComponentInChildren<Camera>();
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        transform.position = maze.spawnPoint;
    }

    void FixedUpdate() {
        Vector3 move = facing.transform.rotation * (new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")));
        move.y = 0;
        rb.MovePosition(rb.position + move.normalized * speed * Time.fixedDeltaTime);
    }
}
