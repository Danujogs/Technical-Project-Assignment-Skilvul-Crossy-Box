using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = this.transform.position - player.transform.position;
    }

    Vector3 lastAnimalPos;

    // Update is called once per frame
    void Update()
    {
        if (player.IsDie || lastAnimalPos == player.transform.position)
            return;

        var targetAnimalPos = new Vector3(
            player.transform.position.x,
            0,
            player.transform.position.z
        );

        transform.position = targetAnimalPos + offset;

        lastAnimalPos = player.transform.position;
    }
}
