using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class PlayerData : MonoBehaviour
{
    Animator cha_ani;
    Color halpalpha = new Color(1, 1, 1, 0.5f);
    Color fullalpha = new Color(1, 1, 1, 1);
    private SpriteRenderer main_cha;
   
    public void Start()
    {
        cha_ani = transform.GetComponent<Animator>();
        main_cha = GetComponent<SpriteRenderer>();
    }
    
    public int hp = 5;
    
    private void Update()
    {
       
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Obstruction")
        {
            
            hp -= 1;
            StartCoroutine(damaged());
            if (hp == 0)
            {


            }
        }
      
    }
    public void Game_over()
    {
        //���н� ȸ�� ���ߴ� �޼���
        //Ÿ�ϸ�,�� ������Ʈ�̵� ����

    }

    IEnumerator damaged()
    {
        yield return new WaitForSeconds(0.1f);
        main_cha.color = halpalpha;
        yield return new WaitForSeconds(0.1f);
        main_cha.color = fullalpha;
        yield return new WaitForSeconds(0.1f);
        main_cha.color = halpalpha;
        yield return new WaitForSeconds(0.1f);
        main_cha.color = fullalpha;
        yield return new WaitForSeconds(0.1f);
        main_cha.color = halpalpha;
        yield return new WaitForSeconds(0.1f);
        main_cha.color = fullalpha;
    }

  
}