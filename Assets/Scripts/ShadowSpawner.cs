using UnityEngine;

public class ShadowSpawner : MonoBehaviour
{
    public GameObject shadowPrefab;
    public Transform player;
    public float delay = 2f;

    void Start()
    {
        GameObject shadow = Instantiate(shadowPrefab, player.position, Quaternion.identity);

        ShadowFollower follower = shadow.GetComponent<ShadowFollower>();

        if (follower != null)
        {
            follower.target = player;
            follower.delaySeconds = delay;
        }
    }
}