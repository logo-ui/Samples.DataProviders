using LogoFX.UI.Navigation;

namespace LogoUI.Samples.Client.Gui.Modularity.Contracts
{
    public interface ILogoUiModule : INavigationModule
    {
        bool IsGroup { get; }
    }
}