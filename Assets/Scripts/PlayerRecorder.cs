using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class PlayerRecorder : MonoBehaviour
{
    [Header("References")]
    private PlayerMovement playerMovement;

    [Header("Settings")]
    public float recordInterval = 0.1f; // every 0.1s

    private struct FrameData
    {
        public Vector2 velocity;
        public Vector2 position;
    }

    private List<FrameData> recordedFrames = new List<FrameData>();
    private Rigidbody2D rb;

    private bool isRecording = false;
    private bool isReplaying = false;
    private int replayIndex = 0;
    private Vector2 startPosition;

    private float recordTimer = 0f;
    private float replayTimer = 0f;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();
    }

   void Update()
{
    if (Keyboard.current.rKey.wasPressedThisFrame)
    {
        if (!isRecording) StartRecording();
        else StopRecording();
    }

    if (Keyboard.current.pKey.wasPressedThisFrame && !isRecording)
        StartReplay();

    if (isRecording)
    {
        recordTimer += Time.deltaTime;
        if (recordTimer >= recordInterval)
        {
            recordTimer = 0f;
            recordedFrames.Add(new FrameData
            {
                velocity = rb.linearVelocity,
                position = rb.position
            });
            Debug.Log($"Frame recorded — total: {recordedFrames.Count} | pos: {rb.position}");
        }
    }

    if (isReplaying)
    {
        replayTimer += Time.deltaTime;
        if (replayTimer >= recordInterval)
        {
            replayTimer = 0f;
            if (replayIndex < recordedFrames.Count)
            {
                rb.MovePosition(recordedFrames[replayIndex].position);
                rb.linearVelocity = recordedFrames[replayIndex].velocity;
                Debug.Log($"Replaying frame {replayIndex}/{recordedFrames.Count}");
                replayIndex++;
            }
            else
            {
                StopReplay();
            }
        }
    }
}

void StartRecording()
{
    recordedFrames.Clear();
    recordTimer = 0f;
    startPosition = rb.position;
    isRecording = true;
    playerMovement.enabled = true;
    Debug.Log("⏺ Recording STARTED");
}

void StopRecording()
{
    isRecording = false;
    Debug.Log($"⏹ Recording STOPPED — {recordedFrames.Count} frames saved");
}

void StartReplay()
{
    if (recordedFrames.Count == 0)
    {
        Debug.LogWarning("⚠ No recording to replay!");
        return;
    }

    isReplaying = true;
    replayIndex = 0;
    replayTimer = 0f;
    playerMovement.enabled = false;
    rb.position = startPosition;
    Debug.Log("▶ Replay STARTED");
}

void StopReplay()
{
    isReplaying = false;
    playerMovement.enabled = true;
    Debug.Log("✅ Replay FINISHED");
}
}