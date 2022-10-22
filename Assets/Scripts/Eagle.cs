using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eagle : MonoBehaviour
{
    [SerializeField] private float speed = 1;

    Player player;

    void Update()
    {
        if (this.transform.position.z <= player.CurrentTravel - 20)
            return;

        transform.Translate(Vector3.forward * Time.deltaTime * speed);

        if (this.transform.position.z <= player.CurrentTravel &&
        player.gameObject.activeInHierarchy)
        {
            //player.gameObject.SetActive(false);
            player.transform.SetParent(this.transform);
        }
    }

    public void SetUpTarget(Player target)
    {
        this.player = target;
    }
}
