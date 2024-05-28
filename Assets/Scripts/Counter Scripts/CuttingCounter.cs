using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter, IHasProgress
{

    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

    public event EventHandler OnCut;

    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;

    private int cuttingProgress;

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
                    cuttingProgress = 0;

                    CuttingRecipeSO CuttingRecipeSO = GettingCuttingRecipeSO(GetKitchenObject().GetKitechObjectSO());

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { 
                        progressNormalized = (float)cuttingProgress/ CuttingRecipeSO.cuttingProgressMax
                    });
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

    public override void InteractAlternate(Player player)
    {
        if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitechObjectSO()))
        {
            cuttingProgress++;

            OnCut?.Invoke(this, EventArgs.Empty);

            // Cut the kitchen object
            CuttingRecipeSO CuttingRecipeSO = GettingCuttingRecipeSO(GetKitchenObject().GetKitechObjectSO());

            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
            {
                progressNormalized = (float)cuttingProgress / CuttingRecipeSO.cuttingProgressMax
            });

            if (cuttingProgress >= CuttingRecipeSO.cuttingProgressMax)
            {
                KitchenObjectSO outputKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitechObjectSO());
                GetKitchenObject().DestroySelf();
                KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
            }            
        }
    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        CuttingRecipeSO CuttingRecipeSO = GettingCuttingRecipeSO(inputKitchenObjectSO);
        return CuttingRecipeSO != null;
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        CuttingRecipeSO CuttingRecipeSO = GettingCuttingRecipeSO(inputKitchenObjectSO);
        if (CuttingRecipeSO != null)
        {
            return CuttingRecipeSO.output;
        }
        else
        {
            return null;
        }
        
    }

    private CuttingRecipeSO GettingCuttingRecipeSO(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (CuttingRecipeSO CuttingRecipeSO in cuttingRecipeSOArray)
        {
            if (CuttingRecipeSO.input == inputKitchenObjectSO)
            {
                return CuttingRecipeSO;
            }
        }
        return null;
    }
}
