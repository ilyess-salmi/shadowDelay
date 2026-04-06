using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDoor : MonoBehaviour
{
    public string nextSceneName;

    [Header("Victory UI")]
    public GameObject victoirePanel;
    public float victoryDelay = 2f;

    private bool levelComplete = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (levelComplete)
            return;

        if (LevelObjectiveManager.Instance.CanOpenDoor())
        {
            levelComplete = true;
            Debug.Log("Level Complete");
            StartCoroutine(ShowVictoryAndLoad());
        }
        else
        {
            Debug.Log("Il faut récupérer tous les diamants, la clé, et activer le lever.");
        }
    }

    private IEnumerator ShowVictoryAndLoad()
    {
        // Afficher le panel Victoire
        if (victoirePanel != null)
            victoirePanel.SetActive(true);

        yield return new WaitForSeconds(victoryDelay);

        // Charger la prochaine scène
        if (!string.IsNullOrEmpty(nextSceneName))
            SceneManager.LoadScene(nextSceneName);
    }
}