using UnityEngine;
using UnityEngine.UI;


namespace JJKid.EzTimer
{
    public class AlarmTimeDialog
    {
        public delegate void dele_newAlarm();
        public dele_newAlarm cbf_newAlarm = null;

        private GameObject rootGO;
        private InputField inputField;




        public AlarmTimeDialog()
        {
            this.prepareUIs();
            this.hide();
        }

        private void prepareUIs()
        {
            this.rootGO = GameObject.Find("Input Dialog");
            this.inputField = rootGO.GetComponentInChildren<InputField>();
        }

        public void update()
        {
            if(this.NowActive)
            {
                if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
                {
                    if(this.cbf_newAlarm != null)
                        this.cbf_newAlarm();
                    this.hide();
                }

                if(Input.GetKeyDown(KeyCode.Escape))
                    this.hide();
            }
            else
            {
                if(Input.GetKeyDown(KeyCode.N))
                    this.show();
            }
        }

        public void dataChanged()
        {
            if(this.NowActive)
            {
                int i;
                bool result = int.TryParse(this.inputField.text, out i);

                if(result == false)
                    this.inputField.text = "0";
            }
        }

        private void show()
        {
            this.inputField.text = "Enter minutes";
            this.rootGO.SetActive(true);
            this.inputField.ActivateInputField();
            this.inputField.Select();
        }

        private void hide()
        {
            this.inputField.text = "0";
            this.rootGO.SetActive(false);
        }

        private bool NowActive
        {
            get { return this.rootGO.activeSelf; }
        }

        public int getAlarmTime()
        {
            int i;
            int.TryParse(this.inputField.text, out i);

            return i;
        }
    }
}
