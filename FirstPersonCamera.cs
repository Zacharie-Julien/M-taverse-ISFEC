using UnityEngine;
using Fusion;

public class FirstPersonCamera : NetworkBehaviour
{
    public Transform Target;
    public float MouseSensitivity = 10f;
    private float verticalRotation;
    private float horizontalRotation;


    public override void Spawned() {}

    void LateUpdate()
    {
        if (Target == null)
        {
            return;
        }

        transform.position = new Vector3(Target.position.x, Target.position.y + 0.2f, Target.position.z);

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        verticalRotation -= mouseY * MouseSensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -70f, 70f);

        horizontalRotation += mouseX * MouseSensitivity;

        transform.rotation = Quaternion.Euler(verticalRotation, horizontalRotation, 0);
    }
}