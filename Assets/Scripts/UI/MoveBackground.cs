using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackground : MonoBehaviour
{
    [SerializeField]
    private float speed = 0;
    private float currentSpeed;

    [SerializeField]
    private bool moveWithPlayer = false;

    // Start is called before the first frame update
    void Start()
    {
        currentSpeed = speed;

        if (moveWithPlayer)
            GameEvents.current.onChangeSpeedMultiplier += ChangeSpeed;
    }

    void ChangeSpeed(float newSpeedMultiplier)
    {
        currentSpeed = speed * newSpeedMultiplier;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * currentSpeed * Time.deltaTime);
    }

    private void OnDestroy()
    {
        if (moveWithPlayer)
            GameEvents.current.onChangeSpeedMultiplier -= ChangeSpeed;
    }
}
