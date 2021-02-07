using UnityEngine;
using UnityEngine.UI;
using System;

using JJKid.Windows;


namespace JJKid.EzTimer
{
    public class GGEasyTimerMain : MonoBehaviour
    {
        private const int EXPECTED_FPS = 20;
        private const float ONE_SECOND = 1;
        private const float HOURS_PER_HALF_DAY = 12f;
        private const float MINUTES_PER_HOUR = 60f;
        private const float SECONDS_PER_MINUTE = 60f;
        private const float ANGLE_OFFSET = 90f;
        private const float ANGLE_FULL_CIRCLE = -360f;
        private const float MINUTE_OFFSET_ANGLE = -30f;

        public RectTransform hourHandTran;
        public RectTransform minuteHandTran;
        public RectTransform secondHandTran;
        public Image redCircle;
        public AudioSource soundPlayer;
        public Animator clockAni;
        public Text time;

        private WindowsBar windowsBar = null;
        private TransparentBackground transparentBg = null;

        private float timePassed = 0;
        private float fullAlarmTime = 0;
        private float nowAlarmTime = 0;
        private bool fullscreenMode = false;

        private HotkeyData hotkeyData;
        private AlarmTimeDialog alarmTimeDialog = null;
        private HotkeyDialog hotkeyDialog = null;




        private void Awake()
        {
            this.windowsBar = new WindowsBar();
            this.transparentBg = new TransparentBackground();

            this.hotkeyData = new HotkeyData();

            this.alarmTimeDialog = new AlarmTimeDialog();
            this.alarmTimeDialog.cbf_newAlarm += this.OnNewAlarm;

            this.hotkeyDialog = new HotkeyDialog(this.hotkeyData);

            this.clearAlarm();
        }

        private void Start()
        {
            Application.targetFrameRate = EXPECTED_FPS;

            this.redCircle.fillAmount = 0;
            this.transparentBg.run();
            this.syncTime();
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Tab))
                this.toggleFullscreen();
            if(Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
                this.setAlarm(this.hotkeyData.minutesOfKeys[0] * 60);
            if(Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
                this.setAlarm(this.hotkeyData.minutesOfKeys[1] * 60);
            if(Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
                this.setAlarm(this.hotkeyData.minutesOfKeys[2] * 60);
            if(Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4))
                this.setAlarm(this.hotkeyData.minutesOfKeys[3] * 60);
            if(Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Keypad5))
                this.setAlarm(this.hotkeyData.minutesOfKeys[4] * 60);
            if(Input.GetKeyDown(KeyCode.Alpha6) || Input.GetKeyDown(KeyCode.Keypad6))
                this.setAlarm(this.hotkeyData.minutesOfKeys[5] * 60);
            if(Input.GetKeyDown(KeyCode.C))
                this.clearAlarm();

            this.alarmTimeDialog.update();
            this.hotkeyDialog.update();

            this.timePassed += Time.deltaTime;
            if(this.timePassed >= ONE_SECOND)
            {
                this.syncTime();
                this.timePassed = 0;
            }

            if(this.fullAlarmTime > 0)
            {
                this.nowAlarmTime += Time.deltaTime;
                this.updateAlarm();
            }
        }

        private void toggleFullscreen()
        {
            this.fullscreenMode = !this.fullscreenMode;

            if(this.fullscreenMode)
                this.windowsBar.showWindowBorders(true);
            else
                this.windowsBar.showWindowBorders(false);
        }

        private void syncTime()
        {
            DateTime nowTime = DateTime.Now.AddSeconds(0.5f);

            float minuteProgress = (float)nowTime.Minute / MINUTES_PER_HOUR;
            float hourAngle = (float)nowTime.Hour % HOURS_PER_HALF_DAY / HOURS_PER_HALF_DAY * ANGLE_FULL_CIRCLE 
                              + ANGLE_OFFSET + minuteProgress * MINUTE_OFFSET_ANGLE;
            float minuteAngle = minuteProgress * ANGLE_FULL_CIRCLE + ANGLE_OFFSET;
            float secondAngle = (float)nowTime.Second / SECONDS_PER_MINUTE * ANGLE_FULL_CIRCLE + ANGLE_OFFSET;

            this.hourHandTran.rotation = Quaternion.Euler(0, 0, hourAngle);
            this.minuteHandTran.rotation = Quaternion.Euler(0, 0, minuteAngle);
            this.secondHandTran.rotation = Quaternion.Euler(0, 0, secondAngle);
        }

        private void setAlarm(float seconds)
        {
            this.fullAlarmTime = seconds;
            this.nowAlarmTime = 0;
            this.time.text = this.toMinutes(seconds).ToString() + " M";
        }

        private int toMinutes(float seconds)
        {
            return (int)(seconds / 60);
        }

        private void updateAlarm()
        {
            float progress = Mathf.Clamp01(this.nowAlarmTime / this.fullAlarmTime);

            this.redCircle.fillAmount = progress;

            if(progress == 1)
            {
                this.fireAlarm();
                this.clearAlarm();
            }
        }

        private void fireAlarm()
        {
            this.clockAni.SetTrigger("Alarm");
            this.soundPlayer.Play();
        }

        private void clearAlarm()
        {
            this.redCircle.fillAmount = 0;
            this.nowAlarmTime = 0;
            this.fullAlarmTime = 0;
            this.time.text = "";
        }


        //======================================================================
        //  Alarm dialog
        //======================================================================
        public void cbf_alarmTimeInputValueChanged()
        {
            if(this.alarmTimeDialog != null)
                this.alarmTimeDialog.dataChanged();
        }

        private void OnNewAlarm()
        {
            this.setAlarm(this.alarmTimeDialog.getAlarmTime() * 60);
        }


        //======================================================================
        //  Hotkey dialog
        //======================================================================
        public void cbf_hotkeyInputValueChanged()
        {
            if(this.hotkeyDialog != null)
                this.hotkeyDialog.dataChanged();
        }
    }
}
