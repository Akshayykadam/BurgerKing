using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class OptionUI : MonoBehaviour
{
    public static OptionUI Instance { get; private set; }

    [SerializeField] private Slider soundEffectSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Button closeButton;

    [SerializeField] private TextMeshProUGUI soundEffectText;
    [SerializeField] private TextMeshProUGUI musicEffectText;

    private Action OnCloseButtonAction;

    private void Awake()
    {
        Instance = this;

        soundEffectSlider.onValueChanged.AddListener(delegate {
            SoundManager.Instance.ChangeVolume(soundEffectSlider.value);
            UpdateVisual();
        });

        musicSlider.onValueChanged.AddListener(delegate {
            MusicManager.Instance.ChangeVolume(musicSlider.value);
            UpdateVisual();
        });

        closeButton.onClick.AddListener(() => {
            Hide();
            OnCloseButtonAction();
        });
    }

    private void Start()
    {
        GameManager.Instance.OnGameUnpaused += GameManager_OnGameUnpaused;
        UpdateVisual();
        Hide();
    }

    private void GameManager_OnGameUnpaused(object sender, System.EventArgs e)
    {
        Hide();
    }

    public void UpdateVisual()
    {
        soundEffectSlider.value = SoundManager.Instance.GetVolume();
        musicSlider.value = MusicManager.Instance.GetVolume();

        soundEffectText.text = "Sound Effect Volume: " + Mathf.Round(SoundManager.Instance.GetVolume() * 10f).ToString();
        musicEffectText.text = "Music Volume: " + Mathf.Round(MusicManager.Instance.GetVolume() * 10f).ToString();
    }

    public void Show(Action OnCloseButtonAction)
    {
        this.OnCloseButtonAction = OnCloseButtonAction;
        gameObject.SetActive(true);
        closeButton.Select();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
