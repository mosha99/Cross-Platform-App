using Pages.Models.StorageModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pages.Interfaces;

public delegate void OnUploadDelegate(string data);

public interface IFileWorker
{
    public Task DownloadBackup(string FileName, string Data);
    public Task UploadFile();

    public event OnUploadDelegate OnUpload;
}

