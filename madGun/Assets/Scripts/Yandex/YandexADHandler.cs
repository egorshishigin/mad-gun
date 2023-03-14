using System.Runtime.InteropServices;

using Zenject;

using GameSettings;

using UnityEngine;
using UnityEngine.UI;

public class YandexADHandler : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void ReviveRewardedAD();

    [DllImport("__Internal")]
    private static extern void DoubleGoldAD();

    [SerializeField] private Button _reviveView;

    [SerializeField] private Button _goldView;

    [SerializeField] private SoundSetting _soundSetting;

    private YandexAD _yandexAD;

    [Inject]
    private void Construct(YandexAD yandexAD)
    {
        _yandexAD = yandexAD;
    }

    private void OnEnable()
    {
        _reviveView.onClick.AddListener(PlayReviveAD);

        _goldView.onClick.AddListener(PlayGoldAD);

        _yandexAD.RewardedADOpened += MuteAudio;

        _yandexAD.RewardedADClosed += ResetAudioVolume;

        _yandexAD.GameWindowMinimazed += MuteAudio;

        _yandexAD.GameWindowFocused += ResetAudioVolume;
    }

    private void OnDisable()
    {
        _reviveView.onClick.RemoveListener(PlayReviveAD);

        _goldView.onClick.RemoveListener(PlayGoldAD);

        _yandexAD.RewardedADOpened -= MuteAudio;

        _yandexAD.RewardedADClosed -= ResetAudioVolume;

        _yandexAD.GameWindowMinimazed -= MuteAudio;

        _yandexAD.GameWindowFocused -= ResetAudioVolume;
    }

    private void PlayReviveAD()
    {
        ReviveRewardedAD();

        DeactivateView(_reviveView);
    }

    private void PlayGoldAD()
    {
        DoubleGoldAD();

        DeactivateView(_goldView);
    }

    private void ResetAudioVolume()
    {
        AudioListener.volume = _soundSetting.Value ? 1f : 0f;
    }

    private void MuteAudio()
    {
        AudioListener.volume = 0f;
    }

    private void DeactivateView(Button button)
    {
        button.gameObject.SetActive(false);
    }
}
