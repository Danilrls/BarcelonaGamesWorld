using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;
public class CharacterCreation : MonoBehaviour
{

    public List<GameObject> models;
    //Model index
    public int selectionIndex;

    public Image lockedMessage;
    public List<Image> stats;
    public List<GameObject> names;

    void Start()
    {
        selectionIndex = 0;
        models = new List<GameObject>();
        foreach (Transform t in transform)
        {
            models.Add(t.gameObject);
            t.gameObject.SetActive(false);
        }
        models[selectionIndex].SetActive(true);
    }
    public void Select(int index)
    {
        if (index == selectionIndex)
        {
            return;
        }
        else if (index < 0 || index > models.Count)
        {
            StartCoroutine(ShowMessage(2));
            return;
        }
        else
        {
            models[selectionIndex].SetActive(false);
            selectionIndex = index;
            models[selectionIndex].SetActive(true);

            for (int i = 0; i < names.Count; i++)
            {
                if (i == selectionIndex)
                {
                    names[i].SetActive(true);
                }
                else
                {
                    names[i].SetActive(false);
                }
            }

            switch (selectionIndex)
            {
                case 0:
                    stats[0].fillAmount = 0.5f;
                    stats[1].fillAmount = 0.5f;
                    stats[2].fillAmount = 0.5f;
                    break;
                case 1:
                    stats[0].fillAmount = 0.75f;
                    stats[1].fillAmount = 0.75f;
                    stats[2].fillAmount = 0.25f;
                    break;
            }
        }
    }

    IEnumerator ShowMessage(float delay)
    {
        lockedMessage.enabled = true;
        yield return new WaitForSeconds(delay);
        lockedMessage.enabled = false;
    }

    public void findMatch()
    {
        DontDestroyOnLoad(GameObject.Find("mc"));

        SceneManager.LoadScene("move_goal");
        //GameObject.Find("NetworkManager").GetComponent<CustomNetworkManager>().stopIterating = false;
        foreach (GameObject model in models)
        {
            model.SetActive(false);
        }
    }

    public void BackToMain()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetMouseButton(0))
        // {
        //     models[selectionIndex].transform.Rotate(new Vector3(0.0f, Input.GetAxis("Mouse X")
        //         * -3, 0.0f), Space.Self);
        // }
    }
}
