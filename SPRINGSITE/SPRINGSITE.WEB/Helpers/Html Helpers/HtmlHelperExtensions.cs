using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SPRINGSITE.WEB;


public static class HtmlHelperExtensions
{
    // for rendering title
    public static IHtmlString RenderTitle(this HtmlHelper htmlHelper)
    {
        var title = String.Empty;

        var viewBagTitle = String.IsNullOrEmpty(htmlHelper.ViewContext.ViewBag.Title) ? null : htmlHelper.ViewContext.ViewBag.Title;
        if (viewBagTitle != null)
        {
            title = viewBagTitle;
            htmlHelper.ViewContext.ViewBag.Title = null;
        }

        return MvcHtmlString.Create(title);
    }

    // for rendering breadcrumb
    public static IHtmlString RenderBreadcrumb(this HtmlHelper htmlHelper)
    {
        var breadcrumb = String.Empty;

        var viewBagBreadcrumb = String.IsNullOrEmpty(htmlHelper.ViewContext.ViewBag.Breadcrumb) ? null : htmlHelper.ViewContext.ViewBag.Breadcrumb;
        if (viewBagBreadcrumb != null)
        {
            breadcrumb += "<span>" + viewBagBreadcrumb + "</span>";

            breadcrumb = viewBagBreadcrumb;
            htmlHelper.ViewContext.ViewBag.Breadcrumb = null;
        }

        return MvcHtmlString.Create(breadcrumb);
    }

    // for rendering message
    public static IHtmlString RenderMessages(this HtmlHelper htmlHelper)
    {
        var messages = String.Empty;
        foreach (var messageType in Enum.GetNames(typeof(MessageType)))
        {
            var message = htmlHelper.ViewContext.ViewData.ContainsKey(messageType)
                            ? htmlHelper.ViewContext.ViewData[messageType]
                            : htmlHelper.ViewContext.TempData.ContainsKey(messageType)
                                ? htmlHelper.ViewContext.TempData[messageType]
                                : null;
            if (message != null)
            {
                messages += "<div class='notification " + messageType.ToString().ToLower() + "'><p><strong>" + messageType.ToString() + " : </strong>" + message + "</p></div>";
            }
        }

        return MvcHtmlString.Create(messages);
    }

}
