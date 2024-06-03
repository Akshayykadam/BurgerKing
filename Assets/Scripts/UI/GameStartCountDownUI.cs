using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameStartCountDownUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countdownText;

    private Animator animator;
    private int previousCountdownNumber;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
        Hide();
    }

    private void Update()
    {
        int CountDownNumber = Mathf.CeilToInt(GameManager.Instance.GetCountdownStartTimer());
        countdownText.text = CountDownNumber.ToString();

        if(previousCountdownNumber != CountDownNumber)
        {
            previousCountdownNumber = previousCountdownNumber;
            animator.SetTrigger("NumberPopup");
            //SoundManager.Instance.CountdownSound();
        }
    }

    private void GameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.IsCountDownTosStartActive())
        {
            Show();
        }
        else {
            Hide();
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
