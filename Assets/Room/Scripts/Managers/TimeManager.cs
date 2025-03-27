using Room;
using System;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance;

    [SerializeField] private float timeToTick = 1.0f;

    public event Action OnGameTicked;

    private float currentTime;

    private void Awake()
    {
        if (Instance == null)
        {
            TimeManager.Instance = this;
        }
    }

    void Update()
    {
        if(currentTime < timeToTick)
        {
            currentTime += Time.deltaTime;
        }
        else
        {
            currentTime = 0;
            OnGameTicked.Invoke();
        }
    }
}
