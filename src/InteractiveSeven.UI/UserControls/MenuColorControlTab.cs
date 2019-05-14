using InteractiveSeven.Core.Memory;
using InteractiveSeven.Core.Models;
using InteractiveSeven.Core.Settings;
using InteractiveSeven.UI.ViewModels;
using ReactiveUI;
using ReactiveUI.Winforms;
using System;
using System.Windows.Forms;
using InteractiveSeven.Core.Events;

namespace InteractiveSeven.UI.UserControls
{
    public partial class MenuColorControlTab : ReactiveUserControl<MenuControlViewModel>
    {
        private readonly IMenuColorAccessor _menuColorAccessor;
        private string ProcessName => ApplicationSettings.Instance.ProcessName;

        // TODO: DI this UserControl in the View to avoid this.
        public MenuColorControlTab()
            : this(new MenuColorAccessor(new MemoryAccessor()), new MenuControlViewModel())
        {
        }

        public MenuColorControlTab(IMenuColorAccessor menuColorAccessor,
            MenuControlViewModel viewModel)
        {
            _menuColorAccessor = menuColorAccessor;
            ViewModel = viewModel;

            InitializeComponent();

            DomainEvents.Register<MenuColorChanging>(de 
                => Invoke(new Action(() => ViewModel.SetColors(de.MenuColors))));

            this.Bind(ViewModel, x => x.EnableChatControl, x => x.enableMenuCheckBox.Checked);
            this.Bind(ViewModel, x => x.BitCost, x => x.bitCostTextBox.Text);

            this.Bind(ViewModel, x => x.TopLeftColor, x => x.topLeftColorPicker.Color);
            this.Bind(ViewModel, x => x.BottomLeftColor, x => x.botLeftColorPicker.Color);
            this.Bind(ViewModel, x => x.TopRightColor, x => x.topRightColorPicker.Color);
            this.Bind(ViewModel, x => x.BottomRightColor, x => x.botRightColorPicker.Color);

            this.OneWayBind(ViewModel, x => x.TopLeftColor, x => x.topLeftColorSwatch.BackColor);
            this.OneWayBind(ViewModel, x => x.BottomLeftColor, x => x.botLeftColorSwatch.BackColor);
            this.OneWayBind(ViewModel, x => x.TopRightColor, x => x.topRightColorSwatch.BackColor);
            this.OneWayBind(ViewModel, x => x.BottomRightColor, x => x.botRightColorSwatch.BackColor);
        }

        internal void RefreshColors()
        {
            if (string.IsNullOrWhiteSpace(ProcessName))
            {
                return;
            }

            MenuColors currentColors = _menuColorAccessor.GetMenuColors(ProcessName);

            Invoke(new Action(() => ViewModel.SetColors(currentColors)));
        }

        private void RefreshColorsButton_Click(object sender, EventArgs e)
        {
            RefreshColors();
        }

        private void SetColorsButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ProcessName))
            {
                return;
            }

            var menuColors = new MenuColors
            {
                TopLeft = topLeftColorPicker.Color,
                TopRight = topRightColorPicker.Color,
                BotLeft = botLeftColorPicker.Color,
                BotRight = botRightColorPicker.Color
            };

            _menuColorAccessor.SetMenuColors(ProcessName, menuColors);
        }
    }
}
