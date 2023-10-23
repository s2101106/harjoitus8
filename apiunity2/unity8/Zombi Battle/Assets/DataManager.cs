using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    [field:SerializeField] public Kuvaus myKuvaus {  get; set; }
    public void GetCompleted()
    {
        string uri = $"https://localhost:7227/quest/completed";
        Quest quest = new Quest();
        StartCoroutine(quest.LoadDataNamesFromDatabase(uri));

    }
    public void GetInProgress()
    {
        string uri = $"https://localhost:7227/quest/inProgress";
        Quest quest = new Quest();
        StartCoroutine(quest.LoadDataNamesFromDatabase(uri));
        
    }
    public void GetId(string id)
    {
        string uri = $"https://localhost:7227/quest/{id}";
        Quest quest = new Quest();
        StartCoroutine(quest.LoadDataFromDatabase(uri, myKuvaus));
        Debug.Log(myKuvaus.tehtavaKuvaus+"datamanager debug");
    }
    public void PutData(string id)
    {
        string uri = $"https://localhost:7227/quest/{id}";
        Quest quest = new Quest(myKuvaus.tehtavaId, myKuvaus.tehtavaNimi,
            myKuvaus.palkkioMaara,myKuvaus.tehtavaKuvaus,myKuvaus.kokemusPisteet,
            myKuvaus.onkoAloitettu,myKuvaus.onkoSuoritettu);
        StartCoroutine(quest.SaveQuestToDatabase(uri,myKuvaus));
    }

}
