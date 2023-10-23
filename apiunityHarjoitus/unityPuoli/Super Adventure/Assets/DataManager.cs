using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    [field: SerializeField] public Player MyPlayer {  get; set; }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L)) 
        { 
            print("Haku"); 
            GetData(1); 
        }
        if (Input.GetKeyDown(KeyCode.K)) 
        { 
            print("Päivitys"); 
            PutData(1); 
        }
    }
    public void GetData(int id) 
    { 
        string uri = $"https://localhost:7150/superadventure/{id}";
        Stat stat = new Stat();
        StartCoroutine(stat.LoadDataFromDatabase(uri, MyPlayer)); 
    }
    public void PutData(int id) 
    { 
        string uri = $"https://localhost:7150/superadventure/{id}";
        Stat stat = new Stat(MyPlayer.Id, MyPlayer.CurrentHitpoints,
        MyPlayer.MaxHitPoints, MyPlayer.Gold, MyPlayer.Exp);
        StartCoroutine(stat.SaveStatToDatabase(uri, MyPlayer)); 
    }
}
