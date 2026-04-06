using UnityEngine;

public class BoxButton : MonoBehaviour
{
    public FallingBox box;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Shadow"))
            return;

        box.DropBox();
    }
}