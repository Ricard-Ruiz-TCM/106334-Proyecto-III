using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialTextWritter : MonoBehaviour {
    private static TutorialTextWritter instance;
    private List<TextWriterSingle> textWriterSingleList;
    private int writtersCounter;

    private void Awake() {
        instance = this;
        textWriterSingleList = new List<TextWriterSingle>();
        writtersCounter = -1;
    }

    public static int AddWritter_Static(TextMeshProUGUI uiTextName, TextMeshProUGUI uiTextDescription, string textOfName, string textOfDescription, float timePerCharacter) {
        return instance.AddWritter(uiTextName, uiTextDescription, textOfName, textOfDescription, timePerCharacter);
    }

    public static void setSetDestroyInstance(int writterId)
    {
        instance.SetDestroyInstance(writterId);
    }

    private void SetDestroyInstance(int writterId)
    {
        textWriterSingleList[writterId].SetDestroyInstance();
    }

    private int AddWritter(TextMeshProUGUI uiTextName, TextMeshProUGUI uiTextDescription, string textOfName, string textOfDescription, float timePerCharacter) {
        writtersCounter++;
        textWriterSingleList.Add(new TextWriterSingle(uiTextName, uiTextDescription, textOfName, textOfDescription, timePerCharacter));
        return writtersCounter;
    }

    private void Update() {
        try {
            foreach (var textWriterSingle in textWriterSingleList) {
                if (textWriterSingle != null) {
                    bool destroyInstance = textWriterSingle.Update();
                    if (destroyInstance) {
                        writtersCounter--;
                        textWriterSingleList.Remove(textWriterSingle);
                    }
                }
            }
        } catch (Exception ex) { }
    }

    public class TextWriterSingle {
        private TextMeshProUGUI uiTextName;
        private TextMeshProUGUI uiTextDescription;
        private string textOfName;
        private string textOfDescription;
        private int characterIndex;
        private float timePerCharacter;
        private float timer;
        private bool destroyInstance;

        public TextWriterSingle(TextMeshProUGUI uiTextName, TextMeshProUGUI uiTextDescription, string textOfName, string textOfDescription, float timePerCharacter) {
            this.uiTextName = uiTextName;
            this.uiTextDescription = uiTextDescription;
            this.textOfName = textOfName;
            this.textOfDescription = textOfDescription;
            this.timePerCharacter = timePerCharacter;
        }

        public bool Update() {
            uiTextName.text = textOfName;
            timer -= Time.deltaTime;
            if (timer <= 0f) {
                FMODManager.instance.PlayWritingMusic();
                timer += timePerCharacter;
                characterIndex++;
                uiTextDescription.text = textOfDescription.Substring(0, characterIndex);

                if (characterIndex >= textOfDescription.Length) {
                    return true;
                }
                else if(destroyInstance)
                {
                    return true;
                }
            }
            return false;
        }

        public void SetDestroyInstance()
        {
            destroyInstance = true;
        }
    }
}
