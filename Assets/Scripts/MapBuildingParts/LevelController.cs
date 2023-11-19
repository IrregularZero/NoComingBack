using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    private bool initialized = false;

    [SerializeField]
    private int chaptersNumber;
    [SerializeField]
    private GameObject chapter0;
    [SerializeField]
    private GameObject chapter1;
    [SerializeField]
    private GameObject chapter2;
    [SerializeField]
    private GameObject chapter3;
    [SerializeField]
    private GameObject chapter4;

    [SerializeField]
    private int maxAmountOfChapters = 3;
    private List<GameObject> chapters;

    private List<GameObject> activeChapters;

    private void Start()
    {
        #region Layouts_definition
        chapters = new List<GameObject>();

        for (int i = 0; i < chaptersNumber; i++)
        {
            switch (i)
            {
                case 0: chapters.Add(chapter0); break;
                case 1: chapters.Add(chapter1); break;
                case 2: chapters.Add(chapter2); break;
                case 3: chapters.Add(chapter3); break;
                case 4: chapters.Add(chapter4); break;
                default: Debug.Log($"Enemy variants number exceeds maximum by {i - i + 1}"); break;
            }
        }
        activeChapters = new List<GameObject>();
        #endregion
    }

    public void Initiate()
    {
        if (initialized)
            return;

        #region Random Number Generation
        int[] chapterIndexes = new int[maxAmountOfChapters];
        for (int i = 0; i < maxAmountOfChapters; i++)
        {
            bool generated = false;
            while (!generated)
            {
                int generatedIndex = Random.Range(0, chaptersNumber - 1);

                generated = true;
                if (i != 0)
                {
                    for (int j = 0; j < i; j++)
                    {
                        if (generatedIndex == chapterIndexes[j])
                        {
                            generated = false;
                            break;
                        }
                    }
                }

                chapterIndexes[i] = generatedIndex;
            }
        } 
        #endregion

        Transform initializationPoint;
        for (int i = 0; i < maxAmountOfChapters; i++)
        {
            if (i == 0)
                initializationPoint = transform.parent.GetChild(transform.parent.childCount - 1);
            else
                initializationPoint = activeChapters[activeChapters.Count - 1].transform.GetChild(
                    activeChapters[activeChapters.Count - 1].transform.childCount - 1);

            Debug.Log(initializationPoint);
            activeChapters.Add(Instantiate(chapters[chapterIndexes[i]], initializationPoint));
        }

        initialized = true;
    }

    public void Deactivate()
    {
        initialized = false;

        for (int i = 0; i < maxAmountOfChapters; i++)
        {
            Destroy(activeChapters[i]);
        }
    }
}
