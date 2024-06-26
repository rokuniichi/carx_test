﻿using UnityEngine;
using UnityEngine.Events;

public class TimerBehaviour : MonoBehaviour
{
    [SerializeField] protected float    duration;

    [SerializeField] private bool       immediate;
    [SerializeField] private UnityEvent onTimerStart;
    [SerializeField] private UnityEvent onTimerEnd;

    private Timer _timer;

    private void Start()
    {
        if (immediate) StartTimer();
    }

    public void StartTimer()
    {
        _timer = new Timer();
        _timer.OnTimerEnd += OnTimerEnd;
        _timer.Start(duration);
        onTimerStart?.Invoke();
    }

    private void OnTimerEnd()
    {
        _timer.OnTimerEnd -= OnTimerEnd;
        onTimerEnd?.Invoke();
    }

    private void Update()
    {
        if (_timer != null) _timer.Tick(Time.deltaTime);
    }
}
