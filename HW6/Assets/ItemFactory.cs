using UnityEngine;

public class ItemFactory : MonoBehaviour
{
    [Header("Prefab 綁定")]
    public GameObject applePrefab;
    public GameObject bombPrefab;

    [Header("可調整生成參數")]
    public float interval = 1.0f;
    public float speed = 3.0f;
    [Range(0f, 1f)]
    public float bombRate = 0.2f;
    public float spawnHeight = 4.0f;

    public bool isSpawning = true;
    private float delta = 0f;

    public void SetParameter(float newInterval, float newSpeed, float newBombRate)
    {
        interval = newInterval;
        speed = newSpeed;
        bombRate = newBombRate;
    }

    void Update()
    {
        if (!isSpawning)
        {
            return;
        }

        delta += Time.deltaTime;
        if (delta >= interval)
        {
            delta = 0f;
            SpawnItem();
        }
    }

    private void SpawnItem()
    {
        GameObject prefabToSpawn = (Random.value < bombRate) ? bombPrefab : applePrefab;
        if (prefabToSpawn == null)
        {
            return;
        }

        int x = Random.Range(-1, 2);
        int z = Random.Range(-1, 2);
        GameObject item = Instantiate(prefabToSpawn, new Vector3(x, spawnHeight, z), Quaternion.identity);

        ItemController controller = item.GetComponent<ItemController>();
        if (controller != null)
        {
            controller.dropSpeed = speed;
        }
    }
}