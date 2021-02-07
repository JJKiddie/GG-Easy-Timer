using UnityEngine;
using UnityEngine.UI;


namespace JJKid.EzTimer
{
    public class HotkeyDialog
    {
        private GameObject rootGO;
        private InputField[] inputFields;

        private HotkeyData hotkeyData;




        public HotkeyDialog(HotkeyData _hotkeyData)
        {
            this.hotkeyData = _hotkeyData;

            this.prepareUIs();
            this.hide();
        }

        private void prepareUIs()
        {
            this.inputFields = new InputField[HotkeyData.TOTAL_HOTKEYS];

            this.rootGO = GameObject.Find("Hotkey Dialog");

            Transform[] trans = this.rootGO.GetComponentsInChildren<Transform>();
            for(int i = 0; i < trans.Length; i++)
            {
                Transform tran = trans[i];

                if(tran.name == "Input 1")
                    this.inputFields[0] = tran.GetComponentInChildren<InputField>();
                else if(tran.name == "Input 2")
                    this.inputFields[1] = tran.GetComponentInChildren<InputField>();
                else if(tran.name == "Input 3")
                    this.inputFields[2] = tran.GetComponentInChildren<InputField>();
                else if(tran.name == "Input 4")
                    this.inputFields[3] = tran.GetComponentInChildren<InputField>();
                else if(tran.name == "Input 5")
                    this.inputFields[4] = tran.GetComponentInChildren<InputField>();
                else if(tran.name == "Input 6")
                    this.inputFields[5] = tran.GetComponentInChildren<InputField>();
            }
        }

        public void update()
        {
            if(this.NowActive)
            {
                if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
                {
                    this.setData();
                    this.hide();
                }

                if(Input.GetKeyDown(KeyCode.Escape))
                    this.hide();
            }
            else
            {
                if(Input.GetKeyDown(KeyCode.H))
                    this.show();
            }
        }

        private void setData()
        {
            for(int i = 0; i < this.inputFields.Length; i++)
                this.hotkeyData.minutesOfKeys[i] = int.Parse(this.inputFields[i].text);
            this.hotkeyData.saveData();
        }

        private void show()
        {
            for(int i = 0; i < this.inputFields.Length; i++)
                this.inputFields[i].text = this.hotkeyData.minutesOfKeys[i].ToString();

            this.rootGO.SetActive(true);
            this.inputFields[0].ActivateInputField();
            this.inputFields[0].Select();
        }

        private void hide()
        {
            this.rootGO.SetActive(false);
        }

        private bool NowActive
        {
            get { return this.rootGO.activeSelf; }
        }

        public void dataChanged()
        {
            if(this.NowActive)
            {
                for(int i = 0; i < this.inputFields.Length; i++)
                {
                    int outInt;
                    bool result = int.TryParse(this.inputFields[i].text, out outInt);

                    if(result == false)
                        this.inputFields[i].text = "1";
                }
            }
        }
    }
}
