using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigHealthPack : MonoBehaviour, iHealthPack
{
    [SerializeField] int hp;
    public void Health(PlayerController player)
    {
        player.Heal(hp);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Health(other.gameObject.GetComponent<PlayerController>());
            Destroy(gameObject);
        }
    }
}
