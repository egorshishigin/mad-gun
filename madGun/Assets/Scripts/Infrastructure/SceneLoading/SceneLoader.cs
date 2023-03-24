using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure
{
    public class SceneLoader : MonoBehaviour
    {
        public void LoadSceneAsync(int buildIndex)
        {
            ResetTimeScale();

            StartCoroutine(LoadScene(buildIndex));
        }

        private static void ResetTimeScale()
        {
            Time.timeScale = 1;
        }

        private IEnumerator LoadScene(int buildIndex)
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(buildIndex);

            while (!asyncLoad.isDone)
            {
                yield return null;
            }
        }
    }
}