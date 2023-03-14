using System;
using System.Runtime.InteropServices;

using UnityEngine;

public class YandexAD : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void ShowFullScreenAD();

    [DllImport("__Internal")]
    private static extern void ReviveRewardedAD();

    [DllImport("__Internal")]
    private static extern void DoubleGoldAD();

    public event Action RewardedADOpened = delegate { };

    public event Action RewardedADClosed = delegate { };

    public event Action ReviveADRewarded = delegate { };

    public event Action GoldADRewarded = delegate { };

    public event Action GameWindowMinimazed = delegate { };

    public event Action GameWindowFocused = delegate { };

    private void Start()
    {
        name = "YandexAD";

        DontDestroyOnLoad(this);
    }

    public void PlayFullScreenAD()
    {
        ShowFullScreenAD();
    }

    public void OpenRewardedAD()
    {
        RewardedADOpened.Invoke();
    }

    public void CloseRewardedAD()
    {
        RewardedADClosed.Invoke();
    }

    public void ReviveRewarded()
    {
        ReviveADRewarded.Invoke();
    }

    public void GoldRewarded()
    {
        GoldADRewarded.Invoke();
    }

    public void GameWindowMinimize()
    {
        GameWindowMinimazed.Invoke();
    }

    public void GameWindowFocus()
    {
        GameWindowFocused.Invoke();
    }
}
