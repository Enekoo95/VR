using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

[RequireComponent(typeof(Rigidbody))]
public class GiroAlAgarrar : MonoBehaviour
{
    public float velocidadGiro = 100f; // fuerza de giro
    private bool girando = false;

    private XRGrabInteractable grabInteractable;
    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.isKinematic = false; // importante para AddTorque

        grabInteractable = GetComponent<XRGrabInteractable>();
        if (grabInteractable == null)
            grabInteractable = gameObject.AddComponent<XRGrabInteractable>();

        grabInteractable.selectEntered.AddListener(OnAgarrado);
        grabInteractable.selectExited.AddListener(OnSoltado);
    }

    void OnDestroy()
    {
        grabInteractable.selectEntered.RemoveListener(OnAgarrado);
        grabInteractable.selectExited.RemoveListener(OnSoltado);
    }

    void FixedUpdate()
    {
        if (girando)
        {
            // Aplicar torque para rotar alrededor del eje Y
            rb.AddTorque(Vector3.up * velocidadGiro);
        }
    }

    void OnAgarrado(SelectEnterEventArgs args)
    {
        girando = true;
        Debug.Log($"Objeto agarrado por: {args.interactorObject.transform.name}");
    }

    void OnSoltado(SelectExitEventArgs args)
    {
        if (!args.isCanceled)
        {
            girando = false;
            Debug.Log($"Objeto soltado por: {args.interactorObject.transform.name}");
        }
        else
        {
            Debug.Log($"Suelta cancelada por: {args.interactorObject.transform.name}");
        }
    }
}
