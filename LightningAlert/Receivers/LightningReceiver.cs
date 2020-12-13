using LightningAlert.Dtos;
using LightningAlert.Enums;
using LightningAlert.Utils;
using System;
using System.IO;
using System.Text.Json;

namespace LightningAlert.Receivers
{
    public class LightningReceiver
    {
        public event EventHandler<LightingReceivedEventArgs> Received;

        public void Listen()
        {
            Console.WriteLine("\nInput the full path of the lightning file:");
            
            var path = Console.ReadLine();

            while (!IsFileValid(path))
            {
                Listen();
            }

            ProcessLightningData(path);

            Listen();
        }

        private void ProcessLightningData(string path)
        {
            Console.WriteLine("Start processing...");

            using var reader = new StreamReader(path);

            while (reader.Peek() >= 0)
            {
                var line = reader.ReadLine();

                if (string.IsNullOrWhiteSpace(line))
                    continue;

                try
                {
                    var lightning = JsonSerializer.Deserialize<LightningDto>(line, Constants.JsonSerializerOptions);

                    if (!IsLightningStrike(lightning.FlashType))
                        continue;

                    OnReceived(new LightingReceivedEventArgs(lightning));
                }
                catch (JsonException) // It will go thru here if "line" is not parsable.
                {
                    // Not sure if it's ok to Console.Write this, but for logging purposes lets just do it.
                    Console.WriteLine($"Invalid lightning data was received.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unknown error occured. {ex.Message}");
                }
            }

            Console.WriteLine("Done!");
        }

        private static bool IsLightningStrike(int flashType)
        {
            return flashType == (int)FlashType.CloudToGround || flashType == (int)FlashType.CloudToCloud;
        }

        private static bool IsFileValid(string path)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine("File doesn't exists.");
                return false;
            }

            if (Path.GetExtension(path) != ".json")
            {
                Console.WriteLine("Invalid file extension. Please provide a valid JSON file.");
                return false;
            }

            return true;
        }

        private void OnReceived(LightingReceivedEventArgs e)
        {
            Received?.Invoke(this, e);
        }
    }
}
