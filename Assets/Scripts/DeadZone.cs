using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    [SerializeField]
    private GameObject _respawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                player.Damage();
            }

            CharacterController characterController = player.GetComponent<CharacterController>();

            if (characterController != null)
            {
                characterController.enabled = false;
            }

            other.transform.position = _respawnPoint.transform.position;

            StartCoroutine(CCRoutine(characterController));
        }
    }

    IEnumerator CCRoutine(CharacterController cc)
    {
        yield return new WaitForSeconds(0.5f);
        cc.enabled = true;
    }
}
