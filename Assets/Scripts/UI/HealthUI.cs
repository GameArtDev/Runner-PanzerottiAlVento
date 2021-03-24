using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField]
    private Image healthBar;
    [SerializeField]
    private SpriteRenderer healthFrame;
    [SerializeField]
    private Sprite aliveFrame;
    [SerializeField]
    private Sprite deadFrame;

    //private TextMeshProUGUI textMesh;

    //[SerializeField]
    //private string healthText = "Life: ";

    // Start is called before the first frame update
    void Start()
    {
        //textMesh = GetComponent<TextMeshProUGUI>();
        GameEvents.current.onPlayerHealthChange += OnHealthChange;
    }

    private void OnHealthChange(int health, int maxHealth)
    {
        if (health > 0)
        {
            healthFrame.sprite = aliveFrame;
        } else
        {
            healthFrame.sprite = deadFrame;
        }
        healthBar.fillAmount = health / (float) maxHealth;
    }

    private void OnDestroy()
    {
        GameEvents.current.onPlayerHealthChange -= OnHealthChange;
    }
}
