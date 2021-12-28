using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuLeveLoad : MonoBehaviour
{
    public int levelLoad;

    public void SelectLevelLoad()
    {
        SceneManager.LoadScene(levelLoad);
    }

}
