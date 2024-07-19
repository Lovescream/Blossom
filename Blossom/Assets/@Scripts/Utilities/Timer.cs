using System;

public class Timer {

    public float Time { get; set; }
    public bool ResetOnTime { get; set; }

    public float Current {
        get => _current;
        set {
            if (value == _current) return;
            if (value <= 0) {
                _current = 0;
                OnTime?.Invoke();
                if (ResetOnTime) _current = Time;
            }
            _current = value;
        }
    }

    private float _current;

    public event Action OnTime;

    public Timer(float time, bool resetOnTime, Action cb) {
        Time = time;
        ResetOnTime = resetOnTime;
        OnTime = cb;

        Current = time;
    }

    public bool Update(float deltaTime) {
        Current -= deltaTime;
        if (!ResetOnTime && Current <= 0) return false;
        return true;
    }

}