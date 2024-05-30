using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlateKitchenObject : KitchenObject
{

    public event EventHandler<OnIngredientsAddedArgs> OnIngredientsAdded;
    public class OnIngredientsAddedArgs : EventArgs {
        public KitchenObjectSO kitchenObjectSO;
    }

    [SerializeField] private List<KitchenObjectSO> validKitchenObjectSOList;
    private List<KitchenObjectSO> kitchenObjectsList;

    private void Awake()
    {
        kitchenObjectsList = new List<KitchenObjectSO>();
    }

    public bool TryAddIngredients(KitchenObjectSO kitchenObjectSO)
    {
        if (!validKitchenObjectSOList.Contains(kitchenObjectSO))
        {
            // Not Valid Incredients
            return false;
        }
        if (kitchenObjectsList.Contains(kitchenObjectSO))
        {
            //already has this object
            return false;
        }
        else
        {
            kitchenObjectsList.Add(kitchenObjectSO);
            OnIngredientsAdded?.Invoke(this, new OnIngredientsAddedArgs
            {
                kitchenObjectSO = kitchenObjectSO
            });
            return true;

        }
    }


    public List<KitchenObjectSO> GetKitchenObjectSOList()
    {
        return kitchenObjectsList;
    }
}
