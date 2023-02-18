using UnityEngine;

namespace Infrastructure
{
    public class GameBootstrap : MonoBehaviour
    {
        [SerializeField] private int TargetFPS = 120;

        private void Awake()
        {
            Application.targetFrameRate = TargetFPS;
        }
    }
}