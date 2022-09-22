using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenades : MonoBehaviour
{
    [SerializeField] private float _throwForce;
    [SerializeField] private int _damage;
    private Rigidbody _body;
    [SerializeField] private float _timer;
    [SerializeField] private float _explosionRadius;
    [SerializeField] private LayerMask _playerLayer;
    private PlayerUnit _thrownPlayer;
    private Camera _camera;
    private Vector3 _cameraOrigPos;
    private Quaternion _cameraOrigRot;
    private void Awake()
    {
        _body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        _timer -= Time.deltaTime;
        if(_timer <= 0)
        {
            Debug.Log("Grenade explosion.");
            Collider[] hitObjects = Physics.OverlapSphere(transform.position, _explosionRadius);
            List<PlayerUnit> hitPlayers = new List<PlayerUnit>();
            foreach (Collider collider in hitObjects)
            {
                PlayerUnit player = collider.gameObject.GetComponent<PlayerUnit>();
                if(player != null && !hitPlayers.Contains(player))
                {
                    Debug.Log("Found player");
                    player.TakeDamage(_damage);
                    hitPlayers.Add(player);
                }
            }
            _thrownPlayer.CanMove(true);
            _thrownPlayer.CanDoActions();
            _camera.transform.SetParent(_thrownPlayer.transform);
            _camera.transform.localPosition = _cameraOrigPos;
            _camera.transform.localRotation = _cameraOrigRot;
            PlayerManager.GetInstance().PlayerEndedTurn();
            Destroy(gameObject);
        }
    }
    public void Initialize(PlayerUnit player, Transform point, Camera camera)
    {
        _body.AddForce((point.forward + transform.up) * _throwForce);
        _thrownPlayer = player;
        _thrownPlayer.CanMove(false);
        _camera = camera;
        _cameraOrigPos = _camera.transform.localPosition;
        _cameraOrigRot = _camera.transform.localRotation;
        _camera.transform.parent = transform;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _explosionRadius);
    }
}
