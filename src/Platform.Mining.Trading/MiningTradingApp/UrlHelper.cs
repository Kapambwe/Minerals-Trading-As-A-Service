using Microsoft.AspNetCore.Components;

namespace MiningTradingApp
{
    public static class UrlHelper
    {
        public static string WithBase(this NavigationManager nav, string relative)
            => $"{nav.BaseUri}{relative.TrimStart('/')}";
    }
}
