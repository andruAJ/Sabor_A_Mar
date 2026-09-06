using UnityEngine;
using System;
using Meta.WitAi;

public class FoodElement : MonoBehaviour
{
    public string food_Name;

    public void Start()
    {
        food_Name = this.tag;
    }

    public string GetName() 
    {
        return food_Name;
    }
}
