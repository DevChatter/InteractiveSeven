using System;
using System.Configuration;

namespace InteractiveSeven.UI.Settings
{
    public class InteractionSettings : ConfigurationSection
    {
        static InteractionSettings()
        {

            Configuration config =
                ConfigurationManager.OpenExeConfiguration(
                    ConfigurationUserLevel.None);

            if (config.Sections[nameof(InteractionSettings)] == null)
            {
                config.Sections.Add(nameof(InteractionSettings), MakeDefault(config));
            }
            Settings = config.GetSection(nameof(InteractionSettings)) as InteractionSettings;
         }

        private InteractionSettings(string processName)
        {
            this["processName"] = processName;
            base[nameof(Interactions)] = new InteractionElementCollection();
        }

        private static InteractionSettings MakeDefault(Configuration config)
        {
            var settings = new InteractionSettings("ff7_en");

            settings.Interactions.Add(new InteractionElement("MenuColor", true));

            return settings;
        }

        public static InteractionSettings Settings { get; }

        [ConfigurationProperty("processName", IsRequired = false, DefaultValue = "ff7_en")]
        public string ProcessName
        {
            get => this["processName"].ToString();
            set
            {
                this["processName"] = value;
                CurrentConfiguration.Save(ConfigurationSaveMode.Minimal);
            }
        }

        [ConfigurationProperty(nameof(Interactions))]
        public InteractionElementCollection Interactions
        {
            get => ((InteractionElementCollection)(base[nameof(Interactions)]));
            set
            {
                base[nameof(Interactions)] = value;
                CurrentConfiguration.Save(ConfigurationSaveMode.Minimal);
            }
        }
    }

    public class InteractionElement : ConfigurationElement
    {
        public InteractionElement(string name, bool enabled)
        {
            base["name"] = name;
            base["enabled"] = enabled;
        }

        [ConfigurationProperty("name", IsRequired = true, IsKey = true)]
        public string Name
        {
            get => base["name"].ToString();
            set
            {
                base["name"] = value;
                CurrentConfiguration.Save(ConfigurationSaveMode.Minimal);
            }
        }

        [ConfigurationProperty("enabled", IsRequired = false)]
        public bool Enabled
        {
            get => (bool)base["enabled"];
            set
            {
                base["enabled"] = value;
                CurrentConfiguration.Save(ConfigurationSaveMode.Minimal);
            }
        }
    }

    [ConfigurationCollection(typeof(InteractionElement))]
    public class InteractionElementCollection : ConfigurationElementCollection
    {
        internal const string PropertyName = "Interaction";

        public override ConfigurationElementCollectionType CollectionType 
            => ConfigurationElementCollectionType.BasicMapAlternate;

        protected override string ElementName => PropertyName;

        protected override bool IsElementName(string elementName)
        {
            return elementName.Equals(PropertyName,
                StringComparison.InvariantCultureIgnoreCase);
        }

        public override bool IsReadOnly() => false;

        protected override ConfigurationElement CreateNewElement() => new InteractionElement(null, false);

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((InteractionElement)(element)).Name;
        }

        public InteractionElement ByName(string name) => (InteractionElement)BaseGet(name);

        public InteractionElement this[int index] => (InteractionElement)BaseGet(index);

        public void Add(InteractionElement element)
        {
            BaseAdd(element, true);
            CurrentConfiguration.Save(ConfigurationSaveMode.Minimal);
        }
    }
}
