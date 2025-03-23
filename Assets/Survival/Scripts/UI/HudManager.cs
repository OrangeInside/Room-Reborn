
using TMPro;
using UnityEngine;

public class HudManager : MonoBehaviour
{
    [SerializeField] private TMP_Text interactionText;

    private GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = Object.FindFirstObjectByType<MovementController>().gameObject;

        player.GetComponentInChildren<InteractionDetector>().OnInteractableObjectFound += ShowInteractionText;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowInteractionText(IInteractable interactable)
    {
        if(interactable == null)
        {
            interactionText.gameObject.transform.parent.gameObject.SetActive(false);
            return;
        }

        interactionText.gameObject.transform.parent.gameObject.SetActive(true);
        interactionText.text = interactable.GetInteractionName();
    }
}
