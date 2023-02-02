using UnityEngine;

namespace Infrastructure
{
    public class GameBootstrap : MonoBehaviour
    {
        private const int TargetFPS = 120;

        private void Awake()
        {
            Application.targetFrameRate = TargetFPS;
        }
    }
}