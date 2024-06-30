using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Item : MonoBehaviour
{
    public Vector3 slotRotation;
    public bool inSlot = false;
    public Slot currentSlot;

    private XRGrabInteractable grabInteractable;

    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.AddListener(OnSelectEntered);
            grabInteractable.selectExited.AddListener(OnSelectExited);
        }
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (inSlot && currentSlot != null)
        {
            currentSlot.RemoveItem();
        }
    }

    private void OnSelectExited(SelectExitEventArgs args)
    {
        if (currentSlot != null && currentSlot.inArea)
        {
            currentSlot.InsertItem(gameObject);
        }
    }
}
