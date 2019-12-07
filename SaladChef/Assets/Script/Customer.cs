using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Customer : MonoBehaviour {

    public Vector3 stopingPos;
    public Vector3 endPos;
    bool isgoforfood = false;
    bool isgoOut = false;
    public bool iswaiting = false;
    public TextMeshPro foot_txt;
    public GameObject indicater;
    // Start is called before the first frame update
    void Start () {
        stopingPos = gameObject.transform.position;
        gameObject.transform.position = new Vector3 (12, stopingPos.y, stopingPos.z);
        foot_txt.text="";
        endPos=new Vector3(-15,stopingPos.y,stopingPos.z);
        indicater.SetActive(false);
        //gotoShoping();
    }
string[] vegName = new string[]{"a","b","c","d","e","f"};
    // Update is called once per frame
    void Update () {
        if(isgoforfood)
        {
            transform.position = Vector3.MoveTowards(transform.position, stopingPos, 2*Time.deltaTime);
            if (Vector3.Distance(transform.position, stopingPos) < 0.001f)
            {
                isgoforfood=false;
                iswaiting=true;
                foot_txt.text=vegName[Random.Range(0,vegName.Length)]+vegName[Random.Range(0,vegName.Length)]+vegName[Random.Range(0,vegName.Length)];
                StartCoroutine(playerStartwait());
            }
        }
        if(isgoOut)
        {
             transform.position = Vector3.MoveTowards(transform.position, endPos, 2*Time.deltaTime);
            if (Vector3.Distance(transform.position, endPos) < 0.001f)
            {
                isgoforfood=false;
                iswaiting=false;
                isgoOut=false;
                gameObject.transform.position = new Vector3 (12, stopingPos.y, stopingPos.z);
            }
        }

    }
    public void gotoShoping()
    {
        isgoforfood=true;
    }

    public bool isAvailable()
    {
        if(!isgoforfood && !iswaiting && !isgoOut)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void gooutside()
    {
         foot_txt.text="";
        indicater.SetActive(false);
        isgoOut=true;
        iswaiting=false;
    }

    IEnumerator playerStartwait()
    {
        indicater.SetActive(true);
        for(int i=40;i>0;i--)
        {
            indicater.transform.localScale=new Vector3((float) i/40,0.14f,1);
            yield return new  WaitForSeconds(1f);
            if(!iswaiting)
            break;
            
        }
        indicater.SetActive(false);
        gooutside();
    }
}