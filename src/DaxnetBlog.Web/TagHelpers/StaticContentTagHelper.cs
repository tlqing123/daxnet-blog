﻿using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace DaxnetBlog.Web.TagHelpers
{
    [HtmlTargetElement("static-content", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class StaticContentTagHelper : TagHelper
    {
        private readonly IHtmlHelper htmlHelper;

        public StaticContentTagHelper(IHtmlHelper htmlHelper)
        {
            this.htmlHelper = htmlHelper;
            this.Style = "primary";
        }

        [HtmlAttributeName("sc-title")]
        public string Title { get; set; }

        [HtmlAttributeName("sc-style")]
        public string Style { get; set; }

        

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var tagBuilder = new TagBuilder("div");
            tagBuilder.AddCssClass("panel");
            tagBuilder.AddCssClass($"panel-{Style}");

            
            var headerTagBuilder = new TagBuilder("div");
            headerTagBuilder.AddCssClass("panel-heading");
            headerTagBuilder.InnerHtml.Append(this.Title);

            var bodyContent = (await output.GetChildContentAsync()).GetContent();
            var bodyTagBuilder = new TagBuilder("div");
            bodyTagBuilder.AddCssClass("panel-body");
            bodyTagBuilder.InnerHtml.AppendHtml(this.htmlHelper.Raw(bodyContent).ToString());

            tagBuilder.InnerHtml.AppendHtml(headerTagBuilder.ToHtmlString());
            tagBuilder.InnerHtml.AppendHtml(bodyTagBuilder.ToHtmlString());

            output.TagMode = TagMode.StartTagAndEndTag;
            output.Content.AppendHtml(tagBuilder.ToHtmlString());
        }
    }
}
