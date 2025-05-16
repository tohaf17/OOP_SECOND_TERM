using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using Laba4_Core.Class;
using Laba4_Core.DTO;
using Laba4_Core.Interface;
using Laba4_Core.ViewModel;

namespace Laba_4
{
    public class WPFNavigationService:INavigationService
    {

        public void OpenAddPerformanceForm(Action<Performance> OnSuccess, Action<string> ShowMessage)
        {
            var window = new Perfomance_Form();
            if (window.ShowDialog() == true)
            {
                OnSuccess?.Invoke(window.Result);
                ShowMessage?.Invoke("Performance додано: " + window.Result.Title);
            }
        }
        public void OpenEditPerformanceForm(Performance selectedPerformance, Action<Performance> Updated, Action<string> ShowMessage)
        {
            if (selectedPerformance == null)
            {
                ShowMessage?.Invoke("Please select a performance");
                return;
            }

            var window = new Perfomance_Form(selectedPerformance);
            if (window.ShowDialog() == true)
            {
                var updated = window.Result;
                Updated?.Invoke(updated);
            }
        }
        public void OpenAddConcertForm(Action<Concert> OnSucces, Action<string> ShowMessage)
        {
            var window = new Concert_Form();
            if (window.ShowDialog() == true)
            {
                OnSucces?.Invoke(window.Result);
                ShowMessage?.Invoke($"Concert created: {window.Result.Organizer}");
            }
        }
        public void OpenEditConcertForm(Concert selectedConcert,Action<Concert> Updated,Action<string> ShowMessage)
        {
            if (selectedConcert == null)
            {
                ShowMessage?.Invoke("Please select a concert");
                return;
            }

            var window = new Concert_Form(selectedConcert);
            if (window.ShowDialog() == true)
            {
                var updated = window.Result;
                Updated?.Invoke(updated);
            }
        }
    }
    
}
