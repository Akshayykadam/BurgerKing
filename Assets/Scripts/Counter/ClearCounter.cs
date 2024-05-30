using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{

    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            // Empty counter
            if (player.HasKitchenObject())
            {
                // Player is carrying something
                player.GetKitchenObject().SetKitchenObjectParent(this);
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
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) //Checking player
                {
                    //Player is holding plate
                    
                    if (plateKitchenObject.TryAddIngredients(GetKitchenObject().GetKitechObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                    }
                }
                else
                {
                    //Player is not carrying a plate but an ingredients
                    if(GetKitchenObject().TryGetPlate(out plateKitchenObject)) //Checking counter
                    {
                        //Counter is holding a plate
                        if (plateKitchenObject.TryAddIngredients(player.GetKitchenObject().GetKitechObjectSO()))
                        {
                            player.GetKitchenObject().DestroySelf();
                        }

                    }

                }
            }
            else 
            {
                // Player is not carrying anything
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

}
