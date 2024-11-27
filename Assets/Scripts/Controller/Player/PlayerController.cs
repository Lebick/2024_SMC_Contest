using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class PlayerController : Controller
{
    private enum ParryingState
    {
        Bad, //실패
        Good, //+-64ms
        Perfect //+-32ms
    }

    //private CircleCollider2D circleCollider;

    public PlayerHUD playerHUD;

    public float moveSpeed;
    private Vector3 moveDir;
    private Vector3 lastestDir = Vector3.right;

    private float walkSFXTimer;
    private float walkSFXStep = 0.25f;
    public AudioClip[] walkClip;

    private bool isCanMovement;
    private bool isInvincibility;

    public float dodgeCD = 0.6f;
    private float dodgeTimer;
    private bool isDodging;

    private bool isParrying;
    private ParryingState parryingState;
    private bool isHalfDamage;
    public AudioClip parryingClip;

    //public ParticleSystem walkDusk;
    //private ParticleSystem.MainModule walkDuskEmission;

    protected override void Awake()
    {
        base.Awake();
        //circleCollider = GetComponent<CircleCollider2D>();
        //walkDuskEmission = walkDusk.main;
    }

    private void Start()
    {
        InputValueManager.instance.dodgeAction.AddListener(() => DodgeAction());
        InputValueManager.instance.parryingAction.AddListener(() => ParryingAction());
    }


    #region Update문
    protected override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SceneLoadManager.instance.ChangeScene("WorldMap");
        }

        if (GameManager.instance.isPause) return;

        base.Update();

        UpdateState();

        UpdateInputValue();

        UpdateSkillCoolDown();
    }

    private void UpdateState()
    {
        isInvincibility = isDodging;
        isCanMovement = !isDodging /*&& 기타조건 */;
    }

    private void UpdateInputValue()
    {
        moveDir = InputValueManager.instance.moveDir;
        lastestDir = moveDir != Vector3.zero ? moveDir : lastestDir;
    }

    private void UpdateSkillCoolDown()
    {
        if(dodgeTimer > 0)
            dodgeTimer -= Time.deltaTime;
    }

    #endregion

    #region FixedUpdate문
    private void FixedUpdate()
    {
        if(isCanMovement)
            UpdateMove();
    }

    private void UpdateMove()
    {
        moveDir = InputValueManager.instance.moveDir;

        rigidBody.position = transform.position + moveDir.normalized * moveSpeed * 0.02f;

        bool isWalk = moveDir != Vector3.zero;

        walkSFXTimer += Time.deltaTime;

        if (!isWalk)
            walkSFXTimer = 0;

        if (isWalk && walkSFXTimer >= walkSFXStep)
        {
            walkSFXTimer -= walkSFXStep;
            SoundManager.instance.PlaySFX(walkClip[UnityEngine.Random.Range(0, walkClip.Length)]);
        }
    }

    #endregion

    #region LateUpdate문
    private void LateUpdate()
    {
        if(playerHUD != null)
            playerHUD.UpdateHUD(this);
    }

    #endregion

    private void DodgeAction()
    {
        if(dodgeTimer <= 0)
        {
            dodgeTimer = dodgeCD;

            isDodging = true;
            StartCoroutine(DodgeState());
        }
    }

    private IEnumerator DodgeState()
    {
        Vector2 dodgeDir = lastestDir;
        float timer = 0f;

        while(timer <= 0.2f)
        {
            timer += Time.deltaTime;
            rigidBody.position += dodgeDir * Time.deltaTime * 10f;
            yield return null;
        }

        isDodging = false;
    }

    private void ParryingAction()
    {

        if (isParrying) return;

        isParrying = true;
        StartCoroutine(CheckParryingTime(64));
    }

    IEnumerator CheckParryingTime(int milliseconds)
    {
        int count = Enum.GetValues(typeof(ParryingState)).Length;

        parryingState = ParryingState.Perfect;
        for (int i=0; i<count-1; i++)
        {
            yield return new WaitForSecondsRealtime(milliseconds / (1000f * count-1));
            parryingState = (ParryingState)(int)parryingState--;
        }

        parryingState = ParryingState.Bad;
        isParrying = false;
    }

    public override void GetDamage(float damage, Vector3 hitObjectPos, float knockback)
    {
        if (isInvincibility) return;

        StartCoroutine(CheckParryingState(() =>
        {
            float value = isHalfDamage ? 0.5f : 1f;
            base.GetDamage(damage * value, hitObjectPos, knockback * value);

            isHalfDamage = false;
        }));
    }

    private IEnumerator CheckParryingState(Action damageAction)
    {
        if (parryingState != ParryingState.Bad)
        {
            ParryingResult(parryingState, damageAction);
            yield break;
        }

        yield return new WaitForSecondsRealtime(0.032f); //32ms대기

        if (isParrying)
        {
            ParryingResult(ParryingState.Perfect, damageAction);
            yield break;
        }

        yield return new WaitForSecondsRealtime(0.032f); //32ms대기

        if (isParrying)
            ParryingResult(ParryingState.Good, damageAction);
        else
            ParryingResult(ParryingState.Bad, damageAction);
    }

    private void ParryingResult(ParryingState type, Action damageAction)
    {
        switch (type)
        {
            case ParryingState.Perfect:
                SoundManager.instance.PlaySFX(parryingClip, pitch: 0.6f);
                break;

            case ParryingState.Good:
                SoundManager.instance.PlaySFX(parryingClip);
                isHalfDamage = true;
                damageAction?.Invoke();
                break;

            case ParryingState.Bad:
                print("버러지");
                damageAction?.Invoke();
                break;
        }
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