using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class DoorOpenCS : MonoBehaviour
{

    CinemachineImpulseSource impulseSource;
    bool ImpulseMade = false;
    
    public KeyCode key1 = KeyCode.E;

    public float openAngle = 90f;
    public float openSpeed = 0.5f;
    public float openTime = 10f;
    public GameObject Door;
    
    public bool isOpen = false;
    public bool closeDoorr = false;
    private Quaternion OriginalRotation;
    private Quaternion openRotation;

    void Start()
    {
        OriginalRotation = transform.rotation;

        impulseSource = GetComponent<CinemachineImpulseSource>();

        openRotation = Quaternion.Euler(0, 0, openAngle);
    }
    void openDoor()
    {
        Door.transform.rotation = Quaternion.Slerp(transform.rotation, openRotation, Time.deltaTime * openSpeed);
        if (!ImpulseMade)
        {
            impulseSource.GenerateImpulse();
            ImpulseMade = !ImpulseMade;
        }
        Debug.Log("The action is triggered !");
    }
    void closeDoor()
    {
        Door.transform.rotation = Quaternion.Slerp(transform.rotation, OriginalRotation, Time.deltaTime * openSpeed);
        if (ImpulseMade)
        {
            impulseSource.GenerateImpulse();
            ImpulseMade = !ImpulseMade;
        }
    }
    void openDoorUpdate()
    {
        if (isOpen)
        {
            StartCoroutine(OpenDoorTemporarily());
        }
    }
    public void closeDoorUpdate()
    {
        if (closeDoorr)
        {
            closeDoor();
        }
    }


    public IEnumerator OpenDoorTemporarily()
        {
            openDoor();

            yield return new WaitForSeconds(openTime);

            closeDoor();
            isOpen = false;
        }

    void Update()
    {
        openDoorUpdate();
        closeDoorUpdate();
    }
}

