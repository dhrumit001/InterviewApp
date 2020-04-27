using App.Services.Logging;
using App.Services.Security;
using App.Web.Areas.Admin.Factories;
using App.Web.Areas.Admin.Models.Logging;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace App.Web.Areas.Admin.Controllers
{
    public class LogController : BaseAdminController
    {
        #region Fields

        private readonly ILogger _logger;
        private readonly ILogModelFactory _logModelFactory;
        private readonly IPermissionService _permissionService;

        #endregion

        #region Ctor

        public LogController(ILogger logger,
            ILogModelFactory logModelFactory,
            IPermissionService permissionService)
        {
            _logger = logger;
            _logModelFactory = logModelFactory;
            _permissionService = permissionService;
        }

        #endregion

        #region Methods

        public virtual IActionResult Index()
        {
            return RedirectToAction("List");
        }

        public virtual IActionResult List()
        {
            if (!_permissionService.Authorize(StandardPermission.ManageSystemLog))
                return AccessDeniedView();

            //prepare model
            var model = _logModelFactory.PrepareLogSearchModel(new LogSearchModel());

            return View(model);
        }

        [HttpPost]
        public virtual IActionResult LogList(LogSearchModel searchModel)
        {
            //if (!_permissionService.Authorize(StandardPermission.ManageSystemLog))
            //    return AccessDeniedDataTablesJson();

            //prepare model
            var model = _logModelFactory.PrepareLogListModel(searchModel);

            return Json(model);
        }

        [HttpPost, ActionName("List")]
        public virtual IActionResult ClearAll()
        {
            if (!_permissionService.Authorize(StandardPermission.ManageSystemLog))
                return AccessDeniedView();

            _logger.ClearLog();

            //activity log
            //_customerActivityService.InsertActivity("DeleteSystemLog", _localizationService.GetResource("ActivityLog.DeleteSystemLog"));

            //_notificationService.SuccessNotification(_localizationService.GetResource("Admin.System.Log.Cleared"));

            return RedirectToAction("List");
        }

        public virtual IActionResult View(int id)
        {
            if (!_permissionService.Authorize(StandardPermission.ManageSystemLog))
                return AccessDeniedView();

            //try to get a log with the specified id
            var log = _logger.GetLogById(id);
            if (log == null)
                return RedirectToAction("List");

            //prepare model
            var model = _logModelFactory.PrepareLogModel(null, log);

            return View(model);
        }

        [HttpPost]
        public virtual IActionResult Delete(int id)
        {
            if (!_permissionService.Authorize(StandardPermission.ManageSystemLog))
                return AccessDeniedView();

            //try to get a log with the specified id
            var log = _logger.GetLogById(id);
            if (log == null)
                return RedirectToAction("List");

            _logger.DeleteLog(log);

            //activity log
            //_customerActivityService.InsertActivity("DeleteSystemLog", _localizationService.GetResource("ActivityLog.DeleteSystemLog"), log);

            //_notificationService.SuccessNotification(_localizationService.GetResource("Admin.System.Log.Deleted"));

            return RedirectToAction("List");
        }

        [HttpPost]
        public virtual IActionResult DeleteSelected(ICollection<int> selectedIds)
        {
            if (!_permissionService.Authorize(StandardPermission.ManageSystemLog))
                return AccessDeniedView();

            if (selectedIds != null)
                _logger.DeleteLogs(_logger.GetLogByIds(selectedIds.ToArray()).ToList());

            //activity log
            //_customerActivityService.InsertActivity("DeleteSystemLog", _localizationService.GetResource("ActivityLog.DeleteSystemLog"));

            return Json(new { Result = true });
        }

        #endregion
    }
}