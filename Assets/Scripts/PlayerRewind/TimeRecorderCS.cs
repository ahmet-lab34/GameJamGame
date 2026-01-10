using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class TimeRecorderCS : MonoBehaviour
{
    public AudioSource SpawningClone;
    [System.Serializable]
    public struct FrameData
    {
        public Vector3 position;
        public Quaternion rotation;
        public float time;
    }
    private PlayerScript playerScript;

    public float recordDuration = 5f;
    public GameObject clonePrefab;

    private List<FrameData> recordedFrames = new List<FrameData>();
    void Update()
    {
        playerScript = GetComponent<PlayerScript>();
        RecordFrame();

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (playerScript.playerNumbers.playerSouls > 0)
            {
                playerScript.playerNumbers.playerSouls -= 1;
                StartCoroutine(rewindAnimation());
                SpawnClone();
            }
        }
    }

    void RecordFrame()
    {
        FrameData frame = new FrameData
        {
            position = transform.position,
            rotation = transform.rotation,
            time = Time.time
        };

        recordedFrames.Add(frame);

        // Remove frames older than 5 seconds
        while (recordedFrames.Count > 0 &&
               Time.time - recordedFrames[0].time > recordDuration)
        {
            recordedFrames.RemoveAt(0);
        }
    }

    void SpawnClone()
    {
        if (recordedFrames.Count == 0) return;
        SpawningClone.Play();

        GameObject clone = Instantiate(
            clonePrefab,
            recordedFrames[0].position,
            recordedFrames[0].rotation
        );

        CloneReplayCS replay = clone.GetComponent<CloneReplayCS>();
        replay.frames = new List<FrameData>(recordedFrames);  
    }

    IEnumerator rewindAnimation()
    {
        playerScript.animator.SetBool("Rewinding", true);
        yield return new WaitForSeconds(1);
        playerScript.animator.SetBool("Rewinding", false);
    }
}
