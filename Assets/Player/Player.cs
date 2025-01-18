using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Singleton;

public class Player : Singleton<Player>
{
    public Animator animator;

    public CharacterController characterController;
    public float speed = 1f;
    public float turnSpeed = 1f;
    public float gravity = -9.8f;
    public float jumpSpeed = 15f;
    public List<Collider> colliders;

    private float vSpeed = 0f;

    private bool _alive = true;

    [Header("Run Setup")]
    public KeyCode keyRun = KeyCode.LeftShift;
    public float speedRun = 1.5f;

    [Header("Flash")]
    public List<FlashColor> flashColor;

    [Header("Life")]
    public UIHealthUpdater uiHealthUpdater;
    public HealthBase healthBase;


    private void OnValidate(){
        if(healthBase == null) healthBase = GetComponent<HealthBase>();
    }

    protected override void Awake(){
        base.Awake();
        OnValidate();

        healthBase.OnDamage += Damage;
        healthBase.OnKill += OnKill;
    }

    private void OnKill(HealthBase h){
        if(_alive){
            _alive = false;
            animator.SetTrigger("Death");
            colliders.ForEach(i => i.enabled = false);

            Invoke(nameof(Revive), 3f);
        }
    }

    private void Revive(){
        _alive = true;
        healthBase.ResetLife();
        animator.SetTrigger("Revive");
        Respawn();
        Invoke(nameof(TurnOnColliders), 1f);
    }

    private void TurnOnColliders(){
        colliders.ForEach(i => i.enabled = true);

    }

    public void Damage(HealthBase h){
        flashColor.ForEach(i => i.Flash());
        EffectsManager.Instance.ChangeVignette();
    }
    
    public void Damage(float damage, Vector3 dir){
        
       // Damage(damage);
    }

    void Update(){
        transform.Rotate(0, Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime, 0);

        var inputAxisVertical = Input.GetAxis("Vertical");
        var speedVector = transform.forward * inputAxisVertical * speed;

        if(characterController.isGrounded){
            vSpeed = 0;
            if(Input.GetKeyDown(KeyCode.Space)){
                vSpeed =jumpSpeed;
            }
        }

        vSpeed -= gravity * Time.deltaTime;
        speedVector.y = vSpeed;

        var isWalking = inputAxisVertical != 0;
        if(isWalking){
            if(Input.GetKey(keyRun)){
                speedVector *= speedRun;
                animator.speed = speedRun;
            }
            else
            {
                animator.speed = 1;
            }
        } 

        characterController.Move(speedVector * Time.deltaTime);

        if(inputAxisVertical != 0){
            animator.SetBool("Run", true);
        }
        else
        {
            animator.SetBool("Run", false);
        }
    }

    [NaughtyAttributes.Button]
    public void Respawn(){
        if(CheckpointManager.Instance.HasCheckpoint()){
            transform.position = CheckpointManager.Instance.GetPositionFromLastCheckpoint();
        }
    }
}
