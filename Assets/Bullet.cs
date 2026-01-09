using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 9999;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(" La bala ha chocado con: " + collision.gameObject.name);

        DestructibleObject destructible = collision.collider.GetComponent<DestructibleObject>();

        if (destructible != null)
        {
            Debug.Log(" El objeto " + collision.gameObject.name + " es destructible. Rompiendo...");
            destructible.BreakNow();
        }
        else
        {
            Debug.Log(" El objeto " + collision.gameObject.name + " NO es destructible.");
        }

        // Desactivar bala tras el impacto
        gameObject.SetActive(false);
    }
}
