using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;    
        
    // Start is called before the first frame update
    void Awake()
    {
        current = this;
    }

    public event Action<int, int> onPlayerHealthChange;
    public void PlayerHealthChange(int newHealth, int maxHealth)
    {
        onPlayerHealthChange?.Invoke(newHealth, maxHealth);
    }

    public event Action<int> onPlayerScoreChange;
    public void PlayerScoreChange(int newScore)
    {
        onPlayerScoreChange?.Invoke(newScore);
    }

    public event Action<Transform> onCheckpointReached;
    public void CheckpointReached(Transform checkpoint)
    {
        onCheckpointReached?.Invoke(checkpoint);
    }

    public event Action<float> onChangeSpeedMultiplier;
    public void ChangeSpeedMultiplier(float newMultiplier)
    {
        onChangeSpeedMultiplier?.Invoke(newMultiplier);
    }

}
