using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.Web.Routing;

namespace gradeprogram.Service
{
    public class DropDownListHelper
    {
        public static string GetDropdownList(string name, IDictionary<string, string> optionData, object htmlAttributes, string defaultSelectValue, bool appendOptionLabel, string optionLabel)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name", "產生DropDownList時 tag Name 不得為空");
            }
            TagBuilder select = new TagBuilder("select");
            select.Attributes.Add("id", name);
            StringBuilder renderHtmlTag = new StringBuilder();
            IDictionary<string, string> newOptionData = new Dictionary<string, string>();
            if (appendOptionLabel)
            {
                newOptionData.Add(new KeyValuePair<string, string>(optionLabel ?? "請選擇", ""));
            }
            foreach (var item in optionData)
            {
                newOptionData.Add(item);
            }
            foreach (var option in newOptionData)
            {
                TagBuilder optionTag = new TagBuilder("option");
                optionTag.Attributes.Add("value", option.Value);
                if (!string.IsNullOrEmpty(defaultSelectValue) && defaultSelectValue.Equals(option.Value))
                {
                    optionTag.Attributes.Add("selected", "selected");
                }
                optionTag.SetInnerText(option.Key);
                renderHtmlTag.AppendLine(optionTag.ToString(TagRenderMode.Normal));
            }
            select.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            select.InnerHtml = renderHtmlTag.ToString();
            return select.ToString();
        }
    }
}