using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [field: SerializeField] public int Id { get; set; }
    [field: SerializeField] public int CurrentHitpoints { get; set; }
    [field: SerializeField] public int MaxHitPoints { get; set; }
    [field: SerializeField] public int Gold { get; set; }
    [field: SerializeField] public int Exp { get; set; }

}
