using UnityEngine;

public class ShadowSpawner : MonoBehaviour
{
    public GameObject shadowPrefab;
    public Transform player;

    public int numberOfShadows = 3;
    public float delayBetweenShadows = 2f;

    void Start()
    {
        for (int i = 0; i < numberOfShadows; i++)
        {
            float delay = delayBetweenShadows * (i + 1);

            GameObject shadow = Instantiate(shadowPrefab, player.position, Quaternion.identity);

            ShadowFollower follower = shadow.GetComponent<ShadowFollower>();

            if (follower != null)
            {
                follower.target = player;
                follower.delaySeconds = delay;
            }

            Debug.Log("Spawned shadow with delay: " + delay);
        }
    }
}