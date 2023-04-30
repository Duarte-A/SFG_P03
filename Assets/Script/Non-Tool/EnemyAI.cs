using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] public AudioSource _shootSound;

    public NavMeshAgent _agent;

    public Transform _player;

    public LayerMask _whatIsGround, _whatIsPlayer;

    //Patrol Behavior
    public Vector3 _walkPoint;
    bool _walkPointSet;
    public float _walkPointRange;

    //Attack Player Behavior
    public float _timeBetweenAttacks;
    bool _alreadyAttacked;
    public GameObject _projectile;
    public Transform _shootPoint;

    //States
    public float _sightRange, _attackRange;
    public bool _playerInSightRange, _playerInAttackRange;

    private float fixedUpdateCount = 0;


    private void Awake()
    {
        _player = GameObject.Find("WallRunnerPlayer").transform;
        _agent = GetComponent<NavMeshAgent>();
    }

    private void FixedUpdate()
    {
        fixedUpdateCount += 1;

        _playerInSightRange = Physics.CheckSphere(transform.position, _sightRange, _whatIsPlayer);
        _playerInAttackRange = Physics.CheckSphere(transform.position, _attackRange, _whatIsPlayer);

        if (!_playerInSightRange && !_playerInAttackRange) Patrolling();
        if (_playerInSightRange && !_playerInAttackRange) ChasePlayer();
        if (_playerInAttackRange && _playerInSightRange) AttackPlayer();
    }

    public void Patrolling()
    {
        if (!_walkPointSet) SearchWalkPoint();

        if (_walkPointSet)
            _agent.SetDestination(_walkPoint);

        Vector3 _distanceToWalkPoint = transform.position - _walkPoint;

        //Walkpoint reached
        if (_distanceToWalkPoint.magnitude < 1f)
            _walkPointSet = false;
    }
    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-_walkPointRange, _walkPointRange);
        float randomX = Random.Range(-_walkPointRange, _walkPointRange);

        _walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(_walkPoint, -transform.up, 2f, _whatIsGround))
            _walkPointSet = true;
    }

    private void ChasePlayer()
    {
        _agent.SetDestination(_player.position);
    }

    private void AttackPlayer()
    {
        _agent.SetDestination(transform.position);

        transform.LookAt(_player);

        if (!_alreadyAttacked)
        {
            //Actual Attack Code

            Rigidbody rb = Instantiate(_projectile, _shootPoint.position, Quaternion.identity).GetComponent<Rigidbody>();

            rb.AddForce(_shootPoint.forward * 32f, ForceMode.Impulse);
            rb.AddForce(_shootPoint.up * 4f, ForceMode.Impulse);

            //
            _alreadyAttacked = true;
            Invoke(nameof(ResetAttack), _timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        _alreadyAttacked = false;
    }
}