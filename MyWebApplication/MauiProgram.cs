using Microsoft.Extensions.Logging;
using MyGYMMaui.Storage;
using Pages.Interfaces;
using Pages.Pages;
using Report;
using Report.Model;
using System.IO;
using System.Threading;
using Stimulsoft.Report;
using Report.Res;

namespace MyWebApplication
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();

            builder.Services.AddSingleton<IStorage, Storage>();
#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            Home.Share = ShareFile;
            Home.PeakFile = PickAndShow;

            Home.Report = async (dataObj) =>
            {
                ReportCreator rc = new ReportCreator();

                await using MemoryStream stream = new MemoryStream();

                var dataList =
                    dataObj.Actions.SelectMany(
                        x => x.ProgramActions,
                        (result, action) => new ReportModel()
                        {
                            Category = CreateProgram.GetProgramActionsStr(result),
                            Count = ViewProgramRecord.GetDetaile(action),
                            Description = action.Description,
                            Name = action.Name
                        });

                rc.CreateReport(stream, CustomReportResource.TestReport,
                    new KeyValuePair<string, object>("ReportModel", dataList));

                await ShareFile($"{dataObj.Name}.pdf", stream.GetBuffer());

            };

            return builder.Build();
        }
        public static async Task ShareFile(string Name, string Data)
        {
            string file = Path.Combine(FileSystem.CacheDirectory, Name);

            File.WriteAllText(file , Data);

            await Share.Default.RequestAsync(new ShareFileRequest
            {
                Title = Name,
                File = new ShareFile(file)
            });
        }
        public static async Task ShareFile(string Name, byte[] Data)
        {
            string file = Path.Combine(FileSystem.CacheDirectory, Name);

            File.WriteAllBytes(file, Data);

            await Share.RequestAsync(new ShareFileRequest
            {
                Title = Name,
                File = new ShareFile(file)
            });
        }

        public static async Task<string> PickAndShow()
        {
            try
            {
                var customFileType = new FilePickerFileType(
                    new Dictionary<DevicePlatform, IEnumerable<string>>
                    {
                        { DevicePlatform.Android, new[] { "application/json" } },
                        { DevicePlatform.WinUI, new[] { ".json" } },
                    });

                PickOptions options = new()
                {
                    PickerTitle = "یک فایل انتخاب کنید",
                    FileTypes = customFileType,
                };

                var result = await FilePicker.Default.PickAsync(options);

                if (!result.FileName.ToLower().EndsWith(".json")) throw new Exception();

                var reader = await result.OpenReadAsync();

                using StreamReader readerFile = new StreamReader(reader);

                return await readerFile.ReadToEndAsync();

            }
            catch (Exception ex)
            {
            }

            return null;
        }
    }
}
