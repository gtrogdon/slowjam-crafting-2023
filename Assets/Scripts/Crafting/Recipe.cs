using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Recipe", menuName = "ScriptableObjects/Recipe", order = 3)]
public class Recipe : ScriptableObject
{
    public ItemSO OutputItem;
    public List<ItemSO> Ingredients;

    public bool CheckIngredients(List<ItemSO> inputs)
    {
        if (Ingredients.Count <= inputs.Count)
        {
            foreach (ItemSO item in Ingredients)
            {
                if (!inputs.Contains(item))
                {
                    return false;
                }
            }
            return true;
        }
        return false;
    }

    public string Name()
    {
        return OutputItem.Name;
    }
}



