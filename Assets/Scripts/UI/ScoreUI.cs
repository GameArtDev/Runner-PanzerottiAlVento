using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    private TextMeshProUGUI textMesh;

    [SerializeField]
    private string scoreText = "Score: ";

    // Start is called before the first frame update
    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        GameEvents.current.onPlayerScoreChange += OnScoreChange;
    }

    private void OnScoreChange(int newScore)
    {
        textMesh.text = scoreText + newScore;
    }

    private void OnDestroy()
    {
        GameEvents.current.onPlayerScoreChange -= OnScoreChange;
    }

}