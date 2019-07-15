using InteractiveSeven.Core.Bidding.Naming;
using InteractiveSeven.Core.Data;
using InteractiveSeven.Core.Models;
using InteractiveSeven.Core.ViewModels;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InteractiveSeven.Startup
{
    public class DataLoader
    {
        private readonly MainWindowViewModel _mainWindowViewModel;
        private readonly ColorPaletteCollection _paletteCollection;
        private readonly IDataStore<CharacterNameBid> _nameBidDataStore;
        private readonly IDataStore<ColorPalette> _paletteDataStore;
        private readonly ILogger<DataLoader> _logger;

        public DataLoader(MainWindowViewModel mainWindowViewModel,
            ColorPaletteCollection paletteCollection,
            IDataStore<CharacterNameBid> nameBidDataStore,
            IDataStore<ColorPalette> paletteDataStore,
            ILogger<DataLoader> logger)
        {
            _mainWindowViewModel = mainWindowViewModel;
            _paletteCollection = paletteCollection;
            _nameBidDataStore = nameBidDataStore;
            _paletteDataStore = paletteDataStore;
            _logger = logger;
        }

        public void LoadPreviousData()
        {
            try
            {
                List<CharacterNameBid> characterNameBids = _nameBidDataStore.LoadData();

                if (characterNameBids?.Any() == true)
                {
                    _mainWindowViewModel.NameBiddingViewModel.Load(characterNameBids);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to load NameBid data.");
            }

            try
            {
                List<ColorPalette> colorPalettes = _paletteDataStore.LoadData();

                if (colorPalettes?.Any() == true)
                {
                    _paletteCollection.Load(colorPalettes);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to load ColorPalette data.");
            }
        }
    }
}