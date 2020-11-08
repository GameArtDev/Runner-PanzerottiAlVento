using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    TextMeshProUGUI textMesh;

    // Start is called before the first frame update
    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        GameEvents.current.onPlayerHealthChange += OnHealthChange;
    }

    private void OnHealthChange(int health)
    {
        textMesh.text = "Life: " + health;
    }

    private void OnDestroy()
    {
        GameEvents.current.onPlayerHealthChange -= OnHealthChange;
    }
}
