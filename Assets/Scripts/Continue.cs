using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Continue : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(onClick);

    }

    // Update is called once per frame
    void onClick()
    {
        SceneManager.LoadScene("Main");
    }
}
