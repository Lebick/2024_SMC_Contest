using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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

    public PlayerHUD playerHUD;

    public float moveSpeed;
    private Vector3 moveDir;
    private Vector3 lastestDir = Vector3.right;

    private float walkSFXTimer;
    private float walkSFXStep = 0.25f;
    public AudioClip[] walkClip;

    private bool isCanMovement;

    public float dodgeCD = 0.6f;
    private float dodgeTimer;
    private bool isDodging;

    private bool isParrying;
    private ParryingState parryingState;
    private bool isHalfDamage;
    public AudioClip parryingClip;

    public float armorStrength;

    public float interactionRange;
    public IInteractableObj nearestInteraction;

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
        if (Input.GetKeyDown(KeyCode.P))
            hp = 0;

        if (isDeath) return;

        if (Input.GetKeyDown(KeyCode.Z))
            hp -= 5;

        if (GameManager.instance.isPause) return;

        base.Update();

        UpdateState();

        UpdateInputValue();

        UpdateSkillCoolDown();

        UpdateInteraction();
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

    private void UpdateInteraction()
    {
        Collider2D[] nearObjs = Physics2D.OverlapCircleAll(transform.position, interactionRange);

        if (nearObjs.Length <= 0)
        {
            nearestInteraction = null;
            return;
        }

        float distance = Mathf.Infinity;
        bool isExistObj = false;

        foreach(Collider2D obj in nearObjs)
        {
            if (obj.TryGetComponent(out IInteractableObj interactableObj))
            {
                float currentDistance = Vector3.Distance(transform.position, obj.transform.position);
                if (currentDistance < distance)
                    nearestInteraction = interactableObj;

                isExistObj = true;
            }

            if (!isExistObj)
                nearestInteraction = null;
        }

    }

    #endregion

    #region FixedUpdate문
    private void FixedUpdate()
    {
        if (isDeath) return;

        if (GameManager.instance.isPause) return;

        if (isCanMovement)
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
        if (isDeath) return;

        if (GameManager.instance.isPause) return;

        if (playerHUD != null)
            playerHUD.UpdateHUD(this);
    }

    #endregion

    private void DodgeAction()
    {
        if (isDeath) return;

        if (dodgeTimer <= 0)
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
            if (GameManager.instance.isPause)
            {
                yield return null;
                continue;
            }

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

        yield return new WaitForSecondsRealtime(0.1f);
        isParrying = false;
    }

    public override void GetDamage(float damage, Vector3 hitObjectPos, float knockback, bool isCritical = false)
    {
        if (isInvincibility) return;
        if (isDeath) return;

        StartCoroutine(CheckParryingState(() =>
        {
            float value = isHalfDamage ? 0.5f : 1f;

            float finalValue = (damage * value) - armorStrength;

            if(finalValue > 0)
                base.GetDamage(damage * value, hitObjectPos, knockback * value);

            isParrying = false;
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
                print("±32ms");
                break;

            case ParryingState.Good:
                SoundManager.instance.PlaySFX(parryingClip);
                isHalfDamage = true;
                damageAction?.Invoke();
                print("±64ms");
                break;

            case ParryingState.Bad:
                damageAction?.Invoke();
                print("실패");
                break;
        }
    }

    protected override void OnDeath()
    {
        if (isDeath) return;

        isDeath = true;

        Instantiate(deathEffect, transform.position, Quaternion.identity);
        foreach(MonoBehaviour script in GetComponents<MonoBehaviour>())
        {
            if(script != this)
                script.enabled = false;
        }
        transform.Find("Sprite").GetComponent<SpriteRenderer>().enabled = false;
        playerHUD.UpdateHUD(this);
        Invoke(nameof(DeathWait), 2f);

        this.enabled = false;
    }

    private void DeathWait()
    {
        SceneLoadManager.instance.ChangeScene(SceneNames.Village);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }
}