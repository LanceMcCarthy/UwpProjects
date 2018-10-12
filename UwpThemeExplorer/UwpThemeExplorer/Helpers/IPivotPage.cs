using System.Threading.Tasks;

namespace UwpThemeExplorer.Helpers
{
    public interface IPivotPage
    {
        Task OnPivotSelectedAsync();

        Task OnPivotUnselectedAsync();
    }
}
