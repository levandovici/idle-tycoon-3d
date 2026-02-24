using UnityEngine;
using UnityEngine.AI;

public class Human : MonoBehaviour
{
    [SerializeField]
    private float _moveRadius = 20f;

    [SerializeField]
    private NavMeshAgent _agent;

    [SerializeField]
    private Animator _animator;



    private void Awake()
    {
        SetRandomPoint();
    }

    private void Update()
    {
        if (!_agent.pathPending && _agent.remainingDistance <= _agent.stoppingDistance)
        {
            SetRandomPoint();
        }

        if (_animator != null)
        {
            _animator.SetBool("Walking", _agent.velocity.magnitude > 0.1f);
        }
    }



    private void SetRandomPoint()
    {
        Vector3 target = transform.position + Random.insideUnitSphere * _moveRadius;

        NavMeshHit hit;

        if (NavMesh.SamplePosition(target, out hit, 100f, NavMesh.AllAreas))
        {
            _agent.SetDestination(hit.position);
        }
    }
}