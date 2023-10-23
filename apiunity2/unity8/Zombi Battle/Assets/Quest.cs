using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.Networking;
using UnityEditor.PackageManager.Requests;
using Newtonsoft.Json.Linq;
using Microsoft.CSharp;
[System.Serializable]
public class Quest
{
    public int tehtavaId;
    public string tehtavaNimi;
    public int palkkioMaara;
    public string tehtavaKuvaus;
    public int kokemusPisteet;
    public bool onkoAloitettu;
    public bool onkoSuoritettu;
    player myPlayer=GameObject.Find("player").GetComponent<player>();
    
    public Quest() { }
    
    public Quest(int tehtavaId, string tehtavaNimi, int palkkioMaara, string tehtavaKuvaus,
        int kokemusPisteet, bool onkoAloitettu,  bool onkoSuoritettu)
    {
        this.tehtavaId = tehtavaId;
        this.tehtavaNimi = tehtavaNimi;
        this.palkkioMaara = palkkioMaara;
        this.tehtavaKuvaus = tehtavaKuvaus;
        this.kokemusPisteet = kokemusPisteet;
        this.onkoAloitettu = onkoAloitettu;
        this.onkoSuoritettu = onkoSuoritettu;
    }
    public IEnumerator LoadDataFromDatabase(string uri, Kuvaus kuvaus)
    {
        using UnityWebRequest request=UnityWebRequest.Get(uri);
        yield return request.SendWebRequest();
        if (request.error != null)
        {
            Debug.LogError($"Nettivirhe: {request.error}");
        }
        else
        {
            string json1 = request.downloadHandler.text;
            Quest quest=JsonConvert.DeserializeObject<Quest>(json1);
            
            kuvaus.tehtavaId=quest.tehtavaId;
            kuvaus.tehtavaNimi=quest.tehtavaNimi;
            kuvaus.palkkioMaara=quest.palkkioMaara;
            kuvaus.tehtavaKuvaus=quest.tehtavaKuvaus;
            kuvaus.kokemusPisteet=quest.kokemusPisteet;
            kuvaus.onkoAloitettu=quest.onkoAloitettu;
            kuvaus.onkoSuoritettu = quest.onkoSuoritettu;
            Debug.Log(kuvaus.tehtavaKuvaus+"quest debug");
        }
    }
    public IEnumerator LoadDataNamesFromDatabase(string uri)
    {
        using UnityWebRequest request = UnityWebRequest.Get(uri);
        yield return request.SendWebRequest();
        if (request.error != null)
        {
            Debug.LogError($"Nettivirhe: {request.error}");
        }
        else
        {
            string json1 = request.downloadHandler.text;
            myPlayer.Listaus1 = JsonConvert.DeserializeObject<List<Quest>>(json1);
        }
    }
    public IEnumerator SaveQuestToDatabase(string uri, Kuvaus kuvaus) 
    { 
        string id=$"\"tehtavaId\":{this.tehtavaId},";
        string nimi=$"\"tehtavaNimi\":\"{this.tehtavaNimi}\",";
        string palkkio = $"\"palkkioMaara\":{this.palkkioMaara},";
        string tkuvaus = $"\"tehtavaKuvaus\":\"{this.tehtavaKuvaus}\",";
        string kokemus=$"\"kokemusPisteet\":{this.kokemusPisteet},";
        string aloitettu = $"\"onkoAloitettu\":{this.onkoAloitettu.ToString()},";
        string suoritettu = $"\"onkoSuoritettu\":{this.onkoSuoritettu.ToString()}";

        string bodyData = "{" + id + nimi + palkkio + tkuvaus + kokemus
             + aloitettu.ToLower() + suoritettu.ToLower() + "}";
        Debug.Log(bodyData);
        using UnityWebRequest request = UnityWebRequest.Put(uri, bodyData);
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();
        if(request.error != null)
        {
            Debug.LogError($"Nettivirhe: {request.error}");
        }
        else
        {
            Debug.Log("Päivitys onnistui!");
        }

    }
}
