using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using Companion.Console;
using Google.Apis.Drive.v2.Data;
using ImageMagick;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using Terminal.Gui;
using UglyToad.PdfPig.Core;
using UglyToad.PdfPig.Writer;
using File = System.IO.File;

string deckPath;
var failedDownloads = new List<string>();
var MainMemoryStream = new MemoryStream();


const int IMAGE_WIDTH_POINT = 198;
const int IMAGE_HEIGHT_POINT = 270;

const double IMAGE_WIDTH_INCH = 2.75;
const double IMAGE_HEIGHT_INCH = 3.75;

var TARGET_DPI = 0;

const bool CONVERT_COLOR = true;

//
// if (args.Length != 1)
// {
//     Console.WriteLine("Specify the path to your ProxyFill list:");
//     deckPath = Console.ReadLine();
// }
// else
// {
//     deckPath = args[0];
// }
//
// Console.WriteLine("Specify the target DPI (300,600,1200):");
// TARGET_DPI = Convert.ToInt32(Console.ReadLine());
//
// var listString = File.ReadAllText(deckPath);
// var order = JsonConvert.DeserializeObject<ProxyFill.Shared.Model.ProxyFillOrder>(listString);
// var cardList = order.Cards.Where(x => x.FrontImage != null);
// DownloadImages(cardList).Wait();
// DownloadBacks(cardList).Wait();
//
// var pdfPath = await GeneratePdf(cardList);
// Console.WriteLine("Press any key to exit...");
// Console.ReadLine();
Application.Init();
Application.Run<MainWindow> ();

// Before the application exits, reset Terminal.Gui for clean shutdown
Application.Shutdown ();



Task<string> GeneratePdf(IEnumerable<ProxyFill.Shared.Model.ProxyCardDto> cards)
{
    Console.WriteLine("Generating PDF...");
    var builder = new PdfDocumentBuilder();
    var groupedList = cards.GroupBy(x => x.FrontImage);
    var i = 0;
    foreach (var group in groupedList)
    {
        //var card = group.Select(x => x).First();
        var placement = new PdfRectangle(0, 0, IMAGE_WIDTH_POINT, IMAGE_HEIGHT_POINT);
        foreach (var card in group)
        {
            //back
            var backFileName = GetGoogleDriveId(card.BackImage) + ".png";
            var backPage = builder.AddPage(IMAGE_WIDTH_POINT,IMAGE_HEIGHT_POINT); //300 dpi
            var backFilePath = Path.Combine("Backs", backFileName);
            var compressedBackImage = ProcessImageForPrinting(backFilePath, 25, 50);
            Console.Clear();
            Console.WriteLine($"Inserting {backFileName} (back) to slot {i}");
            ConsoleUtility.ProgressBar(i,groupedList.Count());
            backPage.AddPng(compressedBackImage, placement);
            
            //front
            var filename = $"{card.Name} {card.SetCode} {card.Number} ({GetGoogleDriveId(card.FrontImage)}).png";
            var page = builder.AddPage(IMAGE_WIDTH_POINT,IMAGE_HEIGHT_POINT); //300 dpi
            var filePath = Path.Combine("Images", filename);

            using var stream = new MemoryStream();
            
            var compressedImage = ProcessImageForPrinting(filePath, 25, 75);
            Console.Clear();
            Console.WriteLine($"Inserting {filename} (front) to slot {i*2}");
            ConsoleUtility.ProgressBar(i,groupedList.Count());
            page.AddPng(compressedImage, placement);
        }
        i++;
    }

    builder.DocumentInformation.Author = "ProxyFill";
    builder.DocumentInformation.Subject = "Created by ProxyFill Companion. https://github.com/nathenxbrewer/ProxyFill";
    var documentBytes = builder.Build();
    var deckFileName = Path.GetFileNameWithoutExtension(deckPath);
    if (!Directory.Exists("Output"))
    {
        Directory.CreateDirectory("Output");
    }

    var pdfFilePath = Path.Combine("Output", $"{deckFileName} ({TARGET_DPI} dpi).pdf");
    Console.WriteLine($"Exporting PDF to {Path.GetFullPath(pdfFilePath)}");
    File.WriteAllBytes(pdfFilePath, documentBytes);
    Console.WriteLine($"PDF export complete!");
    ConsoleUtility.ProgressBar(i,groupedList.Count());
    return Task.FromResult(pdfFilePath);
}

static string GetGoogleDriveId(string driveURL)
{
    return driveURL.Replace(@"https://drive.google.com/uc?export=download&id=", "");
}

MemoryStream ProcessImageForPrinting(string filePath, int quality, int sizePercent)
{
    var filename = Path.GetFileName(filePath);
    Console.WriteLine($"Checking if image needs compressed...");
    using (var image = new MagickImage(filePath))
    {
        var width = image.Width;
        var height = image.Height;
        var dpi = ((width / IMAGE_WIDTH_INCH) + (height / IMAGE_HEIGHT_INCH)) / 2;
        //reset memorystream
        MainMemoryStream.Position = 0;
        MainMemoryStream.SetLength(0);
        if (dpi > TARGET_DPI)
        {
            Console.WriteLine($"Compressing {filename}...");
            var dpiPercent = (TARGET_DPI / dpi * 100);
            image.Resize(new Percentage(dpiPercent));
        }
        //image.Quality = quality; // This is the Compression level.
        //image.Resample(300,300);
        image.Write(MainMemoryStream);
        image.Write("test.png");
        if (CONVERT_COLOR)
        {
            // FileStream fileStream = new FileStream("CGATS21_CRPC7.icc", FileMode.CreateNew);
            // for (int i = 0; i < stream.Length; i++)
            //     fileStream.WriteByte((byte)stream.ReadByte());
            // fileStream.Close();
            var colorProfileStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Companion.Console.Profiles.CGATS21_CRPC1.icc");
            var profile = new ColorProfile(colorProfileStream);
            image.SetProfile(profile);
            // if (image.ColorSpace != ColorSpace.CMYK)
            // {
            //    // image.TransformColorSpace(ColorProfile.);
            // }
        }
        MainMemoryStream.Position = 0;
        // var imageOptimizer = new ImageOptimizer();
        // imageOptimizer.Compress(stream);

        return MainMemoryStream;
    }
}

async Task DownloadImages(IEnumerable<ProxyFill.Shared.Model.ProxyCardDto> cards)
{
    Console.WriteLine("Downloading front images...");
    if (!Directory.Exists("Images"))
    {
        Directory.CreateDirectory("Images");
    }
    
    //group by imageURL for duplicates, they may have multiple cards with different proxy art, so we cant group by card. 
    var groupedList = cards.GroupBy(x => x.FrontImage).ToList();
    var imageCount = groupedList.Count;
    var x = 0;

    foreach (var group in groupedList)
    {
        var card = group.Select(x => x).First();
        var filename = $"{card.Name} {card.SetCode} {card.Number} ({GetGoogleDriveId(card.FrontImage)}).png";
        var path = $"Images/{filename}";

        if (File.Exists(path))
        {
            Console.Clear();
            Console.WriteLine($"{filename} already exists, continuing");
            continue;
        }

        var downloadLink = group.Key;
        if (!await DownloadDriveFile(downloadLink, path))
        {
            Console.WriteLine($"Failed to download {filename}...");
            failedDownloads.Add(filename);
        }

        x++;
        ConsoleUtility.ProgressBar(x,imageCount);
    }
    Console.WriteLine("Front images downloaded!");
}

async Task DownloadBacks(IEnumerable<ProxyFill.Shared.Model.ProxyCardDto> cards)
{
    Console.WriteLine("Downloading back images...");
    if (!Directory.Exists("Backs"))
    {
        Directory.CreateDirectory("Backs");
    }

    //group by imageURL for duplicates, they may have multiple cards with different proxy art, so we cant group by card. 
    var groupedList = cards.GroupBy(x => x.BackImage).ToList();
    var imageCount = groupedList.Count();
    var x = 0;
    foreach (var group in groupedList)
    {
        x++;
        var card = group.Select(x => x).First();
        var filename = card.BackImage.Replace(@"https://drive.google.com/uc?export=download&id=","") + ".png";
        var path = $"Backs/{filename}";

        if (File.Exists(path))
        {
            Console.Clear();
            Console.WriteLine($"{filename} already exists, continuing...");
            continue;
        }

        var downloadLink = group.Key;
        if (!await DownloadDriveFile(downloadLink, path))
        {
            failedDownloads.Add(filename);
        }

        ConsoleUtility.ProgressBar(x,imageCount);
    }
    Console.WriteLine("Back images downloaded!");
}
static async Task<bool> DownloadDriveFile(string downloadLink, string path)
{
    if (downloadLink.Contains("https://drive.google.com/uc?"))
    {
        var fileId = downloadLink.Substring(47);
        downloadLink =
            $"https://content.googleapis.com/drive/v2/files/{fileId}?key=AIzaSyBOGAtxTDZMJas_EkIRb0pVBpyQYpTaHXU&alt=media&source=downloadUrl";
    }

    using var client = new HttpClient();
    using var result = await client.GetAsync(downloadLink);
    if (!result.IsSuccessStatusCode) return false;
    var imageData = await result.Content.ReadAsByteArrayAsync();
    File.WriteAllBytes(path, imageData);
    
    //validate image is there
    if (!File.Exists(path)) return false;
    Console.Clear();
    Console.WriteLine($"Downloaded {Path.GetFileName(path)} successfully!");
    return true;
}

static void AutoFill()
{
    var driver = new EdgeDriver(@"/Users/nathenbrewer/Downloads");
    driver.Url = "https://www.drivethrucards.com/login.php";
    var orLogInLink = driver.FindElement(By.XPath("//*[@id=\"create_account_box\"]/div[2]/span/span[1]/a"));
    orLogInLink.Click();
    driver.FindElement(By.Id("login_email_address")).SendKeys("email");
    driver.FindElement(By.Id("login_password")).SendKeys("password");
    driver.FindElement(By.Id("loginbutton")).Click();
    Thread.Sleep(TimeSpan.FromSeconds(3));
    driver.Navigate().GoToUrl("https://www.drivethrucards.com/builder/deck/images/back/64baff58d1078");
//driver.Navigate().GoToUrl("https://www.drivethrucards.com/builder/deck/images/back/");
    var uploadArea = driver.FindElement(By.XPath("//*[@id=\"upload\"]/div[6]/div/div[2]/div[1]"));
    driver.ExecuteScript("arguments[0].style.display = 'flex';", uploadArea);
    var uploadBox = driver.FindElement(By.Id("upload_upload"));
    var path = Path.GetFullPath("//Users/nathenbrewer//Downloads//Pokemon MPC Back.png");
//uploadBox.SendKeys(path);
//Helper.DropFile(driver, uploadBox, path);


    var dropper = driver.FindElement(By.XPath("//*[@id=\"upload\"]/div[6]/div/div[2]"));
    Helper.DropFile(driver, dropper, path);




//var uploadButton = driver.FindElement(By.XPath("//*[@id=\"upload\"]/div[6]/div/div[2]/div[1]/input[2]"));
//uploadButton.Click();

//wait to see result in table
    Console.ReadLine();
//https://www.drivethrucards.com/login.php
//https://www.drivethrucards.com/index.php
//https://www.drivethrucards.com/builder/deck/images/back/
//name upload_upload
//table class=uploaded-images table

}

