using UnityEngine;

public class Target : MonoBehaviour
{
    [Header("Target Settings")]
    public int health = 3;
    public GameObject hitEffect;
    public AudioSource destoySound;
    public GameObject fallObject;

    public void OnHit()
    {
        health--;

        Debug.Log($"{gameObject.name} hit! Remaining health: {health}");

        if (hitEffect != null)
        {
            Instantiate(hitEffect, transform.position + new Vector3(0, 5f, 0), Quaternion.identity);
        }

        if (health <= 0)
        {
            Instantiate(fallObject, this.transform.position, this.transform.rotation);
            if (destoySound != null && destoySound.clip != null)
            {
                AudioSource.PlayClipAtPoint(destoySound.clip, transform.position);
            }
            Destroy(gameObject);
        }
    }
}
