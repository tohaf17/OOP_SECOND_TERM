using Laba4_Core.Class;
using Laba4_Core.Interface;

namespace Laba4_Web.Services
{
    public class BlazorNavigationService : INavigationService
    {
        private readonly ConcertState concertState;
        private readonly PerformanceState performanceState;


        public BlazorNavigationService(ConcertState cs, PerformanceState ps)
        {
            concertState = cs;
            performanceState = ps;
        }

        public void OpenAddConcertForm(Action<Concert> onSuccess, Action<string> showMessage)
        {
            concertState.SelectedConcert = null;
            concertState.OnSuccess = onSuccess;
            concertState.OnShowMessage = showMessage;
            concertState.TriggerDialog?.Invoke(); // показ діалогу
        }

        public void OpenEditConcertForm(Concert concert, Action<Concert> onUpdated, Action<string> showMessage)
        {
            concertState.SelectedConcert = concert;
            concertState.OnSuccess = onUpdated;
            concertState.OnShowMessage = showMessage;
            concertState.TriggerDialog?.Invoke(); // показ діалогу
        }

        public void OpenAddPerformanceForm(Action<Performance> onSuccess, Action<string> showMessage)
        {
            performanceState.SelectedPerformance = null;
            performanceState.OnSuccess = onSuccess;
            performanceState.OnShowMessage = showMessage;
            performanceState.TriggerDialog?.Invoke(); // показ діалогу
        }

        public void OpenEditPerformanceForm(Performance perf, Action<Performance> onUpdated, Action<string> showMessage)
        {
            performanceState.SelectedPerformance = perf;
            performanceState.OnSuccess = onUpdated;
            performanceState.OnShowMessage = showMessage;
            performanceState.TriggerDialog?.Invoke(); // показ діалогу
        }
    }

    public class ConcertState
    {
        public Concert? SelectedConcert { get; set; }
        public Action<Concert>? OnSuccess { get; set; }
        public Action<string>? OnShowMessage { get; set; }
        public Action? TriggerDialog { get; set; }
    }

    public class PerformanceState
    {
        public Performance? SelectedPerformance { get; set; }
        public Action<Performance>? OnSuccess { get; set; }
        public Action<string>? OnShowMessage { get; set; }
        public Action? TriggerDialog { get; set; }
    }
}
