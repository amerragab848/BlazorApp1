using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp1.Pages
{
    public partial class Counter
    {
        private int? currentCount = 0;

        protected override async Task OnInitializedAsync()
        {
            currentCount = await ProtectedSessionStore.GetAsync<int>("count");
        }
        async  void IncrementCount()
        {
            currentCount++;
            await ProtectedSessionStore.SetAsync("count", currentCount);

        }
    }
}
