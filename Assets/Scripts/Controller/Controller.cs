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

    }

    public virtual void GetDamage(float damage, Vector3 hitObjectPos = new Vector3(), float knockback = 0)
    {
        hp -= damage;

        if (hp <= 0)
            OnDeath();
    }

    protected virtual void OnDeath()
    {
        if (deathEffect != null)
            Instantiate(deathEffect, transform.position, Quaternion.identity);

        if("a".Equals("b")) //юс╫ц
            Destroy(gameObject);
    }
}