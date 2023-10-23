using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.Networking;
using UnityEditor.PackageManager.Requests;

[System.Serializable]
public class Stat
{
    public int id;
    public int currenHitPoints;
    public int maxHitpoints;
    public int gold;
    public int exp;

    public Stat() { }
    public Stat(int id, int currenHitPoints, int maxHitpoints, int gold, int exp) 
    { 
        this.id = id;
        this.currenHitPoints = currenHitPoints;
        this.maxHitpoints = maxHitpoints;
        this.gold = gold;
        this.exp = exp;
    }
    public IEnumerator LoadDataFromDatabase(string uri, Player player)
    {
        string json; 
        using UnityWebRequest request = UnityWebRequest.Get(uri);
        yield return request.SendWebRequest();
        if (request.error != null) 
        { 
            Debug.LogError($"Nettivirhe: {request.error}"); 
        }
        else
        {
            //json = request.downloadHandler.text;
            string json1 = request.downloadHandler.text.Remove(0, 1);
            string json2 = json1.Remove(json1.Length -1, 1);
            Stat stat = JsonConvert.DeserializeObject<Stat>(json2);
            // P‰ivitet‰‰n pelaajan tilatiedot
            player.Id = stat.id;
            player.CurrentHitpoints = stat.currenHitPoints;
            player.MaxHitPoints = stat.maxHitpoints;
            player.Gold = stat.gold;
            player.Exp = stat.exp;
        }
    }

    public IEnumerator SaveStatToDatabase(string uri, Player player)
    {
        string id = $"\"id\":{this.id},";
        string currentHitPoints = $"\"currentHitPoints\":" +
            $"{this.currenHitPoints},";
        string maxHitPoints = $"\"maxHitpoints\":{this.maxHitpoints},";
        string gold = $"\"gold\":{this.gold},";
        string exp = $"\"exp\":{this.exp}";
        string bodyData = "{" + id + currentHitPoints + maxHitPoints + gold + exp + "}";
        using UnityWebRequest request = UnityWebRequest.Put(uri, bodyData);
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();
        if (request.error != null)
        {
            Debug.LogError($"Nettivirhe: {request.error}");
        }
        else
        {
            Debug.Log("P‰ivitys onnistui!");
        }
    }

}
