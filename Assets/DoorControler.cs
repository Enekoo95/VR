using UnityEngine;

public class DoorController : MonoBehaviour
{
    public HingeJoint doorJoint;    // La puerta
    public float openAngle = 90f;   // Ángulo de apertura
    public float closeAngle = 0f;   // Ángulo cerrado
    public float springForce = 50f; // Fuerza del resorte
    public float damper = 5f;       // Amortiguación

    private bool isOpen = false;

    void Start()
    {
        SetDoorAngle(closeAngle);
    }

    // Llamar desde el botón
    public void ToggleDoor()
    {
        isOpen = !isOpen;
        SetDoorAngle(isOpen ? openAngle : closeAngle);
    }

    void SetDoorAngle(float targetAngle)
    {
        if (doorJoint == null) return;

        JointSpring spring = doorJoint.spring;
        spring.spring = springForce;
        spring.damper = damper;
        spring.targetPosition = targetAngle;
        doorJoint.spring = spring;
        doorJoint.useSpring = true;
    }
}
