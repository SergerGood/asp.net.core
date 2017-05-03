﻿using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;
using System.Text.Encodings.Web;

namespace ASP.NET.Sample.Web.Util
{
    public static class ListHelper
    {
        public static HtmlString CreateList(this IHtmlHelper html, string[] items)
        {
            TagBuilder ul = new TagBuilder("ul");

            foreach (string item in items)
            {
                TagBuilder li = new TagBuilder("li");
                li.InnerHtml.Append(item);
                ul.InnerHtml.AppendHtml(li);
            }
            ul.Attributes.Add("class", "itemsList");

            var writer = new StringWriter();
            ul.WriteTo(writer, HtmlEncoder.Default);

            return new HtmlString(writer.ToString());
        }
    }
}
