/*
 *FileName:      CharacterControl.cs
 *Author:        MC
 *Date:          2021/04/24 12:44:16
 *UnityVersion:  2020.3.0f1c1
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterControl : Singleton<CharacterControl>
{
    public float HorizontalSpeed;
    public float VerticalSpeed;
    private Vector2 moveDirection = Vector2.zero;

    private Rigidbody2D rb;
    [SerializeField]
    private bool isfast = false;
    //Ani
    private Animator ani;
    private SpriteRenderer spriteRenderer;
    private float _a = 1;
    //o2
    public Image O2Image;
    public float currentO2=100;
    public float MaxO2=100;
    //Health
    public GameObject[] HealthImages;
    public int CurrentHealth = 3;
    public int MaxHealth = 3;
    public bool isdie = false;
    //Injured
    private bool isInjured = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {        
        if (!isfast)
        {
            GameManager.Instance.GameSpeed = 1f;
            GameManager.Instance.colddown = 2f;
        }
        else
        {
            GameManager.Instance.GameSpeed = 2f;
            GameManager.Instance.colddown = 1f;
        }
        if (isdie==false)
        {
            moveDirection.x = Input.GetAxisRaw("Horizontal");
            moveDirection.y = Input.GetAxisRaw("Vertical");
            //move ani
            Control();
            //O2
            ExpendO2();
        }
        //Health
        healthUI();
        //Player Alpha
        spriteRenderer.color = new Color(1, 1, 1, _a);
        //Injured
        if (isInjured==true)
        {
            time -= Time.deltaTime;
            if (time<0)
            {
                isInjured = false;
                time = 1.1f;
            }
        }
    }

    #region Control
    private void FixedUpdate()
    {
        rb.MovePosition(new Vector2(rb.position.x + moveDirection.x * HorizontalSpeed * Time.fixedDeltaTime, rb.position.y + moveDirection.y * VerticalSpeed * Time.fixedDeltaTime));
    }
    private void Control()
    {
        if (Input.GetAxisRaw("Vertical") > 0) //Up
        {
            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                ani.Play("Right");
                ani.Update(0);
            }
            else if (Input.GetAxisRaw("Horizontal") < 0)
            {
                ani.Play("Left");
                ani.Update(0);
            }
            else
            {
                ani.Play("Up");
                ani.Update(0);
            }
        }
        else if (Input.GetAxisRaw("Vertical") < 0) //Down
        {
            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                ani.Play("Right");
                ani.Update(0);
            }
            else if (Input.GetAxisRaw("Horizontal") < 0)
            {
                ani.Play("Left");
                ani.Update(0);
            }
            else
            {
                ani.Play("Down");
                ani.Update(0);
            }
        }
        else if (Input.GetAxisRaw("Horizontal") > 0) //right
        {
            if (Input.GetAxisRaw("Vertical") < 0) //Down
            {
                ani.Play("Right");
                ani.Update(0);
            }
            else if (Input.GetAxisRaw("Vertical") > 0) //Up
            {
                ani.Play("Right");
                ani.Update(0);
            }
            else
            {
                ani.Play("Right");
                ani.Update(0);
            }
        }
        else if (Input.GetAxisRaw("Horizontal") < 0) //left
        {
            if (Input.GetAxisRaw("Vertical") < 0) //Down
            {
                ani.Play("Left");
                ani.Update(0);
            }
            else if (Input.GetAxisRaw("Vertical") > 0) //Up
            {
                ani.Play("Left");
                ani.Update(0);
            }
            else
            {
                ani.Play("Left");
                ani.Update(0);
            }
        }
    }
    #endregion

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("fast"))
        {
            isfast = true;            
        }
        else if (other.CompareTag("grass"))
        {
            HorizontalSpeed = 2.5f;
            VerticalSpeed = 2.5f;
        }
        else if (other.CompareTag("O2"))
        {
            if (!isdie)
            {
                if (currentO2 < MaxO2)
                {
                    currentO2 += 30;
                    if (currentO2 > 100)
                    {
                        currentO2 = 100;
                    }
                    O2UI();
                    //TODO Audio
                    AudioManager.Instance.AudioInstantiate("O2");
                }

                Destroy(other.gameObject);
            }            
        }
        else if (other.CompareTag("health"))
        {
            if (!isdie)
            {
                if (CurrentHealth < MaxHealth)
                {
                    CurrentHealth += 1;
                    if (CurrentHealth > 3)
                    {
                        CurrentHealth = 3;
                    }
                    //TODO Audio
                    AudioManager.Instance.AudioInstantiate("Health");
                }
                Destroy(other.gameObject);
            }       
        }
        else if (other.CompareTag("fish"))
        {           
            if (CurrentHealth>0)
            {
                if (!isInjured)
                {
                    GameManager.Instance.ShowBlood();
                    AudioManager.Instance.AudioInstantiate("Fish");
                    CurrentHealth -= 1;
                    StartCoroutine(Injured());
                    //Die
                    if (CurrentHealth <= 0)
                    {
                        Dead();
                    }
                    else
                    {
                        isInjured = true;
                    }                    
                }                                            
            }
        }
    }
    float time = 1.1f;
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("fast"))
        {
            isfast = true;
        }
        else if (other.CompareTag("grass"))
        {
            HorizontalSpeed = 2.5f;
            VerticalSpeed = 2.5f;
        }
        //else if (other.CompareTag("fish"))
        //{
        //    time -= Time.deltaTime;
        //    if (time<0)
        //    {
        //        time = 2;
        //        GameManager.Instance.ShowBlood();
        //        CurrentHealth -= 1;
        //        //TODO  ‹…Àani
        //        StartCoroutine(Injured());
        //        //TODO Audio
        //        AudioManager.Instance.AudioInstantiate("Fish");
        //        //Die
        //        if (CurrentHealth <= 0)
        //        {
        //            //Die                    
        //            Dead();
        //        }
        //    }

        //}
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("fast"))
        {
            isfast = false;
        }
        else if (other.CompareTag("grass"))
        {
            HorizontalSpeed = 5f;
            VerticalSpeed = 5f;
        }
    }

    private void O2UI()
    {
        float _o2 = (float)currentO2 / MaxO2;
        if (_o2 >= 1)
        {
            O2Image.fillAmount = 1;
        }
        else
        {
            O2Image.fillAmount = _o2;
        }       
    }
    //œ˚∫ƒO2
    private void ExpendO2()
    {
        if (currentO2 > 0)
        {
            GameManager.Instance.HideBloodAni();
            currentO2 -= Time.deltaTime*3f;
            float _o2 = (float)currentO2 / MaxO2;
            O2Image.fillAmount = _o2;
        }
        else if (currentO2<=0)
        {
            currentO2 = 0;
            GameManager.Instance.ShowBloodAni();
        }
        
    }
    private void healthUI()
    {
        for (int i = 0; i < HealthImages.Length; i++)
        {
            if (CurrentHealth<MaxHealth)
            {
                if (i< HealthImages.Length - CurrentHealth)
                {
                    HealthImages[i].SetActive(false);
                }
                else
                {
                    HealthImages[i].SetActive(true);
                }
            }
            if (CurrentHealth==MaxHealth)
            {
                HealthImages[i].SetActive(true);
            }
        }
    }
    //die
    private void Dead()
    {
        Debug.Log("Die");
        isdie = true;
        TimeManager.Instance.isTimeouts = true;
        GetComponent<Collider2D>().enabled = false;
        HorizontalSpeed = 0;
        VerticalSpeed = 0;
    }

    // ‹…ÀAni
    public IEnumerator FadeOut(float time)
    {
        while (_a < 1)
        {
            _a += Time.deltaTime / time;
            yield return null;
        }
    }

    public IEnumerator FadeIn(float time)
    {
        while (_a > 0.3f)
        {
            _a -= Time.deltaTime / time;
            yield return null;
        }
    }
    IEnumerator Injured()
    {
        yield return StartCoroutine(FadeIn(0.5f));
        yield return StartCoroutine(FadeOut(0.5f));
        yield break;
    }
}
