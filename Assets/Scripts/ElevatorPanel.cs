using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorPanel : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer _panelButton;
    [SerializeField]
    private int _coinsToCollect = 8;
    [SerializeField]
    private Elevator _elevator;
    [SerializeField]
    private Player _player;
    private bool _inZone = false;
    private bool _elevatorCalled = false;
    private bool _canInteract = true;
    private bool _elevatorNotMoving = true;
    private int _timesDisplayedText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _inZone = true;

            if (_player.Coins() >= _coinsToCollect)
                UIManager.Instance.DisplayInteractText(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _inZone = false;
            UIManager.Instance.DisplayInteractText(false);
        }
    }

    private void Update()
    {
        if (_inZone)
        {

            if (Input.GetKeyDown(KeyCode.E) && _player.Coins() >= _coinsToCollect && _canInteract)
            {
                if (_elevatorCalled)
                {
                    _panelButton.material.color = Color.red;
                }
                else
                {
                    _panelButton.material.color = Color.green;
                }

                _elevatorCalled = true;
                _elevatorNotMoving = false;
                _elevator.CallElevator();

                StartCoroutine(InteractionRoutine());
                UIManager.Instance.DisplayInteractText(false);

            }

        }

    }

    IEnumerator InteractionRoutine()
    {
        _canInteract = false;
        yield return new WaitUntil(()=>_elevatorNotMoving);
        _canInteract = true;
    }

    public void ElevatorNotMoving()
    {
        _elevatorNotMoving = true;
        if (_timesDisplayedText == 0)
            UIManager.Instance.DisplayInteractText(true);
        _timesDisplayedText = 1;
    }

}
