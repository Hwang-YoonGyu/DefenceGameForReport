using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("dest", 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.back * 100 * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "field") {
            Destroy(this.gameObject);
        }
        if (other.tag == "zombie") {
            Destroy(this.gameObject);
        }
    }
    private void dest()
    {
        Destroy(this.gameObject);
    }
}
