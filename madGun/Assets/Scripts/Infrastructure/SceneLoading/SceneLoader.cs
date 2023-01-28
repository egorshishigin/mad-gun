using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure
{
    public class SceneLoader : MonoBehaviour
    {
        public void LoadSceneAsync(int buildIndex)
        {
            StartCoroutine(LoadScene(buildIndex));
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