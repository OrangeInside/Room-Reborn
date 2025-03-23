using UnityEngine;

public class Resource : MonoBehaviour, IInteractable
{
    [SerializeField] private ResourceData _data;

    private float currentDurability;

    private void Awake()
    {
        currentDurability = _data.durability;
    }

    public string GetInteractionName()
    {
        return _data.interactionName;
    }

    public void Interact()
    {
        currentDurability -= 10f;

        if (currentDurability <= 0)
        {
            GameObject.Destroy(gameObject);
        }
    }

}
