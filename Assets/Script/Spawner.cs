using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Spawner : MonoBehaviour
{
    private float minBoundsX;
    private float maxBoundsX;

    private BoxCollider spawnerCollider;

    public GameObject[] fruits;

    public GameObject bomb;
    public float spawnBombChance;

    public float minForce, maxForce;

    public float minAngle, maxAngle;

    public float maxLifeTime;
    private void OnEnable()
    {
        spawnerCollider = GetComponent<BoxCollider>();

        StartCoroutine(Spawn());

        minBoundsX = spawnerCollider.bounds.min.x;
        maxBoundsX = spawnerCollider.bounds.max.x;

    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator Spawn()
    {
        //Delay for 2 second when start the game
        yield return new WaitForSeconds(1f);
        while (enabled)
        {
            yield return new WaitForSeconds(1f);
            //Spawn fruit
            int index = Random.Range(0, fruits.Length);
            GameObject prefab = fruits[index];

            //Spawn bomb
            if(Random.value < spawnBombChance)
            {
                prefab = bomb;
            }

            //Grab original position
            Vector3 newPosition = transform.position;
            newPosition.x = Random.Range(minBoundsX, maxBoundsX); //clamp random spawn position

            float angle = Random.Range(minAngle, maxAngle);
            Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            GameObject g = Instantiate(prefab, newPosition, rotation);
            Rigidbody rb = g.GetComponent<Rigidbody>();

            float force = Random.Range(minForce, maxForce);

            rb.AddForce(force * g.transform.up, ForceMode.Impulse);

            Destroy(g, maxLifeTime);
        }
    }
}
