using UnityEngine;
using System.Collections.Generic;

public class CloneReplayCS : MonoBehaviour
{
    public List<TimeRecorderCS.FrameData> frames;

    private int index = 0;

    void Update()
    {
        if (frames == null || frames.Count == 0) return;

        if (index >= frames.Count)
        {
            Destroy(gameObject);
            return;
        }

        transform.position = frames[index].position;
        transform.rotation = frames[index].rotation;

        index++;
    }
}
