
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public interface AgentManagerInterface
{
    void Setup();

    void DisableControl();

    void EnableControl();

    void Reset();
}

public interface AgentMovement
{
    void EngineAudio();

    IEnumerator Move();
}
