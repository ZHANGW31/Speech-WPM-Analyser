using System;
using System.Threading.Tasks;
using Microsoft.CognitiveServices.Speech;
using System.Collections.Generic;


namespace SpeechWPMCalculator
{
    class Program
    {
        public static async Task RecognizeSpeechAsync()
        {
            var config = SpeechConfig.FromSubscription("", "westus");
            using (var recognizer = new SpeechRecognizer(config))
            {
                var result = await recognizer.RecognizeOnceAsync();

                if (result.Reason == ResultReason.RecognizedSpeech)
                {
                    //List<string> wordsList = new List<string>();
                    Console.WriteLine($"We recognized: {result.Text}");


                }
                else if (result.Reason == ResultReason.NoMatch)
                {
                    Console.WriteLine($"NOMATCH: Speech could not be recognized.");
                }
                else if (result.Reason == ResultReason.Canceled)
                {
                    var cancellation = CancellationDetails.FromResult(result);
                    Console.WriteLine($"Canceled: Reason= {cancellation.Reason}");

                    if (cancellation.Reason == CancellationReason.Error)
                    {
                        Console.WriteLine($"Canceled: ErrorCode={cancellation.ErrorCode}");
                        Console.WriteLine($"Canceled: ErrorDetails={cancellation.ErrorDetails}");
                        Console.WriteLine($"Canceled: Did you update the subscription info?");
                    }
                }

            }
        }
        static void Main()
        {
            RecognizeSpeechAsync().Wait();
            Console.WriteLine("Please press <Return> to continue");
            Console.ReadLine();


        }
    }
}
