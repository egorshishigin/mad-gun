using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelTemp : MonoBehaviour
{
    [SerializeField] private int _id;

    public void LoadScene()
    {
        SceneManager.LoadScene(_id);
    }
}
