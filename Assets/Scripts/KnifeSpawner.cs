using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeSpawner : MonoBehaviour
{
    public GameObject knife;

    private float min_X = -2.7f;
    private float max_X = 2.7f;

    /**
        Initializes the knife spawner

        @returns {void}
     */
    void Start()
    {
        StartCoroutine(StartSpawning());
    }

    /**
        Coroutine that calls itself recursively for instantiating knife 
        gameObjects between the min and max X bounds 

        @returns {IEnumerator} returns a random time delay before continuing 
        execution
     */
    IEnumerator StartSpawning()
    {
        yield return new WaitForSeconds(Random.Range(0.5f, 1f));
        GameObject k = Instantiate(knife);
        float x = Random.Range(min_X, max_X);
        k.transform.position = new Vector2(x, transform.position.y);
        StartCoroutine(StartSpawning());
    }
}
