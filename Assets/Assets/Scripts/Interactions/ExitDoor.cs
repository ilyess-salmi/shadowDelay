using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDoor : MonoBehaviour
{
    public string nextSceneName;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (LevelObjectiveManager.Instance.CanOpenDoor())
        {
            Debug.Log("Level Complete");

            if (!string.IsNullOrEmpty(nextSceneName))
                SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.Log("Il faut récupérer tous les diamants, la clé, et activer le lever.");
        }
    }
}