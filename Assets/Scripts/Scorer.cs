using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Scorer : MonoBehaviour
{
    Text txt;
    int score;

    void Start()
    {
        txt = GetComponent<Text>();
    }

    void Update()
    {
        txt.text = "Score: " + score;
    }

    public void givePoint(int points)
    {
        score += points;
    }
}
