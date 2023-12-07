using System.Text;
using Microsoft.JSInterop;
using Pages.Interfaces;
using Pages.Models.StorageModel;
using Pages.Pages;


public class WebFileWorker : IFileWorker
{
    IJSRuntime JS;

    public WebFileWorker(IJSRuntime js)
    {
        JS = js;
    }


    public async Task DownloadBackup(string FileName, string Data)
    {
        byte[] bufer = Encoding.ASCII.GetBytes(Data);
        await JS.InvokeVoidAsync("downloadFileFromStream", FileName, bufer);
    }
    public async Task DownloadBackup(string FileName, byte[] Data)
    {
        await JS.InvokeVoidAsync("downloadFileFromStream", FileName, Data);
    }
    public async Task UploadFile()
    {
        var Reference = DotNetObjectReference.Create(this);

        await JS.InvokeVoidAsync("addCustomEventListener", new object?[] { Reference });
    }

    [JSInvokable]
    public async Task FileInput(string obj)
    {
        OnUpload?.Invoke(obj);
    }
    public event OnUploadDelegate? OnUpload;
}