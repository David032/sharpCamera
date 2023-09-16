using System.Diagnostics;
namespace sharpCamera
{
    public class PiCamera
    {
        private Process? cameraCommand;
        private string outputPath = "/output";

        public PiCamera()
        {
            if (!Directory.Exists(outputPath))
            {
                // Can't set permissions for unix in .net 6 easily :(
                // DirectoryInfo di = Directory.CreateDirectory(outputPath);
            }
        }

        public void TakePicture()
        {
            try
            {
                using (cameraCommand = new Process())
                {
                    string fileNameToOutput = "-o " + DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
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

        public void RecordVideo()
        {
            try
            {
                using (cameraCommand = new Process())
                {
                    string args = "-o " + DateTime.Now.ToString("yyyyMMddHHmmss") + ".h264 -t 30000";
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
    }
}