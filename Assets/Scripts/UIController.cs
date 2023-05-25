using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameObject player;
    public GameObject start;
    public GameObject finish;

    public TMPro.TMP_Text finishText;
    public GameObject scores;
    public ScoreController scoreController;

    private void Start()
    {
        if (player == null)
            Debug.LogError("����� �� �������� � UI!");

        start.SetActive(true);
        scores.SetActive(false);
        finish.SetActive(false);
    }
    public void OnStartPressed()
    {
        player.GetComponent<StarterAssets.ThirdPersonController>().enabled = true;
        start.SetActive(false);
        scores.SetActive(true);
    }

    public void GameEnd()
    {
        scores.SetActive(false);

        if (scoreController.bestResult < scoreController.scores)
        {
            finishText.text = $"�����������!\n�� ���������� ����� ������: {scoreController.scores}";
            StartCoroutine(scoreController.SaveInFile());
        }
        else
            finishText.text = $"�� ������� {scoreController.scores} �����.\n������� ������: {scoreController.bestResult}";
        finish.SetActive(true);

        player.GetComponent<StarterAssets.ThirdPersonController>().enabled = false;
    }

}
