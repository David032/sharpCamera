using System.Diagnostics;
namespace sharpCamera
{
    public class PiCamera
    {
        private Process? cameraCommand;
        private TaskCompletionSource<bool> eventHandled;

        public PiCamera()
        {

        }

        #region Still Images
        public void TakePicture()
        {
            try
            {
                using (cameraCommand = new Process())
                {
                    CameraSettings settings = new();
                    string fileNameToOutput = "-o " + settings.OutputLocation + "/" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
                    cameraCommand.StartInfo.UseShellExecute = false;
                    cameraCommand.StartInfo.FileName = "raspistill";
                    cameraCommand.StartInfo.Arguments = fileNameToOutput;

#if DEBUG
                    Console.Out.WriteLine(cameraCommand.StartInfo.Arguments);
#endif
                    cameraCommand.Start();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public async Task TakePictureAsync()
        {
            eventHandled = new TaskCompletionSource<bool>();
            using (cameraCommand = new Process())
            {
                try
                {
                    CameraSettings settings = new();
                    string fileNameToOutput = "-o " + settings.OutputLocation + "/" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
                    cameraCommand.StartInfo.UseShellExecute = false;
                    cameraCommand.StartInfo.FileName = "raspistill";
                    cameraCommand.StartInfo.Arguments = fileNameToOutput;
                    cameraCommand.EnableRaisingEvents = true;
                    cameraCommand.Exited += new EventHandler(ImageTaken);
#if DEBUG
                    Console.Out.WriteLine(cameraCommand.StartInfo.Arguments);
#endif
                    cameraCommand.Start();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return;
                }

                await Task.WhenAny(eventHandled.Task, Task.Delay(30000));
            }
        }

        private void ImageTaken(object sender, EventArgs e)
        {
            eventHandled.TrySetResult(true);
        }
        #endregion

        #region Videos
        public void RecordVideo()
        {
            try
            {
                using (cameraCommand = new Process())
                {
                    VideoCameraSettings settings = new();
                    string args = "-o " + settings.OutputLocation + "/" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".h264 -t " + (settings.RecordingDuration * 1000);
                    cameraCommand.StartInfo.UseShellExecute = false;
                    cameraCommand.StartInfo.FileName = "raspivid";
                    cameraCommand.StartInfo.Arguments = args;

#if DEBUG
                    Console.Out.WriteLine(cameraCommand.StartInfo.Arguments);
#endif
                    cameraCommand.Start();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public async Task RecordVideoAsync()
        {
            eventHandled = new TaskCompletionSource<bool>();
            using (cameraCommand = new Process())
            {
                try
                {
                    VideoCameraSettings settings = new();
                    string args = "-o " + settings.OutputLocation + "/" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".h264 -t " + (settings.RecordingDuration * 1000);
                    cameraCommand.StartInfo.UseShellExecute = false;
                    cameraCommand.StartInfo.FileName = "raspivid";
                    cameraCommand.StartInfo.Arguments = args;
                    cameraCommand.EnableRaisingEvents = true;
                    cameraCommand.Exited += new EventHandler(ImageTaken);
#if DEBUG
                    Console.Out.WriteLine(cameraCommand.StartInfo.Arguments);
#endif
                    cameraCommand.Start();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return;
                }

                await Task.WhenAny(eventHandled.Task, Task.Delay(45000));
            }
        }

        private void VideoRecorded(object sender, EventArgs e)
        {
            eventHandled.TrySetResult(true);
        }
        #endregion

    }
}