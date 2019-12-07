using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gamemanager : MonoBehaviour
{
    public TextMeshProUGUI scoreText1;
    public TextMeshProUGUI scoreText2;

    public TextMeshProUGUI timer_txt;

    public GameObject resultwindow;
    public TextMeshProUGUI Result;
    public int player1Score=0;
    public int player2Score =0;
int time_cout=600;
    public Customer[] customers;
    bool isEnd = false;
    // Start is called before the first frame update
    void Start()
    {
        customers[0].gotoShoping();
        StartCoroutine(generateCustomer());
        StartCoroutine(timeRemain());
    }
    IEnumerator timeRemain()
    {
        time_cout--;
        timer_txt.text=time_cout+"";
        yield return new  WaitForSeconds(1f);
        if(time_cout<=1)
        {
            resultwindow.SetActive(true);
            if(player1Score>player2Score)
            {
                Result.text="Player1 win";
            }
            else if(player1Score<player2Score)
            {
                Result.text="Player2 win";
            }
            else
            {
                Result.text="Draw";
            }
        }
        else
        {
            StartCoroutine(timeRemain());
        }
    }
    void Update()
    {
        scoreText1.text="P1 "+player1Score;
        scoreText2.text="P2 "+player2Score;
    }
    IEnumerator generateCustomer()
    {
        yield return  new  WaitForSeconds(1f);
        for(int i=0;i<customers.Length;i++)
        {
            if(customers[i].isAvailable())
            {
                customers[i].gotoShoping();
                break;
            }
        }
        yield return  new  WaitForSeconds(18f);
        if(!isEnd)
            StartCoroutine(generateCustomer());
    }

}
