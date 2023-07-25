using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ScenarioDatabase : ScriptableObject
{
    //Create Array to store hawker or supermarket buttons. 
    public Scenario[] Scene;

    public int ScenarioCount
    {
        get
        {
            return Scene.Length;
        }
    }

    public Scenario GetScene(int index)
    {
        return Scene[index];
    }
}
