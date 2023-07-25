using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Fun Fact Database", menuName = "Fun Fact Database")]
public class FunFactDatabase : ScriptableObject
{
    public List<FunFactData> funFactsList = new List<FunFactData>();
}


