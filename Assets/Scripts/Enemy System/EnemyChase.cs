using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Chaos.Escape
{
    public class EnemyChase : MonoBehaviour
    {
        #region INSPECTOR FIELDS
        
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private Transform target;
        [SerializeField] private EnemyAnimationController enemyAnimationController;
        [SerializeField] private EntityHealth entityHealth;
        
        private Action _idleAnimationState;
        private Action _chaseAnimationState;
        
        private bool _isChasing;
        private bool _isIdle;

        #endregion
        
        #region UNITY METHODS

        private void OnEnable()
        {
            _idleAnimationState = IdleAnimation;
            _chaseAnimationState = ChasingAnimation;
            
            InitializeTarget();
            StartCoroutine(ChaseUpdateInterval());
        }
        
        private void OnDisable()
        {
            StopCoroutine(ChaseUpdateInterval());
        }

        private void Update()
        {
            CheckIfDead();
        }

        private void InitializeTarget()
        {
            target = FindObjectOfType<CharacterRotating>().transform;
        }
        
        private void CheckIfDead()
        {
            if (entityHealth.IsDead)
            {
                agent.speed = 0f;
                agent.SetDestination(agent.transform.position);
                enabled = false;
            }
        }
        
        private IEnumerator ChaseUpdateInterval()
        {
            if(!agent.isOnNavMesh) yield break;
            if (CheckIfTargetReached())
            {
                _idleAnimationState?.Invoke();
                StopChase();
            }
            else
            {
                _chaseAnimationState?.Invoke();
                ChaseTarget();
            }
            
            yield return new WaitForSeconds(0.1f);
            StartCoroutine(ChaseUpdateInterval());
        }

        private void ChaseTarget()
        {
            agent.isStopped = false;
            agent.SetDestination(target.position);
        }
        
        private void StopChase()
        {
            agent.isStopped = true;
        }

        private void ChasingAnimation()
        {
            if (!_isChasing)
            {
                enemyAnimationController.SetAnimationState("Chase");
                _isChasing = true;
                _isIdle = false;
            }
        }
        
        private void IdleAnimation()
        {
            if (!_isIdle)
            {
                enemyAnimationController.SetAnimationState("Idle");
                _isIdle = true; 
                _isChasing = false;
            }
        }
        
        private bool CheckIfTargetReached()
        {
            return Vector3.Distance(transform.position, target.position) < 3f;
        }

        #endregion
    }
}
