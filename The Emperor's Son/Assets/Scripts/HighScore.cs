using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour
{
    // Start is called before the first frame update
    public static int HighScoreValue = 0;
    private Text Highscore;
    void Start()
    {
        Highscore = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ScoreScript.scoreValue > HighScoreValue)
            HighScoreValue = ScoreScript.scoreValue;
     Highscore.text = "H I G H  S C O R E : " + HighScoreValue;
    }
}
