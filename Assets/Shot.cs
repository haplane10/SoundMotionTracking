using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Shot : MonoBehaviour
{
    Rigidbody rigid;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rigid.velocity = transform.forward * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Hit");
        }
    }
}
