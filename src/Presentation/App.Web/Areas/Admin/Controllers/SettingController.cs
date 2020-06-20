using System;
using System.Net;
using App.Core;
using App.Core.Infrastructure;
using App.Services.Configuration;
using App.Services.Security;
using App.Web.Areas.Admin.Factories;
using App.Web.Areas.Admin.Models.Settings;
using App.Web.Framework.Mvc;
using App.Web.Framework.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;

namespace App.Web.Areas.Admin.Controllers
{
    public partial class SettingController : BaseAdminController
    {
        #region Fields

        private readonly IEncryptionService _encryptionService;
        private readonly IAppFileProvider _fileProvider;
        private readonly IPermissionService _permissionService;
        private readonly ISettingModelFactory _settingModelFactory;
        private readonly ISettingService _settingService;
        private readonly IWorkContext _workContext;
        
        #endregion

        #region Ctor

        public SettingController(IEncryptionService encryptionService,
            IAppFileProvider fileProvider,
            IPermissionService permissionService,
            ISettingModelFactory settingModelFactory,
            ISettingService settingService,
            IWorkContext workContext)
        {
            _encryptionService = encryptionService;
            _fileProvider = fileProvider;
            _permissionService = permissionService;
            _settingModelFactory = settingModelFactory;
            _settingService = settingService;
            _workContext = workContext;
        }

        #endregion

        #region Methods

        public virtual IActionResult AllSettings(string settingName)
        {
            if (!_permissionService.Authorize(StandardPermission.ManageSettings))
                return AccessDeniedView();

            //prepare model
            var model = _settingModelFactory.PrepareSettingSearchModel(new SettingSearchModel() { SearchSettingName = WebUtility.HtmlEncode(settingName) });

            return View(model);
        }

        [HttpPost]
        public virtual IActionResult AllSettings(SettingSearchModel searchModel)
        {
            if (!_permissionService.Authorize(StandardPermission.ManageSettings))
                return AccessDeniedDataTablesJson();

            //prepare model
            var model = _settingModelFactory.PrepareSettingListModel(searchModel);

            return Json(model);
        }

        [HttpPost]
        public virtual IActionResult SettingUpdate(SettingModel model)
        {
            if (!_permissionService.Authorize(StandardPermission.ManageSettings))
                return AccessDeniedView();

            if (model.Name != null)
                model.Name = model.Name.Trim();

            if (model.Value != null)
                model.Value = model.Value.Trim();

            if (!ModelState.IsValid)
                return ErrorJson(ModelState.SerializeErrors());

            //try to get a setting with the specified id
            var setting = _settingService.GetSettingById(model.Id)
                ?? throw new ArgumentException("No setting found with the specified id");

            if (!setting.Name.Equals(model.Name, StringComparison.InvariantCultureIgnoreCase))
            {
                //setting name has been changed
                _settingService.DeleteSetting(setting);
            }
            _settingService.SetSetting(model.Name, model.Value);

            return new NullJsonResult();
        }

        [HttpPost]
        public virtual IActionResult SettingAdd(SettingModel model)
        {
            if (!_permissionService.Authorize(StandardPermission.ManageSettings))
                return AccessDeniedView();

            if (model.Name != null)
                model.Name = model.Name.Trim();

            if (model.Value != null)
                model.Value = model.Value.Trim();

            if (!ModelState.IsValid)
                return ErrorJson(ModelState.SerializeErrors());

            _settingService.SetSetting(model.Name, model.Value);

            
            return Json(new { Result = true });
        }

        [HttpPost]
        public virtual IActionResult SettingDelete(int id)
        {
            if (!_permissionService.Authorize(StandardPermission.ManageSettings))
                return AccessDeniedView();

            //try to get a setting with the specified id
            var setting = _settingService.GetSettingById(id)
                ?? throw new ArgumentException("No setting found with the specified id", nameof(id));

            _settingService.DeleteSetting(setting);

            return new NullJsonResult();
        }

        #endregion
    }
}