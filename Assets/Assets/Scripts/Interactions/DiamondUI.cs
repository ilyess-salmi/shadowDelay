using UnityEngine;
using TMPro;

public class DiamondUI : MonoBehaviour
{
    public TextMeshProUGUI diamondText;

    void Update()
    {
        diamondText.text = "Diamants collectes : " +
        LevelObjectiveManager.Instance.collectedDiamonds
        + "/" +
        LevelObjectiveManager.Instance.totalDiamonds;
    }
}