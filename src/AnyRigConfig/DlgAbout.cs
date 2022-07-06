using System.Reflection;
using AnyRigConfigCommon;
using Terminal.Gui;

namespace AnyRigConfig
{
    internal class DlgAbout : Dialog
    {
        internal DlgAbout() : base("About", 40, 16)
        {

            AssemblyName assemblyName = Assembly.GetExecutingAssembly().GetName();

            AssemblyInformationalVersionAttribute attribute = Assembly.GetExecutingAssembly()
                .GetCustomAttribute(typeof(AssemblyInformationalVersionAttribute))
                as AssemblyInformationalVersionAttribute;

            this.Add(new Label(1, 0, assemblyName.Name));
            //this.Add(new Label(1, 1, "AnyRigLibrary"));
            this.Add(new Label(1, 2, $"Ver {attribute?.InformationalVersion}"));
            this.Add(new Label(1, 3, ConfigCommon.GetServiceVersion()));
            this.Add(new Label(1, 4, $"Copyright 2022-{DateTime.Now.Year} by IW1QLH"));
            this.Add(new Label(1, 5, "Translation by IW1QLH"));

            this.Add(new Label(new Rect(0, 7, 38, 5), "This software is provided 'as-is', without any express or implied warranty. In no event will the authors be held liable for any damages arising from the use of this software.") 
            { 
                ColorScheme = Colors.Error 
            });

            Button btnClose = new Button("Close", true);
            btnClose.Clicked += () => { Running = false; };
            this.AddButton(btnClose);

        }

    }
}
