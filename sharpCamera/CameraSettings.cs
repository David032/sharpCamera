namespace sharpCamera
{
    public enum Duration
    {
        FifteenSeconds = 15,
        ThirtySeconds = 30,
        OneMinute = 60,
        TwoMinutes = 120,
    }

    public class CameraSettings
    {
        public int Height
        {
            get
            {
                return _height;
            }
            set
            {
                _height = value;
            }
        }
        protected int _height = 1080;

        public int Width
        {
            get
            {
                return _width;
            }
            set
            {
                _width = value;
            }
        }
        protected int _width = 1920;

        public string OutputLocation
        {
            get { return _outputLocation; }
        }
        protected string _outputLocation;

        public CameraSettings()
        {
            _outputLocation = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
        }
    }

    public class VideoCameraSettings : CameraSettings
    {
        private int duration;

        public int RecordingDuration
        {
            get { return duration; }
            set { duration = value; }
        }

        public VideoCameraSettings(Duration recordTime = Duration.ThirtySeconds)
        {
            _outputLocation = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos);
            duration = (int)recordTime;
        }
    }
}
