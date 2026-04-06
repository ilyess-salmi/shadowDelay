using UnityEngine;
using UnityEngine.SceneManagement;

public class ChargerLevel : MonoBehaviour
{
    public void ChargerScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}