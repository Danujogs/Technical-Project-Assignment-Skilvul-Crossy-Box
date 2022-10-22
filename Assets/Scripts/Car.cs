using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] float speed = 1;
    int extent;
    private void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);

        if (this.transform.position.x < -(extent + 1) || this.transform.position.x > extent + 1)
            Destroy(this.gameObject);

    }

    public void SetUp(int extent)
    {
        this.extent = extent;
    }
}
