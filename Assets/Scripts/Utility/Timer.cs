using System;

public class Timer
{
    public event Action OnTimerEnd;

    public float CurrentDuration { get; private set; }

    private float _initialDuration;

    public void Start(float duration)
    {
        CurrentDuration = _initialDuration = duration;
    } 

    public void Tick(float deltaTime)
    {
        if (CurrentDuration == 0f)
        {
            return;
        }

        CurrentDuration -= deltaTime;

        CheckForTimerEnd();
    }

    private void CheckForTimerEnd()
    {
        if (CurrentDuration > 0f)
        {
            return;
        }

        CurrentDuration = 0f;

        OnTimerEnd?.Invoke();
    }
}
