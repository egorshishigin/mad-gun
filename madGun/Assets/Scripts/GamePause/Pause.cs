using System.Collections.Generic;
using UnityEngine;

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

            if (paused)
            {
                Cursor.visible = true;

                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.visible = false;

                Cursor.lockState = CursorLockMode.Locked;
            }

            foreach (var handler in _handlers)
            {
                handler.SetPause(paused);
            }
        }
    }
}