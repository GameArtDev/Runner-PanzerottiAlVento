using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    private static TextMeshProUGUI textMesh;
    private static string scoreText = "Gems: ";
    private static int count = 0;

    // Start is called before the first frame update
    void Awake()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        textMesh.text = scoreText + count;
    }

    public static void OnAddDiamound()
    {
        count++;
        textMesh.text = scoreText + count;
    }

}