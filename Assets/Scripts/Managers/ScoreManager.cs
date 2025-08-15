using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
public class ScoreManager : MonoBehaviour
{
    public static int score;


    TMP_Text text;


    void Awake ()
    {
        text = GetComponent <TMP_Text> ();
        score = 0;
    }


    void Update ()
    {
        text.text = "Score: " + score;
    }
}
