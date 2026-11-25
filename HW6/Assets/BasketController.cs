using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

public class BasketController : MonoBehaviour
{
    public GameDirector director;
    public bool canMove = true;

    private float basketY;

    void Start()
    {
        basketY = transform.position.y;
    }

    void Update()
    {
        if (!canMove)
        {
            return;
        }

        if (TryGetPointerPosition(out Vector2 screenPos))
        {
            Ray ray = Camera.main.ScreenPointToRay(screenPos);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, Physics.DefaultRaycastLayers, QueryTriggerInteraction.Ignore))
            {
                float x = Mathf.Clamp(Mathf.RoundToInt(hit.point.x), -1, 1);
                float z = Mathf.Clamp(Mathf.RoundToInt(hit.point.z), -1, 1);
                transform.position = new Vector3(x, basketY, z);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!canMove)
        {
            return;
        }

        if (other.CompareTag("Apple"))
        {
            if (director != null)
            {
                director.GetApple();
            }
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Bomb"))
        {
            if (director != null)
            {
                director.GetBomb();
            }
            Destroy(other.gameObject);
        }
    }

    private bool TryGetPointerPosition(out Vector2 screenPos)
    {
#if ENABLE_INPUT_SYSTEM
        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed)
        {
            screenPos = Touchscreen.current.primaryTouch.position.ReadValue();
            return true;
        }
        if (Mouse.current != null && Mouse.current.leftButton.isPressed)
        {
            screenPos = Mouse.current.position.ReadValue();
            return true;
        }
        screenPos = Vector2.zero;
        return false;
#else
        if (Input.touchCount > 0)
        {
            screenPos = Input.GetTouch(0).position;
            return true;
        }
        if (Input.GetMouseButton(0))
        {
            screenPos = Input.mousePosition;
            return true;
        }
        screenPos = Vector2.zero;
        return false;
#endif
    }
}