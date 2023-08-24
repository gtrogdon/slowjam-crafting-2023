﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CraftingManager : MonoBehaviour
{
    public static CraftingManager Instance { get; private set; }
    public Recipes Recipes;
    public List<ItemSO> currentIngredients { get; private set; }
    public Recipe currentRecipe { get; private set; }

    public delegate void OnItemChanged();
    public OnItemChanged OnItemChangedCallback;
    public delegate void OnRecipeChanged();
    public OnRecipeChanged OnRecipeChangedCallback;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        currentIngredients = new List<ItemSO>();
    }

    public void AddIngredient(ItemSO item)
    {
        currentIngredients.Add(item);
        if (OnItemChangedCallback != null)
        {
            OnItemChangedCallback.Invoke();
        }
        TryCrafting();
    }

    public void RemoveIngredient(ItemSO item)
    {
        currentIngredients.Remove(item);
        if (OnItemChangedCallback != null)
        {
            OnItemChangedCallback.Invoke();
        }
        TryCrafting();
    }

    private void TryCrafting()
    {
        foreach (Recipe recipe in Recipes.RecipeList)
        {
            bool success = TryCrafting(recipe);
            if (success)
            {
                Debug.Log("Can craft: " + currentRecipe.Name());
                if (OnRecipeChangedCallback != null)
                {
                    OnRecipeChangedCallback.Invoke();
                }
                return;
            }
        }
        Debug.Log("Craft Fail");
        if (currentRecipe && OnRecipeChangedCallback != null)
        {
            currentRecipe = null;
            OnRecipeChangedCallback.Invoke();
        }
        return;
    }

    private bool TryCrafting(Recipe recipe)
    {
        if (recipe.CheckIngredients(currentIngredients))
        {
            currentRecipe = recipe;
            return true;
        }
        else
        {
            Debug.LogWarning("Unable to Craft this Recipe");
            return false;
        }
    }

    public void Craft()
    {
        if(currentRecipe)
        {
            InstantiateOutput(currentRecipe);
        }
    }

    public void InstantiateOutput(Recipe recipe)
    {
        Debug.Log("Creating " + recipe.Name());
        InventoryManager.Instance.Add(recipe.OutputItem);
        ClearCurrentIngredients();
        if (OnItemChangedCallback != null)
        {
            OnItemChangedCallback.Invoke();
        }
    }

    private void ClearCurrentIngredients()
    {
        foreach (ItemSO item in currentIngredients)
        {
            if (item.Type == ItemType.TOOL)
            {
                InventoryManager.Instance.Add(item);
            }    
        }
        currentIngredients.Clear();
    }
}
