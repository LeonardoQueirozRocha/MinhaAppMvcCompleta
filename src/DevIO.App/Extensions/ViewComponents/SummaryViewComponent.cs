using DevIO.Business.Interfaces.Notifications;
using Microsoft.AspNetCore.Mvc;

namespace DevIO.App.Extensions.ViewComponents
{
    public class SummaryViewComponent : ViewComponent
    {
        private readonly INotifier _notifier;

        public SummaryViewComponent(INotifier notifier)
        {
            _notifier = notifier;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var notications = await Task.FromResult(_notifier.GetNotifications());

            notications.ForEach(notication => ViewData.ModelState.AddModelError(string.Empty, notication.Message));

            return View();
        }
    }
}
