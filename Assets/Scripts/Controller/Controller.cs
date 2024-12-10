using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Controller : MonoBehaviour
{
    public GameObject damageEffect;
    public GameObject deathEffect;
    protected Rigidbody2D rigidBody;
    protected Animator animator;

    public bool isDeath;

    public float maxHP;
    private float _hp;
    public float hp
    {
        get{ return _hp; }
        set{ _hp = Mathf.Min(value, maxHP); }
    }

    public bool isInvincibility;

    protected virtual void Awake()
    {
        hp = maxHP; 
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    protected virtual void Update()
    {
        if (GameManager.instance.isPause) return;
    }

    public virtual void GetDamage(float damage, Vector3 hitObjectPos = new Vector3(), float knockback = 0)
    {
        hp -= damage;

        Quaternion rotDirection = Quaternion.LookRotation(transform.position - hitObjectPos);
        Vector2 direction = (transform.position - hitObjectPos).normalized;

        GetKnockback(direction, knockback);

        if (hp <= 0)
            OnDeath();
    }

    public virtual void GetKnockback(Vector2 dir, float knockback, float knockbackReturnTime = 0.5f)
    {
        StopCoroutine(SetVelocityZero(dir * knockback));
        StartCoroutine(SetVelocityZero(dir * knockback));
    }

    private IEnumerator SetVelocityZero(Vector2 dir)
    {
        float progress = 0f;

        Vector3 knockbackDir = dir;

        while(progress <= 1f)
        {
            Vector3 newKnockback = knockbackDir * (1 - progress);
            transform.position += newKnockback * Time.deltaTime * 2f;
            progress += Time.deltaTime * 2f;
            yield return null;
        }

        yield return null;
    }

    protected virtual void OnDeath()
    {
        if (deathEffect != null)
            Instantiate(deathEffect, transform.position, Quaternion.identity);

        isDeath = true;
    }
}