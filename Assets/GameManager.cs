using System.Collections.Generic;
using NUnit.Framework;
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
    }
}
