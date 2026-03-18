// AI project once again :(
// At least this code is (somewhat) handwritten...

// short rant: where the fuck is the documentation for image-to-image generation in .NET??
// Haken, I'm sorry, but I'm gonna have to borrow code from your demo once again

using Azure;
using Azure.AI.OpenAI;
using OpenAI.Images;

Console.Write("Feed me an API key: ");
string apiKey = Console.ReadLine();

AzureOpenAIClient leClient = new(
    new Uri("https://hakenswedenoai.openai.azure.com/"),
    new AzureKeyCredential(apiKey));

ImageClient leImageClient = leClient.GetImageClient("gpt-image-1.5");

Console.WriteLine("\nFor the sake of '''convenience''', all images will be stored in the desktop directory.");
Console.WriteLine("Please input a file name. Make sure to include the file extension (i.e. .png, .jpeg, et cetera)");
Console.Write("Your file here: ");
string leImagePath = Environment.SpecialFolder.DesktopDirectory + "\\" + Console.ReadLine();

if (!File.Exists(leImagePath)) {
    Console.WriteLine("This file doesn't exist.");
    Console.WriteLine(leImagePath);
    return;
}

List<string> listofObjects = 
    ["a pair of scissors", "a rock", "a sheet of paper", "a bus", "a tram", "an SUV",
    "a transport truck", "a trolleybus", "a train", "a brain cell", "a laptop",
    "a computer keyboard", "a computer mouse", "a piano", "a guitar", "a flute", "a clarinet",
    "a trombone", "a whistle", "a ukulele", "a bell", "a liver", "a jet plane", "a clay model",
    "a robot", "a bicycle", "a motorcycle", "a pistol", "a crossbow", "a glass of water",
    "a chalice", "a coffee mug", "a door", "a window", "an atom", "a molecule",
    "a proton", "a neutron", "an electron"];

Random rng = new Random();

// Create a prompt to "objectify" the main character of interest
string prompt = """
                Based on the appearance of the main character of interest seen in the image, 
                generate a photograph of the main character of interest if they were 
                """ + listofObjects[rng.Next(0, listofObjects.Count)] + """.""";

Console.WriteLine("\nObjectifying...");

GeneratedImage result = await leImageClient.GenerateImageEditAsync(leImagePath, prompt);
File.WriteAllBytes(leImagePath, result.ImageBytes);