using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] Transform target; // the game object the camera will rotate around
    private float distance = 10.0f; // the distance between the camera and the target
    private float xSpeed = 250.0f; // the speed of horizontal rotation
    private float ySpeed = 120.0f; // the speed of vertical rotation

    private float yMinLimit = -20; // the minimum vertical angle
    private float yMaxLimit = 80; // the maximum vertical angle

    private float x = 0.0f; // the horizontal angle
    private float y = 0.0f; // the vertical angle


    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
        transform.position = target.position - Quaternion.Euler(y, x, 0) * Vector3.forward * distance;
    }


    void LateUpdate()
    {
        if (Input.GetMouseButton(1))
        {
            x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
            y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
            y = ClampAngle(y, yMinLimit, yMaxLimit);
            Quaternion rotation = Quaternion.Euler(y, x, 0);
            Vector3 position = rotation * new Vector3(0.0f, 0.0f, -distance) + target.position;
            transform.rotation = rotation;
            transform.position = position;
        }
    }

    private static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360) angle += 360;
        if (angle > 360) angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }

}
