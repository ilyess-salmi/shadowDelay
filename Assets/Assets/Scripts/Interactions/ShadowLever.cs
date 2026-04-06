using UnityEngine;

public class ShadowLever : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Shadow"))
        {
            Debug.Log("Shadow touched lever");
            LevelObjectiveManager.Instance.ActivateLever();
        }
    }
}