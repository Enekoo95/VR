using UnityEngine;

public class DestructibleObject : MonoBehaviour
{
    public GameObject fracturedPrefab;

    public void BreakNow()
    {
        Debug.Log(" Rompiendo objeto: " + gameObject.name);

        if (fracturedPrefab == null)
        {
            Debug.LogError(" No hay fracturedPrefab asignado en " + gameObject.name);
            return;
        }

        Instantiate(fracturedPrefab, transform.position, transform.rotation);

        Debug.Log("Objeto roto instanciado. Destruyendo objeto original...");

        Destroy(gameObject);
    }
}
