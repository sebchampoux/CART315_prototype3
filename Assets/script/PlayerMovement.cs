using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _baseSpeed = 4.0f;
    [SerializeField] private float _sprintSpeed = 8.0f;
    [SerializeField] private float _rotationSpeed = 5.0f;
    [HideInInspector] public Vector3 startPos;

    private float _gaz = 100.0f;
    private float _maxGaz = 100.0f;

    public float _currentDanger = 0;
    public float _maxDanger = 100.0f;

    private bool _touchable = true;
    private GameObject _camera;
    private Keyboard _keyboard;
    private PlayerStatus _playerStatus;
    private GameObject[] _possibleDangers;

    public event EventHandler MovementStatsUpdate;

    public float Gaz
    {
        get { return _gaz; }
        private set
        {
            _gaz = Mathf.Max(0, value);
            OnMovementStatsUpdate();
        }
    }

    public float Danger
    {
        get { return _currentDanger; }
        private set
        {
            _currentDanger = Mathf.Max(0, value);
            OnMovementStatsUpdate();
        }
    }

    public float MaxGaz
    {
        get { return _maxGaz; }
    }
    public float MaxDanger
    {
        get { return _maxDanger; }
    }
    public bool Touchable
    {
        get { return _touchable; }
    }

    protected void OnMovementStatsUpdate()
    {
        MovementStatsUpdate?.Invoke(this, EventArgs.Empty);
    }

    void Start()
    {
        _playerStatus = GetComponent<PlayerStatus>();
        RetrieveKeyboard();
        StartCoroutine(DistanceComparison());
    }

    private void RetrieveKeyboard()
    {
        _keyboard = Keyboard.current;
        if (_keyboard == null)
        {
            throw new UnityException("Keyboard not detected, not cool");
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("oeuf"))
        {
            PickUpEgg(collider.gameObject);
        }
        else if (collider.gameObject.CompareTag("DropLocation"))
        {
            DropLocation dropLocation = collider.gameObject.GetComponent<DropLocation>();
            _playerStatus.DropEggs(dropLocation);
        }
    }

    private void PickUpEgg(GameObject egg)
    {
        _playerStatus.PickUpEgg();
        Destroy(egg.gameObject);
    }

    private void ListenForInputs()
    {
        if (_keyboard == null)
        {
            throw new UnityException("No keyboard detected");
        }
        if (_keyboard.dKey.isPressed)
        {
            transform.Rotate(Vector3.up * _rotationSpeed, Space.World);
        }
        if (_keyboard.aKey.isPressed)
        {
            transform.Rotate(Vector3.up * -1 * _rotationSpeed, Space.World);
        }
        if (_keyboard.wKey.isPressed)
        {
            MoveTractorForward();
        }
    }

    private void MoveTractorForward()
    {
        RaycastHit hit;
        if (TractorHitsObject(out hit))
        {
            string hitTag = hit.transform.tag;
            if (hitTag.Equals("Danger") || hitTag.Equals("obstacle") || hitTag.Equals("DropLocation"))
            {
                return;
            }
        }
        if (_keyboard.leftShiftKey.isPressed && HasGazLeft())
        {
            MoveForwardWithBoost();
        }
        else
        {
            transform.Translate(Vector3.forward * _baseSpeed * Time.deltaTime);
        }
    }

    private bool TractorHitsObject(out RaycastHit hit)
    {
        return Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 1f);
    }

    private bool HasGazLeft()
    {
        return Gaz > 0;
    }

    private void MoveForwardWithBoost()
    {
        transform.Translate(Vector3.forward * _sprintSpeed * Time.deltaTime);
        Gaz--;
        if (!HasGazLeft())
        {
            return;
        }
    }

    void Update()
    {
        ListenForInputs();

        if (_playerStatus.Lives == 0)
            mort();

        if (Gaz < _maxGaz && !Keyboard.current.leftShiftKey.isPressed)
            Gaz += 0.5f;

        if (Danger > 95.0f && _touchable == true)
            perdVie();

        if (Danger > _maxDanger)
            Danger = _maxDanger;

        if (Danger > 50.0f)
        {
            //camera1.GetComponent<Grayscale>().enabled = true;
        }
        else
        {
            //camera1.GetComponent<Grayscale>().enabled = false;
        }
    }

    public IEnumerator DistanceComparison()
    {
        while (true)
        {
            _possibleDangers = GameObject.FindGameObjectsWithTag("Danger");
            yield return new WaitForSeconds(0.01f);

            float distanceFromPlayer;

            for (int x = 0; x < _possibleDangers.Length; x++)
            {

                distanceFromPlayer = Vector3.Distance(this.transform.position, _possibleDangers[x].transform.position);

                if (distanceFromPlayer < 7.5f)
                {
                    if (distanceFromPlayer < 5f)
                    {
                        if (distanceFromPlayer < 3.5f)
                        {
                            if (distanceFromPlayer < 1.5f)
                            {
                                if (Danger < _maxDanger)
                                    Danger += 1f;
                            }
                            else if (Danger < _maxDanger)
                                Danger += 0.5f;
                        }
                        else if (Danger < _maxDanger)
                            Danger += 0.25f;
                    }
                    else if (Danger < _maxDanger)
                        Danger += 0.15f;
                }
            }

            if (Danger > 0.0f)
            {
                Danger -= 1f;
            }

        }
    }

    public void perdVie()
    {
        StartCoroutine(immunity());
        _playerStatus.LoseLife();
    }

    public IEnumerator immunity()
    {
        _touchable = false;
        yield return new WaitForSeconds(2);
        _touchable = true;
    }

    public void mort()
    {
        _playerStatus.SetMaximumLives();
        transform.position = startPos;
        Gaz = _maxGaz;
        _playerStatus.SetMaximumLives();
    }
}
