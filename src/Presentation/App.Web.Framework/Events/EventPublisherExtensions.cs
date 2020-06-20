using App.Services.Events;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Web.Framework.Events
{
    /// <summary>
    /// Represents event publisher extensions
    /// </summary>
    public static class EventPublisherExtensions
    {
        /// <summary>
        /// Publish ModelPrepared event
        /// </summary>
        /// <typeparam name="T">Type of the model</typeparam>
        /// <param name="eventPublisher">Event publisher</param>
        /// <param name="model">Model</param>
        public static void ModelPrepared<T>(this IEventPublisher eventPublisher, T model)
        {
            eventPublisher.Publish(new ModelPreparedEvent<T>(model));
        }

        /// <summary>
        /// Publish ModelReceived event
        /// </summary>
        /// <typeparam name="T">Type of the model</typeparam>
        /// <param name="eventPublisher">Event publisher</param>
        /// <param name="model">Model</param>
        /// <param name="modelState">Model state</param>
        public static void ModelReceived<T>(this IEventPublisher eventPublisher, T model, ModelStateDictionary modelState)
        {
            eventPublisher.Publish(new ModelReceivedEvent<T>(model, modelState));
        }
    }
}
