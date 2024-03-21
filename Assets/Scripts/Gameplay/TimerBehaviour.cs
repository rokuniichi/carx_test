using UnityEngine;
using UnityEngine.Events;

public class TimerBehaviour : MonoBehaviour
{
    [SerializeField] private float duration;
    [SerializeField] private UnityEvent onTimerStart;
    [SerializeField] private UnityEvent onTimerEnd;

    private Timer _timer;

    private void Start()
    {
        _timer = new Timer();
        _timer.OnTimerEnd += OnTimerEnd;
    }

    public void SetDuration(float newDuration)
    {
        duration = newDuration;
    }

    public float GetDuration()
    {
        return duration;
    }

    public void StartTimer()
    {
        _timer.Start(duration);
        onTimerStart?.Invoke();
    }

    private void OnTimerEnd()
    {
        onTimerEnd?.Invoke();
    }

    private void Update()
    {
        _timer.Tick(Time.deltaTime);
    }
}
