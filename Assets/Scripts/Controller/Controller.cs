using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Controller : MonoBehaviour
{
    public GameObject damageEffect;
    public GameObject deathEffect;
    protected SpriteRenderer spriteRenderer;
    protected Rigidbody2D rigidBody;
    protected Animator animator;

    public int maxHP;
    public int hp;

    protected virtual void Awake()
    {
        hp = maxHP;
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    protected virtual void Update()
    {
        ClampValue();
    }

    protected virtual void ClampValue()
    {
        hp = Mathf.Clamp(hp, 0, maxHP);
    }

    public virtual void GetDamage(int damage, Vector3 hitObjectPos = new Vector3(), float knockback = 0)
    {
        hp -= damage;

        if (hp <= 0)
            OnDeath();
    }

    protected virtual void OnDeath()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}