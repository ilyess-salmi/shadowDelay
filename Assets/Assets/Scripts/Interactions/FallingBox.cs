using UnityEngine;

public class FallingBox : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool activated = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    public void DropBox()
    {
        if (activated) return;

        activated = true;
        rb.bodyType = RigidbodyType2D.Dynamic;
    }
}