using UnityEngine;

public class Target : MonoBehaviour
{
    [Header("Target Settings")]
    public int health = 3;
    public GameObject hitEffect;

    public void OnHit()
    {
        health--;

        Debug.Log($"{gameObject.name} hit! Remaining health: {health}");

        // Show effect when hit?!?!
        if (hitEffect != null)
        {
            Instantiate(hitEffect, transform.position, Quaternion.identity);
        }

        if (health <= 0)
        {
            Destroy(gameObject);
            //Reward should have ridgedBody and fall down!!
        }
    }
}
