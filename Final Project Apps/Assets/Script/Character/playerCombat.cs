using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class playerCombat : MonoBehaviour
{
    //identitiy
    public string charName;
    public int player;

    //animator
    public Animator animator;

    //playerControl data
    public GameObject getPlayerControl;
    private playerControl playerControl;

    //health
    public int maxHealth = 100;
    public int currentHealth;

    public healthBar healthBar;

    //enemy layer
    public LayerMask enemyLayer;

    //light attack
    public Transform lightAttackPoint;
    public float lightAttackRange = 0.5f;
    public int lightAttackDamage = 3;
    public float lightAttackRate = 2f;
    float nextLightAttackTime = 0f;
    public float lightAttackDelay = .6f;
    public bool lightAttack = false;

    //heavy attack
    public Transform heavyAttackPoint;
    public float heavyAttackRange = 1f;
    public int heavyAttackDamage = 6;
    public float heavyAttackRate = 1f;
    float nextHeavyAttackTime = 0f;
    public float heavyAttackDelay = .6f;
    public bool heavyAttack = false;

    //jump light attack
    public Transform jumpLightAttackPoint;
    public float jumpLightAttackRange = 0.5f;
    public int jumpLightAttackDamage = 3;
    public float jumpLightAttackRate = 2f;
    float nextJumpLightAttackTime = 0f;
    public bool jumpLightAttack = false;

    //jump heavy attack
    public Transform jumpHeavyAttackPoint;
    public float jumpHeavyAttackRange = 1f;
    public int jumpHeavyAttackDamage = 6;
    public float jumpHeavyAttackRate = 1f;
    float nextJumpHeavyAttackTime = 0f;
    public bool jumpHeavyAttack = false;

    //crouch light attack
    public Transform crouchLightAttackPoint;
    public float crouchLightAttackRange = 0.5f;
    public int crouchLightAttackDamage = 3;
    public float crouchLightAttackRate = 2f;
    float nextCrouchLightAttackTime = 0f;
    public float crouchLightAttackDelay = .6f;
    public bool crouchLightAttack = false;

    //crouch heavy attack
    public Transform crouchHeavyAttackPoint;
    public float crouchHeavyAttackRange = 1f;
    public int crouchHeavyAttackDamage = 6;
    public float crouchHeavyAttackRate = 1f;
    float nextCrouchHeavyAttackTime = 0f;
    public float crouchHeavyAttackDelay = .6f;
    public bool crouchHeavyAttack = false;

    //cover
    //stand cover
    public bool cover = false;
    Vector2 coverPress;
    [SerializeField] private Collider2D standCol;
    [SerializeField] private Collider2D crouchCol;

    //crouch cover

    private void Start()
    {
        playerControl = getPlayerControl.GetComponent<playerControl>();
        currentHealth = maxHealth;

        healthBar.setMaxHealth(maxHealth);
    }

    private void Update()
    {
        //health
        animator.SetInteger("health", currentHealth);

        //light attack reset time
        if (Time.time >= nextLightAttackTime)
        {
            lightAttack = false;
        }

        //heavy attack reset time
        if(Time.time >= nextHeavyAttackTime)
        {
            heavyAttack = false;
        }

        //jump light attack reset time
        if(Time.time >= nextJumpLightAttackTime)
        {
            jumpLightAttack = false;
        }

        //jump heavy attack reset time
        if(Time.time >= nextJumpHeavyAttackTime)
        {
            jumpHeavyAttack = false;
        }
        
        //crouch light attack reset time
        if(Time.time >= nextCrouchLightAttackTime)
        {
            crouchLightAttack = false;
        }

        //crouch heavy attack reset time
        if(Time.time >= nextCrouchHeavyAttackTime)
        {
            crouchHeavyAttack = false;
        }

        //cover
        if (coverPress.y == 1)
        {
            if (!playerControl.crouch && !animator.GetBool("jump"))
            {
                cover = true;
                Debug.Log("stand cover");
                animator.SetBool("cover", true);
                standCol.enabled = false;
                crouchCol.enabled = true;
            } else if (playerControl.crouch && !animator.GetBool("jump"))
            {
                cover = true;
                Debug.Log("crouch cover");
                animator.SetBool("cover", true);
                standCol.enabled = false;
                crouchCol.enabled = false;
            }
        } else
        {
            cover = false;
            standCol.enabled = true;
            crouchCol.enabled = true;
            animator.SetBool("cover", cover);
        }
    }

    //menerima damage
    public void takeDamage(int damage)
    {
        currentHealth -= damage;
        animator.SetTrigger("hurt");
        hit();
        if (charName == "bode")
        {
            budiHit();
        }else if(charName == "miftah")
        {
            miftahHit();
        }else if(charName == "theo")
        {
            theoHit();
        }
        healthBar.setHealth(currentHealth);

        //animasi menerima damage;
        if(currentHealth <= 0)
        {
            die();
        }

    }

    //mati
    void die()
    {
        Debug.Log("Died");
        //animasi mati
        animator.SetBool("dead", true);

        this.enabled = false;
    }

    //membaca area serangan
    private void OnDrawGizmosSelected()
    {
        //light attack
        if (lightAttackPoint == null)
            return;
        Gizmos.DrawWireSphere(lightAttackPoint.position, lightAttackRange);

        //heavy attack
        if (heavyAttackPoint == null)
            return;
        Gizmos.DrawWireSphere(heavyAttackPoint.position, heavyAttackRange);

        //jump light attack
        if(jumpLightAttackPoint == null)
            return;
        Gizmos.DrawWireSphere(jumpLightAttackPoint.position, jumpLightAttackRange);

        //jump heavy attack 
        if (jumpHeavyAttackPoint == null)
            return;
        Gizmos.DrawWireSphere(jumpHeavyAttackPoint.position, jumpHeavyAttackRange);

        //crouch light attack
        if (crouchLightAttackPoint == null)
            return;
        Gizmos.DrawWireSphere(crouchLightAttackPoint.position, crouchLightAttackRange);

        //crouch heavy attack
        if (crouchHeavyAttackPoint == null)
            return;
        Gizmos.DrawWireSphere(crouchHeavyAttackPoint.position, crouchHeavyAttackRange);
    }

    IEnumerator doDamage (Collider2D enemy, float delayTime, int damage)
    {
        yield return new WaitForSeconds(delayTime);
        enemy.GetComponent<playerCombat>().takeDamage(damage);
    }

    //light attack
    private void OnLightAttack()
    {
        if (Time.timeScale != 0f && !playerControl.gameFinish && playerControl.gameStart)
        {
            if(!playerControl.crouch && !animator.GetBool("jump"))            //light attack
            {
                if (!lightAttack)
                {
                    //animasi attack
                    animator.SetTrigger("lightAttack");
                    if (charName == "bode")
                    {
                        budiLightAttack();
                    }
                    else if (charName == "miftah")
                    {
                        miftahLightAttack();
                    }
                    else if (charName == "theo")
                    {
                        theoLightAttack();
                    }
                    //mendeteksi musuh di attack range
                    Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(lightAttackPoint.position, lightAttackRange, enemyLayer);

                    //memberi damage
                    foreach (Collider2D enemy in hitEnemies)
                    {
                        Debug.Log("hit light attack " + enemy.name);
                        StartCoroutine(doDamage(enemy, lightAttackDelay, lightAttackDamage));
                    }
                    nextLightAttackTime = Time.time + 1f / lightAttackRate;
                    lightAttack = true;
                }
            } else if (!playerControl.crouch && animator.GetBool("jump"))     //jump light attack
            {
                if (!jumpLightAttack)
                {
                    animator.SetTrigger("lightAttack");

                    Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(jumpLightAttackPoint.position, jumpLightAttackRange, enemyLayer);

                    foreach (Collider2D enemy in hitEnemies)
                    {
                        Debug.Log("hit jump light attack " + enemy.name);
                        enemy.GetComponent<playerCombat>().takeDamage(jumpLightAttackDamage);
                    }
                    nextJumpLightAttackTime = Time.time + 1f / jumpLightAttackRate;
                    jumpLightAttack = true;
                }
            }else if (playerControl.crouch && !animator.GetBool("jump"))      //crouch light attack
            {
                if (!crouchLightAttack)
                {
                    animator.SetTrigger("lightAttack");
                    if (charName == "bode")
                    {
                        budiLightAttack();
                    }
                    else if (charName == "miftah")
                    {
                        miftahLightAttack();
                    }
                    else if (charName == "theo")
                    {
                        theoLightAttack();
                    }
                    Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(crouchLightAttackPoint.position, crouchLightAttackRange, enemyLayer);

                    foreach (Collider2D enemy in hitEnemies)
                    {
                        Debug.Log("hit crouch light attack " + enemy.name);
                        StartCoroutine(doDamage(enemy, crouchLightAttackDelay, crouchLightAttackDamage));
                        
                    }
                    nextCrouchLightAttackTime = Time.time + 1f / crouchLightAttackRate;
                    crouchLightAttack = true;
                }
            }
        }
    }

    //heavy attack
    private void OnHeavyAttack()
    {
        if (Time.timeScale != 0f && !playerControl.gameFinish && playerControl.gameStart)
        {
            if(!playerControl.crouch && !animator.GetBool("jump"))        //heavy attack
            {
                if(!heavyAttack)
                {
                    animator.SetTrigger("heavyAttack");
                    if (charName == "bode")
                    {
                        budiHeavyAttack();
                    }
                    else if (charName == "miftah")
                    {
                        miftahHeavyAttack();
                    }
                    else if (charName == "theo")
                    {
                        theoHeavyAttack();
                    }
                    Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(heavyAttackPoint.position, heavyAttackRange, enemyLayer);

                    foreach (Collider2D enemy in hitEnemies)
                    {
                        Debug.Log("hit heavy attack " + enemy.name);
                        StartCoroutine(doDamage(enemy, heavyAttackDelay, heavyAttackDamage));
                    }
                    nextHeavyAttackTime = Time.time + 1f / heavyAttackRate;
                    heavyAttack = true;
                }
            }else if(!playerControl.crouch && animator.GetBool("jump"))   //jump heavy attadck
            {
                if (!jumpHeavyAttack)
                {
                    animator.SetTrigger("heavyAttack");

                    Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(jumpHeavyAttackPoint.position, jumpHeavyAttackRange, enemyLayer);

                    foreach (Collider2D enemy in hitEnemies)
                    {
                        Debug.Log("hit jump heavy attack " + enemy.name);
                        enemy.GetComponent<playerCombat>().takeDamage(jumpHeavyAttackDamage);
                    }
                    nextJumpHeavyAttackTime = Time.time + 1f / jumpHeavyAttackRate;
                    jumpHeavyAttack = true;
                }
            }else if(playerControl.crouch && !animator.GetBool("jump"))   //crouch heavy attack
            {
                if (!crouchHeavyAttack)
                {
                    animator.SetTrigger("heavyAttack");
                    if (charName == "bode")
                    {
                        budiHeavyAttack();
                    }
                    else if (charName == "miftah")
                    {
                        miftahHeavyAttack();
                    }
                    else if (charName == "theo")
                    {
                        theoHeavyAttack();
                    }
                    Debug.Log("crouch heavy attack");

                    Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(crouchHeavyAttackPoint.position, crouchHeavyAttackRange, enemyLayer);

                    foreach (Collider2D enemy in hitEnemies)
                    {
                        Debug.Log("hit crouch heavy attack " + enemy.name);
                        StartCoroutine(doDamage(enemy, crouchHeavyAttackDelay, crouchHeavyAttackDamage));
                    }
                    nextCrouchHeavyAttackTime = Time.time + 1f / crouchHeavyAttackRate;
                    crouchHeavyAttack = true;
                }
            }
        }
    }

    //cover
    private void OnCover(InputValue value)
    {
        coverPress = value.Get<Vector2>();
        if (charName == "miftah")
        {
            miftahCover();
        }
        else if (charName == "theo")
        {
            theoCover();
        }
    }

    void budiHit()
    {
        FindObjectOfType<audioManager>().play("budiHit");
    }
    void budiHeavyAttack()
    {
        FindObjectOfType<audioManager>().play("budiHeavyAttack");
    }

    void budiLightAttack()
    {
        FindObjectOfType<audioManager>().play("budiLightAttack");
    }

    void miftahHeavyAttack()
    {
        FindObjectOfType<audioManager>().play("miftahHeavyAttack");
    }

    void miftahLightAttack()
    {
        FindObjectOfType<audioManager>().play("miftahLightAttack");
    }

    void miftahCover()
    {
        FindObjectOfType<audioManager>().play("miftahCover");
    }

    void miftahHit()
    {
        FindObjectOfType<audioManager>().play("miftahHit");
    }

    void theoCover()
    {
        FindObjectOfType<audioManager>().play("theoCover");
    }

    void theoHeavyAttack()
    {
        FindObjectOfType<audioManager>().play("theoHeavyAttack");
    }

    void theoHit()
    {
        FindObjectOfType<audioManager>().play("theoHit");
    }

    void theoLightAttack()
    {
        FindObjectOfType<audioManager>().play("theoLightAttack");
    }

    void hit()
    {
        FindObjectOfType<audioManager>().play("hit");
    }
}
