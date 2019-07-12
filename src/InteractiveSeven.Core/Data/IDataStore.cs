using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace InteractiveSeven.Core.Data
{
    public interface IDataStore<T>
    {
        void SaveData(List<T> items);
        List<T> LoadData();
    }

    public class FileDataStore<T> : IDataStore<T>
    {
        private readonly ILogger<FileDataStore<T>> _logger;
        private string FileName => $"i7-{typeof(T).Name}-data.json";

        public FileDataStore(ILogger<FileDataStore<T>> logger)
        {
            _logger = logger;
        }

        public void SaveData(List<T> items)
        {
            try
            {
                string text = JsonConvert.SerializeObject(items);
                File.WriteAllText(FileName, text);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Failed to Save Data to {FileName}.");
            }
        }

        public List<T> LoadData()
        {
            try
            {
                if (File.Exists(FileName))
                {
                    string json = File.ReadAllText(FileName);
                    return JsonConvert.DeserializeObject<List<T>>(json);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Failed to Load Data from {FileName}.");
            }

            return null;
        }
    }
}