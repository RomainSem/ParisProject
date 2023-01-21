using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MoveObjectEnter : MonoBehaviour
{
    public float reachRange = 1.8f;

    private Animator _anim;
    private GameObject _player;


    private void Awake()
    {
        
    }

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _anim = GetComponent<Animator>();
        _anim.enabled = false;  //disable animation states by default.  
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == _player)
        {
            _anim.enabled = true;
            _anim.SetBool("isOpen_Obj_1", false);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == _player)        //player has exited trigger
        {
            _anim.SetBool("isOpen_Obj_1", true);

        }
    }


    #region GUI Config

    //configure the style of the GUI
    //private void setupGui()
    //{
    //    guiStyle = new GUIStyle();
    //    guiStyle.fontSize = 16;
    //    guiStyle.fontStyle = FontStyle.Bold;
    //    guiStyle.normal.textColor = Color.white;
    //    msg = "Press E/Fire1 to Open";
    //}

    //private string getGuiMsg(bool isOpen)
    //{
    //    string rtnVal;
    //    if (isOpen)
    //    {
    //        rtnVal = "Press E/Fire1 to Close";
    //    }
    //    else
    //    {
    //        rtnVal = "Press E/Fire1 to Open";
    //    }

    //    return rtnVal;
    //}

    //void OnGUI()
    //{
    //    if (showInteractMsg)  //show on-screen prompts to user for guide.
    //    {
    //        GUI.Label(new Rect(50, Screen.height - 50, 200, 50), msg, guiStyle);
    //    }
    //}
    //End of GUI Config --------------
    #endregion
}
