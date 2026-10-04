using System;
using System.Collections.Generic;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<Cell> Cells = new List<Cell>();
    public static GameManager Instance;
    private void Awake()
    {
        if(Instance == null)
        {
            Debug.Log("set as instance");
            DontDestroyOnLoad(this);
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        Cell[] startingCells = FindObjectsByType<Cell>(FindObjectsSortMode.None);
        for(int i = 0; i < startingCells.Length; i++)
        {
            Cells.Add(startingCells[i]);
        }
    }
}
