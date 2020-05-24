using App.Core.Domain.Logging;
using App.Services;
using App.Services.Logging;
using App.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using App.Web.Areas.Admin.Models.Logging;
using App.Web.Framework.Models.Extensions;
using System;
using System.Linq;

namespace App.Web.Areas.Admin.Factories
{
    /// <summary>
    /// Represents the log model factory implementation
    /// </summary>
    public partial class LogModelFactory : ILogModelFactory
    {
        #region Fields

        private readonly ILogger _logger;

        #endregion

        #region Ctor

        public LogModelFactory(ILogger logger)
        {
            _logger = logger;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Prepare log search model
        /// </summary>
        /// <param name="searchModel">Log search model</param>
        /// <returns>Log search model</returns>
        public virtual LogSearchModel PrepareLogSearchModel(LogSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //prepare available log levels
            var availableLogLevelItems = LogLevel.Debug.ToSelectList(false);
            foreach (var logLevelItem in availableLogLevelItems)
            {
                searchModel.AvailableLogLevels.Add(logLevelItem);
            }

            return searchModel;
        }

        /// <summary>
        /// Prepare paged log list model
        /// </summary>
        /// <param name="searchModel">Log search model</param>
        /// <returns>Log list model</returns>
        public virtual LogListModel PrepareLogListModel(LogSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get parameters to filter log
            var createdOnFromValue = searchModel.CreatedOnFrom.HasValue
                ? (DateTime?)searchModel.CreatedOnFrom.Value : null;
            var createdToFromValue = searchModel.CreatedOnTo.HasValue
                ? (DateTime?)searchModel.CreatedOnTo.Value : null;
            var logLevel = searchModel.LogLevelId > 0 ? (LogLevel?)searchModel.LogLevelId : null;

            //get log
            var logItems = _logger.GetAllLogs(message: searchModel.Message,
                fromUtc: createdOnFromValue,
                toUtc: createdToFromValue,
                logLevel: logLevel,
                pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);

            //prepare list model
            var model = new LogListModel().PrepareToGrid(searchModel, logItems, () =>
            {
                //fill in model values from the entity
                return logItems.Select(logItem =>
                {
                    //fill in model values from the entity
                    var logModel = logItem.ToModel<LogModel>();

                    //convert dates to the user time
                    logModel.CreatedOn = logItem.CreatedOnUtc;

                    //fill in additional values (not existing in the entity)
                    logModel.LogLevel = logItem.LogLevel.ToString();
                    logModel.ShortMessage = logItem.ShortMessage;
                    logModel.FullMessage = string.Empty;
                    logModel.UserEmail = logItem.User?.Email ?? string.Empty;

                    return logModel;
                });
            });

            return model;
        }

        /// <summary>
        /// Prepare log model
        /// </summary>
        /// <param name="model">Log model</param>
        /// <param name="log">Log</param>
        /// <param name="excludeProperties">Whether to exclude populating of some properties of model</param>
        /// <returns>Log model</returns>
        public virtual LogModel PrepareLogModel(LogModel model, Log log, bool excludeProperties = false)
        {
            if (log != null)
            {
                //fill in model values from the entity
                if (model == null)
                {
                    model = log.ToModel<LogModel>();

                    model.LogLevel = log.LogLevel.ToString();
                    model.ShortMessage = log.ShortMessage.ToString();
                    model.FullMessage = log.FullMessage.ToString();
                    model.CreatedOn = log.CreatedOnUtc;
                    model.UserEmail = log.User?.Email ?? string.Empty;
                }
            }
            return model;
        }

        #endregion
    }
}
