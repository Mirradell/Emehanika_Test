using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;

public class ScoreController : MonoBehaviour
{
    public int scores;
    public int bestResult;
    public int step;

    public TMP_Text scoreText;

    private string fname = "scores.txt";
    // Start is called before the first frame update
    void Start()
    {
        if (File.Exists(fname))
            bestResult = int.Parse(File.ReadAllText(fname));
        else bestResult = 0;
    }

    private void FixedUpdate()
    {
        scoreText.text = scores.ToString();
    }

    public IEnumerator SaveInFile()
    {
        File.WriteAllText(fname, scores.ToString());
        bestResult = scores;
        scores = 0;
        yield return true;
    }

    public void ScoresIncrease()
    {
        scores += step;
    }

    public void ScoresDecrease()
    {
        scores -= step;
    }
}
