using UnityEngine;


namespace JJKid.EzTimer
{
    public class HotkeyData
    {
        public const int TOTAL_HOTKEYS = 6;
        public const string KEY_1 = "Hotkey1";
        public const string KEY_2 = "Hotkey2";
        public const string KEY_3 = "Hotkey3";
        public const string KEY_4 = "Hotkey4";
        public const string KEY_5 = "Hotkey5";
        public const string KEY_6 = "Hotkey6";

        public int[] minutesOfKeys;




        public HotkeyData()
        {
            this.init();
            this.loadData();
        }

        private void init()
        {
            this.minutesOfKeys = new int[TOTAL_HOTKEYS];
        }

        private void loadData()
        {
            this.minutesOfKeys[0] = PlayerPrefs.GetInt(KEY_1, 10);
            this.minutesOfKeys[1] = PlayerPrefs.GetInt(KEY_2, 25);
            this.minutesOfKeys[2] = PlayerPrefs.GetInt(KEY_3, 3);
            this.minutesOfKeys[3] = PlayerPrefs.GetInt(KEY_4, 45);
            this.minutesOfKeys[4] = PlayerPrefs.GetInt(KEY_5, 5);
            this.minutesOfKeys[5] = PlayerPrefs.GetInt(KEY_6, 60);
        }

        public void saveData()
        {
            PlayerPrefs.SetInt(KEY_1, this.minutesOfKeys[0]);
            PlayerPrefs.SetInt(KEY_2, this.minutesOfKeys[1]);
            PlayerPrefs.SetInt(KEY_3, this.minutesOfKeys[2]);
            PlayerPrefs.SetInt(KEY_4, this.minutesOfKeys[3]);
            PlayerPrefs.SetInt(KEY_5, this.minutesOfKeys[4]);
            PlayerPrefs.SetInt(KEY_6, this.minutesOfKeys[5]);
        }
    }
}
