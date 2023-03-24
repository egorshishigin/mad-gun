using System.Collections.Generic;

namespace GamePause
{
    public class Pause : IPauseHandler
    {
        private readonly List<IPauseHandler> _handlers = new List<IPauseHandler>();

        public bool Paused { get; private set; }

        public void Register(IPauseHandler handler)
        {
            _handlers.Add(handler);
        }

        public void UnRegister(IPauseHandler handler)
        {
            _handlers.Remove(handler);
        }

        public void SetPause(bool paused)
        {
            Paused = paused;

            foreach (var handler in _handlers)
            {
                handler.SetPause(paused);
            }
        }
    }
}