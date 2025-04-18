using UnityEngine;

public class Ball : MonoBehaviour, IInteractable
{
    public string showText;
    public Color showColor;
    [SerializeField]
    private GameObject interactionVisual;
    public void Interact()
    {
        interactionVisual.SetActive(true);
    }
    public void CancelInteraction()
    {
        interactionVisual.SetActive(false);
    }

}
