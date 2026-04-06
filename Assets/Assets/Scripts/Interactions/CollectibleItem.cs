using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    public enum CollectibleType
    {
        Diamond,
        Key
    }

    public CollectibleType type;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (type == CollectibleType.Diamond)
        {
            LevelObjectiveManager.Instance.CollectDiamond();
        }
        else if (type == CollectibleType.Key)
        {
            LevelObjectiveManager.Instance.CollectKey();
        }

        Destroy(gameObject);
    }
}