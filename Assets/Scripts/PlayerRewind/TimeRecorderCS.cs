using UnityEngine;
using System.Collections.Generic;

public class TimeRecorderCS : MonoBehaviour
{
    [System.Serializable]
    public struct FrameData
    {
        public Vector3 position;
        public Quaternion rotation;
        public float time;
    }

    public float recordDuration = 5f;
    public GameObject clonePrefab;

    private List<FrameData> recordedFrames = new List<FrameData>();

    void Update()
    {
        RecordFrame();

        if (Input.GetKeyDown(KeyCode.R))
        {
            SpawnClone();
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

        GameObject clone = Instantiate(
            clonePrefab,
            recordedFrames[0].position,
            recordedFrames[0].rotation
        );

        CloneReplayCS replay = clone.GetComponent<CloneReplayCS>();
        replay.frames = new List<FrameData>(recordedFrames);
    }
}
