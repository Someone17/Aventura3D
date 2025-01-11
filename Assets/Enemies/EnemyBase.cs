using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Animation;

namespace Enemy{

    public class EnemyBase : MonoBehaviour, IDamageable
    {
        [Header("Animation")]
        [SerializeField]private AnimationBase _animationBase;
        
        public Collider collider;

        public FlashColor flashColor;
        public ParticleSystem particleSystem;

        private Player _player;

        public float startLife = 10f;
        public bool lookAtPlayer = false;

        [SerializeField] private float _currentLife;

        [Header("Start Animation")]
        public float startAnimationDuration = .2f;
        public Ease startAnimationEase = Ease.OutBack;
        public bool startWithBornAnimation = true;

        private void Awake(){
            Init();
            
            if(startWithBornAnimation)
                BornAnimation();
        }

        protected void ResetLife(){
            _currentLife = startLife;
        }

        protected virtual void Init(){
            ResetLife();
            _player = GameObject.FindObjectOfType<Player>();
        }

        protected virtual void Kill(){
            OnKill();
        }

        protected virtual void OnKill(){
            if(collider != null) collider.enabled = false;
            Destroy(gameObject, 3f);
            PlayAnimationByTrigger(AnimationType.DEATH);
        }

        public void OnDamage(float f){
            if(flashColor != null) flashColor.Flash();
            if(particleSystem != null) particleSystem.Emit(15);

            transform.position -= transform.forward;

            _currentLife -= f;

            if(_currentLife > 0){
                Kill();
            }
        }

        private void BornAnimation(){
            transform.DOScale(0, startAnimationDuration).SetEase(startAnimationEase).From();
        }

        public void PlayAnimationByTrigger(AnimationType animationType){
            _animationBase.PlayAnimationByTrigger(animationType);
        }

        public virtual void Update(){
            if(Input.GetKeyDown(KeyCode.T)){
                OnDamage(5f);
            }
            
            if(lookAtPlayer){
                transform.LookAt(_player.transform.position);
            }
        }

        public void Damage(float damage){
            Debug.Log("Damage");
            OnDamage(damage);
        }

        public void Damage(float damage, Vector3 dir){
            transform.DOMove(transform.position - dir, .1f);
        }

        private void OnCollisionEnter(Collision collision){
            Player p = collision.transform.GetComponent<Player>();

            if(p != null){
                p.Damage(1);
            }
        }
    }
}
