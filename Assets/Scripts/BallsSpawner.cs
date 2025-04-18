using UnityEngine;

public class BallsSpawner : MonoBehaviour
{
    [SerializeField]
    private Transform spawnPoint;

    [SerializeField]
    private Transform[] ballPrefabs;

    [SerializeField]
    private int maxBalls = 10;
    [SerializeField]
    private float range = 0.3f;

    void Start()
    {
        Spawn();
    }
    private void Spawn()
    {
        int currentBallsSpawned = 0;

        for (int i = 0; i < maxBalls; i++)
        {
            float offsetZ = Mathf.Lerp(-range, range, i / (float)(maxBalls - 1));
            float offsetX = Random.Range(-range, range);

            Vector3 spawnPosition = spawnPoint.position + new Vector3(offsetX, 0f, offsetZ);

            Transform ballPrefab = ballPrefabs[currentBallsSpawned % ballPrefabs.Length];
            Instantiate(ballPrefab.gameObject, spawnPosition, Quaternion.identity);
            currentBallsSpawned++;
        }
    }
}
