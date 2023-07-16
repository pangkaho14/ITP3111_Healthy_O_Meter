using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevivalQuizSelector : MonoBehaviour
{
    public GameObject hawkerRevivalQuiz;
    public GameObject ntucRevivalQuiz;
    
    // Start is called before the first frame update
    void Start()
    {
        int selectedScene = PlayerPrefs.GetInt("selectedScenarioName");

        if (selectedScene == 1)
        {
            // create hawker revival quiz
            Instantiate(hawkerRevivalQuiz, new Vector3(0, 0, 0), Quaternion.identity);
        }
        else if (selectedScene == 0)
        {
            // create NTUC revival quiz
            Instantiate(ntucRevivalQuiz, new Vector3(0, 0, 0), Quaternion.identity);
        }
    }
}
