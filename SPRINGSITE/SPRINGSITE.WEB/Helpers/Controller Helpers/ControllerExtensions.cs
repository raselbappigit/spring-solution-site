﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SPRINGSITE.WEB.Helpers
{
    public static class ControllerExtensions
    {
        public static void ShowTitle(this Controller controller, string title)
        {
            controller.ViewBag.Title = title;
        }

        //breadcrumb
        public static void ShowBreadcrumb(this Controller controller, string contrl, string action)
        {
            string breadcrumbLink = string.Empty;

            //breadcrumbLink += @"<a href='/" + contrl + "/" + action + "'>" + contrl + " > " + action + "</a>";
            breadcrumbLink += @" <span><a href='/" + contrl + "'>" + contrl + "</a> > <a href='/" + contrl + "/" + action + "'>" + action + "</a></span> ";

            controller.ViewBag.Breadcrumb = breadcrumbLink;
        }

        public static void ShowMessage(this Controller controller, string message, MessageType messageType = MessageType.Information, bool showAfterRedirect = true)
        {
            var messageTypeKey = messageType.ToString();
            if (showAfterRedirect)
            {
                controller.TempData[messageTypeKey] = message;
            }
            else
            {
                controller.ViewData[messageTypeKey] = message;
            }
        }

    }
}