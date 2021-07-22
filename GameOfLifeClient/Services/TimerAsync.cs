using System;
using System.Threading;
using System.Threading.Tasks;

namespace GameOfLifeClient.Services
{
    public class TimerAsync
    {
        private int _delay;
        private CancellationTokenSource _token;

        public TimerAsync(int delay)
        {
            _delay = delay;
        }

        public async Task Start(Func<Task> onTimeEvent)
        {
            _token = new CancellationTokenSource();
            while(!_token.Token.IsCancellationRequested)
            {
                await onTimeEvent();
                await Task.Delay(_delay);
            }
        }

        public void Stop()
        {
            _token.Cancel();
        }
    }
}