using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryResultUI : MonoBehaviour
{
    [SerializeField] private Image bakgroundImage;
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private Color sucessColor;
    [SerializeField] private Color failColor;
    [SerializeField] private Sprite sucessSprite;
    [SerializeField] private Sprite failSprite;


    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;
        gameObject.SetActive(false);
    }

    private void DeliveryManager_OnRecipeFailed(object sender, System.EventArgs e)
    {
        gameObject.SetActive(true);
        StartCoroutine(SetObjectFalse());
        bakgroundImage.color = failColor;
        iconImage.sprite = failSprite;
        messageText.text = "Delivery\nFailed";
        
    }

    private void DeliveryManager_OnRecipeSuccess(object sender, System.EventArgs e)
    {
        gameObject.SetActive(true);
        StartCoroutine(SetObjectFalse());
        bakgroundImage.color = sucessColor;
        iconImage.sprite = sucessSprite;
        messageText.text = "Delivery\nSuccessful";
        
    }

    private IEnumerator SetObjectFalse()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }
}
