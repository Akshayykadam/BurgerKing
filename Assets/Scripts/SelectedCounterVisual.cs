using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private GameObject[] VisualObjectArray;
    [SerializeField] private BaseCounter baseCounter;
    private void Start()
    {
        Player.instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
        if (e.selectedCounter == baseCounter)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    void Show()
    {
        foreach (var visualGameObject in VisualObjectArray)
        {
            visualGameObject.SetActive(true);
        }
    }

    void Hide()
    {
        foreach (var visualGameObject in VisualObjectArray)
        {
            visualGameObject.SetActive(false);
        }
    }
}
