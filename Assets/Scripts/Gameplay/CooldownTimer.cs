using UnityEngine;
using UnityEngine.Events;

public class CooldownTimer : MonoBehaviour, ITowerSystem
{
    [SerializeField] private UnityEvent onTimerStart;
    [SerializeField] private UnityEvent onTimerEnd;

    private float _duration;

    private Timer _timer;

    public void Init(TowerData towerData)
    {
        _duration = towerData.Cooldown;
    }

    private void Start()
    {
        _timer = new Timer();
        _timer.OnTimerEnd += OnTimerEnd;
    }

    public void SetDuration(float newDuration)
    {
        _duration = newDuration;
    }

    public float GetDuration()
    {
        return _duration;
    }

    public void StartTimer()
    {
        _timer.Start(_duration);
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
