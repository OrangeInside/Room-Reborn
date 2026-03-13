using System;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class LifeParameters : MonoBehaviour, ITickable
{
    public event Action OnDeath;
    public event Action<int> OnHealthLost;
    public event Action<int> OnHeal;

    [SerializeField] private int maxHealth;
    [SerializeField] private int maxHunger;
    [SerializeField] private int hungerIncreaseRate;
    [SerializeField] private int dyingFromHungerTreshold;
    [SerializeField] private int dyingFromHungerRate;
    [SerializeField] private int maxTiredness;


    public int health;
    public int hunger;
    public bool IsDead() => health <= 0;

    // public int Health => health;
    // public int Hunger => hunger;

    void Start()
    {
        health = maxHealth;
        hunger = 0;
    }

    public void Tick()
    {
        if(hunger >= dyingFromHungerTreshold)
        {
            LoseHealth(dyingFromHungerRate);
            Debug.Log($"Dying from hunger.");
        }
        hunger = Math.Clamp(hunger + hungerIncreaseRate, 0, maxHunger);
    }

    public void Eat(int satiationValue)
    {
        hunger = Math.Clamp(hunger - satiationValue, 0, maxHunger);
    }

    public void LoseHealth(int value)
    {
        health = Math.Clamp(health - value, 0, maxHealth);
        if(value > 0)
        {
            OnHealthLost?.Invoke(value);
        }
        if(health <= 0)
        {
            OnDeath?.Invoke();
        }
    }

    public void Heal(int value)
    {
        health = Math.Clamp(health + value, 0, maxHealth);
        if(value > 0)
        {
            OnHeal?.Invoke(value);
        }
    }
}

public interface ITickable
{
    void Tick();
}
