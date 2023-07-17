using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RevivalQuizData", menuName = "RevivalQuizData", order = 1)]
public class RevivalQuizData : ScriptableObject
{
    public int HealAmount = 20;
    public int Attempts = 1;
}
