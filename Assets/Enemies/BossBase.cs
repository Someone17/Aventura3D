using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.StateMachine;
using DG.Tweening;
using System;

namespace Boss
{
    public enum BossAction
    {
        INIT,
        IDLE,
        WALK,
        ATTACK,
        DEATH
    }

    public class BossBase : MonoBehaviour
    {
        [Header("Animation")]
        public float startAnimationDuration = .5f;
        public Ease startAnimationEase = Ease.OutBack;

        [Header("Attack")]
        public int attackAmount = 5;
        public float timeBetweenAttacks = .5f;

        public HealthBase healthBase;

        public float speed = 5f;
        public List<Transform> waypoints;

        private StateMachine<BossAction> stateMachine;

        private void OnValidate(){
            if(healthBase == null) healthBase = GetComponent<HealthBase>();
        }

        private void Awake(){
            Init();
            OnValidate();
            healthBase.OnKill += OnBossKill;
        }

        private void Init(){
            stateMachine = new StateMachine<BossAction>();
            stateMachine.Init();

            stateMachine.RegisterStates(BossAction.INIT, new BossStateInit());
            stateMachine.RegisterStates(BossAction.WALK, new BossStateWalk());
            stateMachine.RegisterStates(BossAction.ATTACK, new BossStateAttack());
            stateMachine.RegisterStates(BossAction.DEATH, new BossStateKill());
        }

        private void OnBossKill(HealthBase h){
            SwitchState(BossAction.DEATH);
        }

        public void GoToRandomPoint(Action onArrive = null){
            StartCoroutine(GoToRandomPointCoroutine(waypoints[UnityEngine.Random.Range(0, waypoints.Count)], onArrive));
        }

        IEnumerator GoToRandomPointCoroutine(Transform t, Action onArrive = null){
            while(Vector3.Distance(transform.position, t.position) > 1f){
                transform.position = Vector3.MoveTowards(transform.position, t.position, Time.deltaTime * speed);
                yield return new WaitForEndOfFrame();
            }
            onArrive?.Invoke();
        }

        public void StartAttack(Action endCallback = null){
            StartCoroutine(AttackCoroutine(endCallback));
        }

        IEnumerator AttackCoroutine(Action endCallback = null){
            int attacks = 0;
            while(attacks < attackAmount){
                attacks++;
                transform.DOScale(1.1f, .1f).SetLoops(2, LoopType.Yoyo);
                yield return new WaitForSeconds(timeBetweenAttacks);
            }
            endCallback?.Invoke();
        }

        [NaughtyAttributes.Button]
        private void SwitchInit(){
            SwitchState(BossAction.INIT);
        }

        [NaughtyAttributes.Button]
        private void SwitchWalk(){
            SwitchState(BossAction.WALK);
        }

        [NaughtyAttributes.Button]
        private void SwitchAttack(){
            SwitchState(BossAction.ATTACK);
        }

        public void SwitchState(BossAction state){
            stateMachine.SwitchState(state, this);
        }

        public void StartInitAnimation(){
            transform.DOScale(0, startAnimationDuration).SetEase(startAnimationEase).From();
        }
    }
}
