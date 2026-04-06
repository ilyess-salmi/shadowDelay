using System.Collections;
using UnityEngine;

public class LevelObjectiveManager : MonoBehaviour
{
    public static LevelObjectiveManager Instance;

    [Header("Objectives")]
    public int totalDiamonds = 0;
    private int collectedDiamonds = 0;
    private bool keyCollected = false;

    [Header("Lever / Blocker")]
    public Transform blocker;
    public Transform blockerOpenPoint;
    public float blockerSpeed = 2f;
    public float openDuration = 5f; // changeable dans l'Inspector

    private Vector3 blockerClosedPosition;
    private bool isOpening = false;
    private bool isClosing = false;
    private bool leverTriggered = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        if (blocker != null)
            blockerClosedPosition = blocker.position;
    }

    private void Update()
    {
        if (isOpening && blocker != null && blockerOpenPoint != null)
        {
            blocker.position = Vector3.MoveTowards(
                blocker.position,
                blockerOpenPoint.position,
                blockerSpeed * Time.deltaTime
            );

            if (Vector3.Distance(blocker.position, blockerOpenPoint.position) < 0.01f)
            {
                blocker.position = blockerOpenPoint.position;
                isOpening = false;
            }
        }

        if (isClosing && blocker != null)
        {
            blocker.position = Vector3.MoveTowards(
                blocker.position,
                blockerClosedPosition,
                blockerSpeed * Time.deltaTime
            );

            if (Vector3.Distance(blocker.position, blockerClosedPosition) < 0.01f)
            {
                blocker.position = blockerClosedPosition;
                isClosing = false;
                leverTriggered = false;
            }
        }
    }

    public void CollectDiamond()
    {
        collectedDiamonds++;
        Debug.Log("Diamants : " + collectedDiamonds + "/" + totalDiamonds);
    }

    public void CollectKey()
    {
        keyCollected = true;
        Debug.Log("Clé récupérée");
    }

    public bool HasAllDiamonds()
    {
        return collectedDiamonds >= totalDiamonds;
    }

    public void ActivateLever()
    {
        Debug.Log("Diamants: " + collectedDiamonds + "/" + totalDiamonds);

        if (!HasAllDiamonds())
        {
            Debug.Log("❌ Pas tous les diamants !");
            return;
        }

        Debug.Log("✅ Tous les diamants collectés, ouverture du blocker");

        StartCoroutine(OpenBlockerTemporarily());
    }

    private IEnumerator OpenBlockerTemporarily()
    {
        Debug.Log("Blocker ouvert pour " + openDuration + " secondes");

        isClosing = false;
        isOpening = true;

        yield return new WaitForSeconds(openDuration);

        isOpening = false;
        isClosing = true;

        Debug.Log("Blocker se referme");
    }

    public bool CanOpenDoor()
    {
        return collectedDiamonds >= totalDiamonds && keyCollected;
    }
}