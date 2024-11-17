using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class PlayerController : Controller
{
    private CircleCollider2D circleCollider;

    public PlayerHUD playerHUD;

    public float moveSpeed;
    private Vector3 moveDir;

    private float walkSFXTimer;
    private float walkSFXStep = 0.25f;
    public AudioClip[] walkClip;

    //public ParticleSystem walkDusk;
    //private ParticleSystem.MainModule walkDuskEmission;

    protected override void Awake()
    {
        base.Awake();
        circleCollider = GetComponent<CircleCollider2D>();

        //walkDuskEmission = walkDusk.main;
    }

    protected override void Update()
    {
        if (GameManager.instance.isPause) return;

        base.Update();

        UpdateInputValue();
    }

    private void FixedUpdate()
    {
        UpdateMove();
    }

    private void LateUpdate()
    {
        if(playerHUD != null)
            playerHUD.UpdateHUD(this);
    }

    private void UpdateInputValue()
    {
        moveDir = InputValueManager.instance.moveDir;
    }

    private void UpdateMove()
    {
        Vector3 previousPos = transform.position;

        Vector3 horizontal = transform.right * Input.GetAxisRaw("Horizontal");
        Vector3 vertical = transform.up * Input.GetAxisRaw("Vertical");
        moveDir = horizontal + vertical;

        rigidBody.position = transform.position + moveDir * moveSpeed * 0.02f;
        
        bool isWalk = moveDir != Vector3.zero;

        walkSFXTimer += Time.deltaTime;

        if (!isWalk)
            walkSFXTimer = 0;

        if (isWalk && walkSFXTimer >= walkSFXStep)
        {
            walkSFXTimer -= walkSFXStep;
            SoundManager.instance.PlaySFX(walkClip[Random.Range(0, walkClip.Length)]);
        }
    }

    /* Æó±â
    private void MovePosClamp(Vector3 nextPosition)
    {
        Vector3 dir = (nextPosition - transform.position).normalized; //¹æÇâ°ª
        Vector3 detectOffset = dir * circleCollider.radius;
        Vector3 center = transform.position + (Vector3)circleCollider.offset;
        Vector3 fixPos = nextPosition + detectOffset + (Vector3)circleCollider.offset;
    
        Debug.DrawLine(center, fixPos, Color.red);
        RaycastHit2D hit = Physics2D.Linecast(center, fixPos, LayerMask.GetMask("Wall"));
    
        if (hit.collider == null)
            transform.position = nextPosition;
        else
            transform.position = (Vector3)hit.point - detectOffset - (Vector3)circleCollider.offset;
    }
    */

    public override void GetDamage(int damage, Vector3 hitObjectPos, float knockback)
    {
        base.GetDamage(damage, hitObjectPos, knockback);
    }


    protected override void OnDeath()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        foreach(MonoBehaviour script in GetComponents<MonoBehaviour>())
        {
            if(script == this)
                script.enabled = false;
        }
        this.enabled = false;

        
    }
}