using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    private TextMeshProUGUI textMesh;

    [SerializeField]
    private string healthText = "Life: ";

    // Start is called before the first frame update
    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        GameEvents.current.onPlayerHealthChange += OnHealthChange;
    }

    private void OnHealthChange(int health)
    {
        textMesh.text = healthText + health;
    }

    private void OnDestroy()
    {
        GameEvents.current.onPlayerHealthChange -= OnHealthChange;
    }
}
