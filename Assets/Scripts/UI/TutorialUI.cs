using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    private void Start()
    {
        GameManager.Instance.OnStateChanged += Instance_OnStateChanged;
        Show();
    }

    private void Instance_OnStateChanged(object sender, System.EventArgs e)
    {
        if(GameManager.Instance.IsCountDownTosStartActive())
        {
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
