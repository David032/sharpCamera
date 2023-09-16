using sharpCamera;

namespace sharpCameraExample
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            Console.WriteLine("Say cheese!");
            PiCamera cam = new();
            await cam.TakePictureAsync();
            Console.WriteLine("Now time for a little video!");
            await cam.RecordVideoAsync();
            Console.WriteLine("All done!");
        }
    }
}