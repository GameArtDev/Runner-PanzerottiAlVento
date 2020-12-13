using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackground : MonoBehaviour
{
    [SerializeField]
    private float speed = 0;
    private float currentSpeed;

    // Start is called before the first frame update
    void Start()
    {
        currentSpeed = speed;
        //GameEvents.current.onChangeSpeedMultiplier += ChangeSpeed;
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
        //GameEvents.current.onChangeSpeedMultiplier -= ChangeSpeed;
    }
}
