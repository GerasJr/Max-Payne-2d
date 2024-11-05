using UnityEngine;

public class SlowMotion : MonoBehaviour
{
    [SerializeField] private float _power;

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.Space))
        {
            Time.timeScale = _power;
            Time.fixedDeltaTime = Time.fixedDeltaTime * Time.timeScale;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
}
