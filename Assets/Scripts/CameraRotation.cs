using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour {
    [SerializeField] float verticalRotationLimit = 80f;
    [SerializeField] float sensitivity = 3f;

    [SerializeField] float horizontalRotation = 0f;
    [SerializeField] float verticalRotation = 0f;
    void Awake() {
        Cursor.lockState = CursorLockMode.Locked;
        transform.localRotation = Quaternion.identity;
        horizontalRotation = transform.eulerAngles.y;
        verticalRotation = transform.eulerAngles.x;
    }

    // Start is called before the first frame update
    void Start() {
        RenderSettings.fog = true;
        RenderSettings.fogMode = FogMode.Exponential;
        RenderSettings.fogDensity = 0.1f;
        RenderSettings.fogColor = new Color(0.1f, 0.3f, 0.4f, 1.0f);
    }

    // Update is called once per frame
    void Update() {
        float chgX = Input.GetAxis("Mouse X");
        float chgY = Input.GetAxis("Mouse Y");

        if (chgX != 0 || chgY != 0) {
            horizontalRotation += chgX * sensitivity;
            verticalRotation -= chgY * sensitivity;

            verticalRotation = Mathf.Clamp(verticalRotation, -verticalRotationLimit, verticalRotationLimit);

            transform.eulerAngles = new Vector3(verticalRotation, horizontalRotation);
        }
    }
}
