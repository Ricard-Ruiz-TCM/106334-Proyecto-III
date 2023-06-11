using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialTextWritter : MonoBehaviour
{
    private static TutorialTextWritter instance;
    private List<TextWriterSingle> textWriterSingleList;

    private void Awake()
    {
        instance = this;
        textWriterSingleList = new List<TextWriterSingle>();
    }

    public static void AddWritter_Static(TextMeshProUGUI uiTextName, TextMeshProUGUI uiTextDescription, string textOfName, string textOfDescription, float timePerCharacter)
    {
        instance.AddWritter(uiTextName, uiTextDescription, textOfName, textOfDescription, timePerCharacter);
    }
    private void AddWritter(TextMeshProUGUI uiTextName, TextMeshProUGUI uiTextDescription, string textOfName,string textOfDescription, float timePerCharacter)
    {
        textWriterSingleList.Add(new TextWriterSingle(uiTextName, uiTextDescription, textOfName, textOfDescription, timePerCharacter));
    }

    private void Update()
    {
        try
        {
            foreach (var textWriterSingle in textWriterSingleList)
            {
                if (textWriterSingle != null)
                {
                    bool destroyInstance = textWriterSingle.Update();
                    if (destroyInstance)
                    {
                        textWriterSingleList.Remove(textWriterSingle);
                    }
                }
            }
        } catch(Exception ex) { }        
    }

    public class TextWriterSingle
    {
        private TextMeshProUGUI uiTextName;
        private TextMeshProUGUI uiTextDescription;
        private string textOfName;
        private string textOfDescription;
        private int characterIndex;
        private float timePerCharacter;
        private float timer;

        public TextWriterSingle(TextMeshProUGUI uiTextName, TextMeshProUGUI uiTextDescription, string textOfName, string textOfDescription, float timePerCharacter)
        {
            this.uiTextName = uiTextName;
            this.uiTextDescription = uiTextDescription;
            this.textOfName = textOfName;
            this.textOfDescription = textOfDescription;
            this.timePerCharacter = timePerCharacter;
        }

        public bool Update()
        {
            uiTextName.text = textOfName;
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                timer += timePerCharacter;
                characterIndex++;
                uiTextDescription.text = textOfDescription.Substring(0, characterIndex);

                if (characterIndex >= textOfDescription.Length)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
