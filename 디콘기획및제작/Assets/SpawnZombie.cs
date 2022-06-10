using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawnZombie : MonoBehaviour
{
    public GameObject zombie;
    private float time = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        

    }
    public void GameStart() {
        StartCoroutine(Spawn());
    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator Spawn() {
        while (true)
        {
            time += Time.deltaTime;
            if (time > 2.0f) {
                Instantiate(zombie, transform.position, transform.rotation);
                time = 0.0f;
            }
            yield return null;
        }

    }
}
