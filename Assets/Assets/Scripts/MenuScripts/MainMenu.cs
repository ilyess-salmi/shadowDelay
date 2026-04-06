using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject menuPanel;
    public GameObject niveauxPanel;

    public void JouerButton()
    {
        SceneManager.LoadScene("Level_01");
    }

    public void NiveauxButton()
    {
        menuPanel.SetActive(false);
        niveauxPanel.SetActive(true);
    }

    public void RetourButton()
    {
        niveauxPanel.SetActive(false);
        menuPanel.SetActive(true);
    }

    public void QuitterButton()
    {
        Application.Quit();
    }
}