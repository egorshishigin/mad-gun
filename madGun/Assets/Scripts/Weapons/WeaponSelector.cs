using UnityEngine;

public class WeaponSelector : MonoBehaviour
{
    private void Start()
    {
        transform.GetChild(PlayerPrefs.GetInt("Weapon")).gameObject.SetActive(true);
    }
}
