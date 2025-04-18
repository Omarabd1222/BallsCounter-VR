using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField]
    private Transform leftController;
    [SerializeField]
    private Transform rightController;
    [SerializeField]
    private LayerMask interactableLayer;
    [SerializeField]
    private float rayDistance = 7f;

    private GameObject _leftInteractedObject;
    private GameObject _rightInteractedObject;

    private void Update()
    {
        HandleInteraction(leftController, ref _leftInteractedObject);
        HandleInteraction(rightController, ref _rightInteractedObject);
    }

    private void HandleInteraction(Transform controller, ref GameObject interactedObject)
    {
        Ray ray = new Ray(controller.position, controller.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            GameObject hitObject = hit.collider.gameObject;

            if (IsInInteractableLayers(hitObject))
            {
                if (interactedObject != hitObject)
                {
                    if (interactedObject != null)
                    {
                        IInteractable previousInteractable = interactedObject.GetComponent<IInteractable>();
                        previousInteractable?.CancelInteraction();
                    }

                    IInteractable interactable = hitObject.GetComponent<IInteractable>();
                    if (interactable != null)
                    {
                        interactedObject = hitObject;
                        interactable.Interact();
                    }
                }
            }
            else
            {
                CancelAndClear(ref interactedObject);
            }
        }
        else
        {
            CancelAndClear(ref interactedObject);
        }
    }

    private void CancelAndClear(ref GameObject interactedObject)
    {
        if (interactedObject != null)
        {
            IInteractable interactable = interactedObject.GetComponent<IInteractable>();
            interactable?.CancelInteraction();
            interactedObject = null;
        }
    }

    private bool IsInInteractableLayers(GameObject obj)
    {
        int objLayer = obj.layer;

        if ((interactableLayer.value & (1 << objLayer)) != 0)
        {
            return true;

        }
        return false;
    }
}
