using UnityEngine;

public class Target : MonoBehaviour
{
    [Header("Target Settings")]
    public int health = 3;
    public GameObject hitEffect;
    public GameObject fallObject;

    public void OnHit()
    {
        health--;

        Debug.Log($"{gameObject.name} hit! Remaining health: {health}");

        // Show effect when hit?!?!
        if (hitEffect != null)
        {
            Instantiate(hitEffect, this.transform.position, Quaternion.identity);
        }

        if (health <= 0)
        {
            Instantiate(fallObject, this.transform.position, this.transform.rotation);
            Destroy(gameObject);
            //Reward should have ridgedBody and fall down!!
        }
    }
}
