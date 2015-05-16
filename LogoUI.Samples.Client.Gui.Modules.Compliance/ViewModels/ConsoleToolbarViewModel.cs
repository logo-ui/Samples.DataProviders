using Caliburn.Micro;

namespace LogoUI.Samples.Client.Gui.Modules.Compliance.ViewModels
{
    public sealed class ConsoleToolbarViewModel : Conductor<ToolbarItemViewModel>.Collection.AllActive
    {
         
    }

    public abstract class ToolbarItemViewModel : Screen
    {
        
    }

    public sealed class ButtonToolbarItemViewModel : ToolbarItemViewModel
    {
        
    }

    public sealed class MenuButtonToolbarItemViewModel : ToolbarItemViewModel
    {

    }
}