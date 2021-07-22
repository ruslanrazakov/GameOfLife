using System;
using System.IO;
using System.Timers;
using System.Threading.Tasks;
using GameOfLife.Core;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

namespace GameOfLifeClient.Pages
{
    public partial class Index
    {
        
        [Inject]
        public ILogger<Index> Logger { get; set; }
        public int TimerInterval { get; set; } = 500;
        public Services.TimerAsync Timer { get; set; }
        Universe Universe { get; set; } = new Universe();
        
        protected override async Task OnInitializedAsync()
        {
            Universe.CreateEmpty(10);
            Universe.DrawGlider(new Point(0, 0));

            Timer = new Services.TimerAsync(delay: 500);
            await Timer.Start(async () => await Update());
        }

        private async Task Update()
        {
            Universe.Update();
            StateHasChanged();
            //await 
        }
    }
}