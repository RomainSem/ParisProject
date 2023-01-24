using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueScript : MonoBehaviour
{
    #region Expose
    [SerializeField] TextMeshProUGUI _textComponent;
    [SerializeField] string[] _lines;
    [SerializeField] float _textSpeed;
    
    #endregion

    #region Unity Lyfecycle

    private void Awake()
    {
        
    }

    void Start()
    {
        _textComponent.text = string.Empty;
        StartDialogue();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_textComponent.text == _lines[_index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                _textComponent.text = _lines[_index];
            }
        }
    }


    #endregion

    #region Methods

    void StartDialogue()
    {
        _index = 0;
        StartCoroutine(TypeLine());
    }

    void NextLine()
    {
        if (_index < _lines.Length - 1)
        {
            _index++;
            _textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    //private void OnMouseDown()
    //{
    //    if (_textComponent.text == _lines[_index])
    //    {
    //        NextLine();
    //    }
    //    else
    //    {
    //        StopAllCoroutines();
    //        _textComponent.text = _lines[_index];
    //    }
    //}


    IEnumerator TypeLine()
    {
        foreach (char c in _lines[_index].ToCharArray())
        {
            _textComponent.text += c;
            yield return new WaitForSeconds(_textSpeed);
        }
    }
    #endregion

    #region Private & Protected

    private int _index;

    #endregion
}
