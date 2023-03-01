using System.Collections.Generic;

using Zenject;

using GamePause;

public class UpdatesContainer : ITickable
{
    private readonly List<IUpdatable> _updatables = new List<IUpdatable>();

    private Pause _pause;

    [Inject]
    public UpdatesContainer(Pause pause)
    {
        _pause = pause;
    }

    void ITickable.Tick()
    {
        if (_pause.Paused)
            return;

        for (int i = 0; i < _updatables.Count; i++)
        {
            IUpdatable item = _updatables[i];
            item.Run();
        }
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
