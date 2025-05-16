
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Laba4_Core.Class;
using Laba4_Core.DTO;
using Laba4_Core.Interface;

namespace Laba4_Core.Interface
{
    public interface INavigationService
    {
        void OpenAddPerformanceForm(Action<Performance> OnSuccess, Action<string> ShowMessage);
        void OpenEditPerformanceForm(Performance performance, Action<Performance> Updated, Action<string> ShowMessage);

        void OpenAddConcertForm(Action<Concert> OnSuccess, Action<string> ShowMessage);
        void OpenEditConcertForm(Concert selectedConcert, Action<Concert> Updated, Action<string> ShowMessage);

    }
}
