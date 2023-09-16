using sharpCamera;

namespace sharpCameraExample
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Say cheese!");
            PiCamera cam = new PiCamera();
            cam.RecordVideo();
        }
    }
}