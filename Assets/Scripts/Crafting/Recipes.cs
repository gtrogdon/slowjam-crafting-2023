using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Recipes", menuName = "ScriptableObjects/Recipes", order = 4)]
public class Recipes : ScriptableObject
{
    public Recipe[] RecipeList;
    public Dictionary<string, Recipe> recipes { get; private set; }

    public Recipes()
    {

        recipes = new Dictionary<string, Recipe>();
    }

    public void InitalizeRecipeDictionary()
    {
        if (RecipeList.Length > 0)
        {
            foreach (Recipe r in RecipeList)
            {
                recipes.Add(r.Name(), r);
            }
        }
    }
}