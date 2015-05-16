using System.Windows.Controls.Primitives;

namespace LogoUI.Samples.Client.Gui.Shared.Views.Controls
{
    public interface IDragThumb
    {
        event DragStartedEventHandler DragStarted;

        event DragCompletedEventHandler DragCompleted;

        event DragDeltaEventHandler DragDelta;

        void CancelDrag();

        void BeginDrag();
    }
}