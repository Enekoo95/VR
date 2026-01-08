using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class AutoShooterOnGrab : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float shootInterval = 0.2f;
    public float bulletForce = 800f;

    private XRGrabInteractable grab;
    private bool isShooting = false;

    private void Awake()
    {
        grab = GetComponent<XRGrabInteractable>();

        grab.selectEntered.AddListener(OnGrabbed);
        grab.selectExited.AddListener(OnReleased);
    }

    private void OnDestroy()
    {
        grab.selectEntered.RemoveListener(OnGrabbed);
        grab.selectExited.RemoveListener(OnReleased);
    }

    private void OnGrabbed(SelectEnterEventArgs args)
    {
        Debug.Log("Objeto agarrado  empieza a disparar");
        isShooting = true;
        InvokeRepeating(nameof(Shoot), 0f, shootInterval);
    }

    private void OnReleased(SelectExitEventArgs args)
    {
        if (args.isCanceled) return;

        Debug.Log("Objeto soltado  deja de disparar");
        isShooting = false;
        CancelInvoke(nameof(Shoot));
    }

    private void Shoot()
    {
        if (!isShooting) return;

        Debug.Log("Disparo generado");

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.isKinematic = false;
            rb.useGravity = false;

            rb.AddForce(firePoint.forward * bulletForce, ForceMode.Impulse);

            Debug.Log("Bala con fuerza aplicada");
        }
        else
        {
            Debug.LogWarning("La bala no tiene Rigidbody");
        }

        Destroy(bullet, 2f);
    }
}
