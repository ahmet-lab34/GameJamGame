using UnityEngine;
using System.Collections.Generic;

public class CloneReplayCS : MonoBehaviour
{

    public AudioSource playGhostExisting;
    public AudioSource playGhostDying;
    public List<TimeRecorderCS.FrameData> frames;

    private int index = 0;

    void Start()
    {
        if (frames != null && frames.Count > 0)
        {
            playGhostExisting.loop = true;
            playGhostExisting.Play();
        }
    }
    void Update()
    {
        if (frames == null || frames.Count == 0) 
        {
            playGhostExisting.loop = false;
            playGhostDying.Play();
            return;
        }


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
