using UnityEngine;

public class ItemController : MonoBehaviour
{
    public float dropSpeed = 3.0f;

    void Update()
    {
        transform.Translate(0f, -dropSpeed * Time.deltaTime, 0f, Space.World);

        if (transform.position.y < -1.0f)
        {
            Destroy(gameObject);
        }
    }
}