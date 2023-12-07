using Pages.Interfaces;

namespace GymApp.Storage;

public class MauiFileWorker : IFileWorker
{
    public async Task DownloadBackup(string FileName, string Data)
    {
        string file = Path.Combine(FileSystem.CacheDirectory, FileName);

        await File.WriteAllTextAsync(file, Data);

        await Share.Default.RequestAsync(new ShareFileRequest
        {
            Title = FileName,
            File = new ShareFile(file)
        });
    }

    public async Task UploadFile()
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

            var peaked = await FilePicker.Default.PickAsync(options);

            if (!peaked.FileName.ToLower().EndsWith(".json")) throw new Exception();

            var reader = await peaked.OpenReadAsync();

            using StreamReader readerFile = new StreamReader(reader);

            var result = await readerFile.ReadToEndAsync();

            OnUpload?.Invoke(result);
        }
        catch (Exception ex)
        {
        }


    }

    public event OnUploadDelegate? OnUpload;
}