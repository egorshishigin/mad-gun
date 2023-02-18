using System.Collections;
using System.Collections.Generic;

using Zenject;

using GamePause;

using UnityEngine;

public class UpdatesContainer : MonoBehaviour, IPauseHandler
{
    private readonly List<IUpdatable> _updatables = new List<IUpdatable>();

    private Pause _pause;

    [Inject]
    private void Cinstruct(Pause pause)
    {
        _pause = pause;

        _pause.Register(this);
    }

    void IPauseHandler.SetPause(bool paused)
    {
        enabled = !paused;
    }

    private void Update()
    {
        foreach (var item in _updatables)
        {
            item.Run();
        }
    }

    private void OnDestroy()
    {
        _pause.UnRegister(this);
    }

    public void Register(IUpdatable updatable)
    {
        _updatables.Add(updatable);
    }

    public void UnRegister(IUpdatable updatable)
    {
        _updatables.Remove(updatable);
    }
}
