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
        public System.Timers.Timer MainLoopTimer { get; set; }
        Universe Universe { get; set; } = new Universe();
        
        protected override async Task OnInitializedAsync()
        {
            Universe.CreateEmpty(10);
            Universe.DrawGlider(new Point(0, 0));

            MainLoopTimer = new Timer(TimerInterval)
            {
                Enabled = true
            };
            MainLoopTimer.Elapsed += Update;
        }

        private void Update(object sender, ElapsedEventArgs e)
        {
            Universe.Update();
            InvokeAsync(() => StateHasChanged());
        }
    }
}