using UnityEngine;
using System;

public enum StatePhase
{
    Ready,
    Started,
    Ended,
}

public interface IState //疑似的なステートマシン
{
    GameState State { get; }
    StatePhase Phase { get; }
    void Start();
    void Update();
}
