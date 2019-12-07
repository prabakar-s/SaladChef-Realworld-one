using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerAction : MonoBehaviour
{
    public bool isPlayer1 = false;
    bool isRight = false;
    bool isLeft = false;
    bool isTop = false;
    bool isBottom = false;

    public int[] player_item_Id = new int[] { -1, -1 };
    public int currentId = -1;
    public GameObject cust_obj =null;
    public string[] vegName = new string[]{"a","b","c","d","e","f"};
    public TextMeshPro foot_txt;
    bool istrash =false;

    bool ischop = false;
    int itemcount =0;

    public TextMeshPro chop_txt;
     public TextMeshPro info_txt;
    public GameObject chop_obj;
    bool isitemready = false;
    bool iscookediteminhand=false;

    public Gamemanager manager;
    void Start()
    {
        foot_txt.text="";
    }
    // Update is called once per frame
    void Update()
    {
        if (isPlayer1)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                isTop = true;
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                isLeft = true;
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                isBottom = true;
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                isRight = true;
            }

            if (Input.GetKeyUp(KeyCode.W))
            {
                isTop = false;
            }
            if (Input.GetKeyUp(KeyCode.A))
            {
                isLeft = false;
            }
            if (Input.GetKeyUp(KeyCode.S))
            {
                isBottom = false;
            }
            if (Input.GetKeyUp(KeyCode.D))
            {
                isRight = false;
            }
            if (Input.GetKeyDown(KeyCode.T))
            {
                pickItem();
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                DestroyItem();
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                startCooking();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                isTop = true;
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                isLeft = true;
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                isBottom = true;
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                isRight = true;
            }

            if (Input.GetKeyUp(KeyCode.UpArrow))
            {
                isTop = false;
            }
            if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                isLeft = false;
            }
            if (Input.GetKeyUp(KeyCode.DownArrow))
            {
                isBottom = false;
            }
            if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                isRight = false;
            }
            if (Input.GetKeyDown(KeyCode.P))
            {
                pickItem();
            }
            if (Input.GetKeyDown(KeyCode.O))
            {
                DestroyItem();
            }
            if (Input.GetKeyDown(KeyCode.M))
            {
                startCooking();
            }
        }

        if (isTop)
        {
            gameObject.transform.position += new Vector3(0, 0, 0.1f);
        }
        if (isBottom)
        {
            gameObject.transform.position += new Vector3(0, 0, -0.1f);
        }
        if (isRight)
        {
            gameObject.transform.position += new Vector3(0.1f, 0, 0);
        }
        if (isLeft)
        {
            gameObject.transform.position += new Vector3(-0.1f, 0, 0);
        }
        Vector3 pos = gameObject.transform.position;
        if (pos.x > 6.5f)
        {
            gameObject.transform.position = new Vector3(6.5f, pos.y, pos.z);
        }
        if (pos.x < -6.5f)
        {
            gameObject.transform.position = new Vector3(-6.5f, pos.y, pos.z);
        }
        if (pos.z > 2.5f)
        {
            gameObject.transform.position = new Vector3(pos.x, pos.y, 2.5f);
        }
        if (pos.z < -3.5f)
        {
            gameObject.transform.position = new Vector3(pos.x, pos.y, -3.5f);
        }

    }

    public void pickItem()
    {
        if(currentId!=-1 && itemcount<2)
        {
            foot_txt.text+=vegName[currentId-1];
            itemcount++;
        }
        if(ischop && isitemready)
        {
            if(foot_txt.text =="")
            {
                foot_txt.text=chop_txt.text;
                chop_txt.text="";
                itemcount=2;
                isitemready=false;
                iscookediteminhand=true;
            }
        }
    }
    public void DestroyItem()
    {
        if(istrash)
        {
            foot_txt.text="";
            itemcount=0;
            iscookediteminhand=true;
        }
        if(cust_obj!=null)
        {
            if(foot_txt.text==cust_obj.GetComponent<Customer>().foot_txt.text)
            {
                if(isPlayer1)
                {
                    manager.player1Score +=200;
                }
                else
                {
                    manager.player2Score +=200;
                }
            }
            else
            {
                if(isPlayer1)
                {
                    manager.player1Score -=100;
                }
                else
                {
                    manager.player2Score -=100;
                }
            }
            
            foot_txt.text="";
            itemcount=0;
            iscookediteminhand=true;
            cust_obj.GetComponent<Customer>().iswaiting=false;

        }
        if(ischop && !isitemready && !iscookediteminhand)
        {
            chop_txt.text+=foot_txt.text;
            foot_txt.text="";
            itemcount=0;
        }
    }
    public void startCooking()
    {
        StartCoroutine(cookingtime());
    }
    IEnumerator cookingtime()
    {
        
        yield return new  WaitForSeconds(3f);
        isitemready=true;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "veg")
        {
            currentId = other.gameObject.GetComponent<Vegetable>().myId;
        }
        if (other.gameObject.tag == "cust")
        {
            cust_obj=other.gameObject;
        }
        if (other.gameObject.tag == "trash")
        {
            istrash=true;
        }
        if(other.gameObject==chop_obj)
        {
            ischop=true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "veg" && currentId == other.gameObject.GetComponent<Vegetable>().myId)
        {
            currentId = -1;
        }
        if (other.gameObject.tag == "cust" && cust_obj == other.gameObject)
        {
            cust_obj=null;
        }
        if (other.gameObject.tag == "trash")
        {
            istrash=false;
        }
        if(other.gameObject==chop_obj)
        {
            ischop=false;
        }
    }
}