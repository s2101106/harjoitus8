using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kuvaus : MonoBehaviour
{

    [field: SerializeField] public int tehtavaId { get; set; }
    [field: SerializeField] public string tehtavaNimi { get; set; }
    [field: SerializeField] public int palkkioMaara {  get; set; }
    [field: SerializeField] public string tehtavaKuvaus {  get; set; }
    [field: SerializeField] public int kokemusPisteet {  get; set; }
    [field: SerializeField] public bool onkoAloitettu {  get; set; }
    [field: SerializeField] public bool onkoSuoritettu { get; set; }

}
