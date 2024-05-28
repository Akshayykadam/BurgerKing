using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter
{

    private enum state {
        Idel,
        Frying,
        Fried,
        Burned,
    }


    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;

    private float fryingTimer;
    private FryingRecipeSO fryingRecipeSO;

    private void Update()
    {
        if (HasKitchenObject())
        {
            fryingTimer += Time.deltaTime;
            
            if (fryingTimer > fryingRecipeSO.fryingTimerMax)
            {
                fryingTimer = 0f;
                Debug.Log("Fried");
                //fried
                GetKitchenObject().DestroySelf();

                KitchenObject.SpawnKitchenObject(fryingRecipeSO.output, this);
            }
            Debug.Log(fryingTimer);
        }
    }

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            // Empty counter
            if (player.HasKitchenObject())
            {
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitechObjectSO()))
                {
                    // Player is carrying something
                    player.GetKitchenObject().SetKitchenObjectParent(this);

                    fryingRecipeSO = GettingFryingRecipeSO(GetKitchenObject().GetKitechObjectSO());
                }
            }
            else
            {
                // Player is not carrying anything
            }
        }
        else
        {
            // Counter is not empty
            if (player.HasKitchenObject())
            {
                // Player is carrying something
            }
            else
            {
                // Player is not carrying anything
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        FryingRecipeSO FryingRecipeSO = GettingFryingRecipeSO(inputKitchenObjectSO);
        return FryingRecipeSO != null;
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        FryingRecipeSO FryingRecipeSO = GettingFryingRecipeSO(inputKitchenObjectSO);
        if (FryingRecipeSO != null)
        {
            return FryingRecipeSO.output;
        }
        else
        {
            return null;
        }

    }

    private FryingRecipeSO GettingFryingRecipeSO(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (FryingRecipeSO FryingRecipeSO in fryingRecipeSOArray)
        {
            if (FryingRecipeSO.input == inputKitchenObjectSO)
            {
                return FryingRecipeSO;
            }
        }
        return null;
    }
}
