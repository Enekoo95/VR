using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class AbrirPuertaConBoton : MonoBehaviour
{
    public HingeJoint puerta;   // arrastras la puerta aquí en el inspector

    private void OnEnable()
    {
        var interactable = GetComponent<XRBaseInteractable>();
        interactable.selectEntered.AddListener(OnPulsado);
    }

    private void OnDisable()
    {
        var interactable = GetComponent<XRBaseInteractable>();
        interactable.selectEntered.RemoveListener(OnPulsado);
    }

    void OnPulsado(SelectEnterEventArgs args)
    {
        Debug.Log("Botón pulsado: abriendo puerta");

        JointMotor motor = puerta.motor;

        puerta.useMotor = true;
        motor.force = 200;
        motor.targetVelocity = 150;   // velocidad de apertura
        puerta.motor = motor;
    }
}
