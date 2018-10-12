using System.Collections.Generic;
using System.Threading.Tasks;

namespace UwpThemeExplorer.Helpers
{
    public interface IPivotActivationPage
    {
        Task OnPivotActivatedAsync(Dictionary<string, string> parameters);
    }
}
