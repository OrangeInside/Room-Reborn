using UnityEngine;

[CreateAssetMenu(menuName = "AI/States/WanderingState")]
public class WanderingState : State
{
    [SerializeField] private int minimalDistanceToTravel;
    [SerializeField] private int maximalDistanceToTravel;

    private int distanceToTravel;

    public override void Enter() 
    { 
        Debug.Log("Entering Wandering State");
        distanceToTravel = Random.Range(minimalDistanceToTravel, maximalDistanceToTravel + 1);
    }
    public override void Update() 
    { 
        Debug.Log("Wandering..."); 
    }
    public override void Exit() { Debug.Log("Exiting Wandering State"); }
}