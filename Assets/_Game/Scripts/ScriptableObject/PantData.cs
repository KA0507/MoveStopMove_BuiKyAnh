using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PantData", menuName = "ScriptableObjects/PantData", order = 1)]
public class PantData : ScriptableObject
{
    public Material[] pant;

    public Material GetMat(int index)
    {
        return pant[index];
    }
}
