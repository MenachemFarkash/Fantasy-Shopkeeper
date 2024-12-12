using UnityEngine;

public class CustomerSpawner : MonoBehaviour {
    public GameObject CustomerPrefab;
    public BoxCollider spawnAreaCollider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        InvokeRepeating(nameof(SpawnCustomer), 5, 5);
    }

    public void SpawnCustomer() {
        Instantiate(CustomerPrefab, new Vector3(Random.Range(0, 3), 0, Random.Range(0, 3)), Quaternion.identity);
    }
}
