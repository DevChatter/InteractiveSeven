using InteractiveSeven.Core.Bidding.Naming;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace InteractiveSeven.Core.Data
{
    public interface IDataStore
    {
        void SaveData(List<CharacterNameBid> nameBids);
        List<CharacterNameBid> LoadData();
    }

    public class FileDataStore : IDataStore
    {
        private readonly ILogger<FileDataStore> _logger;
        private const string FileName = "i7-data.json";

        public FileDataStore(ILogger<FileDataStore> logger)
        {
            _logger = logger;
        }

        public void SaveData(List<CharacterNameBid> nameBids)
        {
            try
            {
                string text = JsonConvert.SerializeObject(nameBids);
                File.WriteAllText(FileName, text);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Failed to Save Data to {FileName}.");
            }
        }

        public List<CharacterNameBid> LoadData()
        {
            try
            {
                if (File.Exists(FileName))
                {
                    string json = File.ReadAllText(FileName);
                    return JsonConvert.DeserializeObject<List<CharacterNameBid>>(json);
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