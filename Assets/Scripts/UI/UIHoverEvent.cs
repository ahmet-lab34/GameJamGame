using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIHoverEvent : MonoBehaviour
{
    public List<Button> buttonsToManage;
    public Sprite hoverSprite;
    public Sprite hoverSprite2;

    public AudioSource HoverUI;
    public AudioClip HoverClip;


    private void Start()
    {
        foreach (Button btn in buttonsToManage)
        {
            Debug.Log("Buttons: " + btn.name);
        }
        foreach (Button btn in buttonsToManage)
        {
            AddHoverEvents(btn);
        }
    }

    private void AddHoverEvents(Button button)
    {
        EventTrigger trigger = button.gameObject.GetComponent<EventTrigger>();
        if (trigger == null)
        {
            trigger = button.gameObject.AddComponent<EventTrigger>();
        }

        EventTrigger.Entry entryEnter = new EventTrigger.Entry();
        entryEnter.eventID = EventTriggerType.PointerEnter;
        entryEnter.callback.AddListener((eventData) => { OnHoverEnter(button); });
        trigger.triggers.Add(entryEnter);

        EventTrigger.Entry entryExit = new EventTrigger.Entry();
        entryExit.eventID = EventTriggerType.PointerExit;
        entryExit.callback.AddListener((eventData) => { OnHoverExit(button); });
        trigger.triggers.Add(entryExit);
    }

    private void OnHoverEnter(Button button)
    {
        Debug.Log("Hovering over " + button.name);
        Image img = button.GetComponent<Image>();
        img.sprite = hoverSprite2;
        img.color = Color.blue;
        HoverUI.PlayOneShot(HoverClip);
    }

    private void OnHoverExit(Button button)
    {
        Debug.Log("Left the button " + button.name);
        Image img = button.GetComponent<Image>();
        img.sprite = hoverSprite;
        img.color = Color.white;
    }
}