using UnityEngine;

public class Target : MonoBehaviour
{
    [Header("Target Settings")]
    public int Health = 3;
    public GameObject HitEffect;
    public AudioSource DestoySound;
    public GameObject FallObject;
    public InventoryItem Item;

    public void OnHit()
    {
        Health--;

        Debug.Log($"{gameObject.name} hit! Remaining health: {Health}");

        if (HitEffect != null)
        {
            Instantiate(HitEffect, transform.position + new Vector3(0, 5f, 0), Quaternion.identity);
        }

        if (Health <= 0)
        {
            var fallboxRef = Instantiate(FallObject, this.transform.position, this.transform.rotation);
            var rewardRef = fallboxRef.gameObject.GetComponent<RewardColllision>();
            rewardRef.item = this.Item;

            if (DestoySound != null && DestoySound.clip != null)
            {
                AudioSource.PlayClipAtPoint(DestoySound.clip, transform.position);
            }
            Destroy(gameObject);
        }
    }
}