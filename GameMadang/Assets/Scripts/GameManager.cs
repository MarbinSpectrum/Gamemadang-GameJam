using System;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public int ClearStage=1;
    public Action OnLife;

    public void UpdateLife()
    {
        OnLife?.Invoke();
    }

    
   
}
