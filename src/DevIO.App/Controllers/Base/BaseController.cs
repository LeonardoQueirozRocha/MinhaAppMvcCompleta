using DevIO.Business.Interfaces.Notifications;
using Microsoft.AspNetCore.Mvc;

namespace DevIO.App.Controllers.Base
{
    public abstract class BaseController : Controller
    {
        private readonly INotifier _notifier;

        protected BaseController(INotifier notifier) => _notifier = notifier;

        protected bool IsValid() => !_notifier.HaveNotification();
    }
}
