using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace UserSysCore.TagHelpers
{
    [HtmlTargetElement("checkBoxList")]
    public class CheckBoxListTagHelper : TagHelper
    {
        public IList<CheckBoxItem> Items { set; get; }
        public string Name { set; get; }
        public string Id { set; get; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "checkbox";
            if (Items != null)
            {
                int i = 0; 
                foreach(var item in Items)
                {
                    string checkBoxId = Id + "_" + i;
                    var checkBox = new TagBuilder("input")
                    {
                        TagRenderMode = TagRenderMode.SelfClosing
                    };
                    checkBox.Attributes.Add("id", checkBoxId);
                    checkBox.Attributes.Add("name", Name);
                    if (item.Checked)
                    {
                        checkBox.Attributes.Add("checked", "checked");
                    }
                    if (item.Disabled)
                    {
                        checkBox.Attributes.Add("disabled", "disabled");
                    }
                    checkBox.Attributes.Add("type", "checkbox");
                    checkBox.Attributes.Add("value", item.Value);
                    checkBox.Attributes.Add("title", item.Text);
                    

                    output.Content.AppendHtml(checkBox);
                    var label = new TagBuilder("label");
                    label.Attributes.Add("for", checkBoxId);
                    label.InnerHtml.Append(item.Text);
                    output.Content.AppendHtml(label);
                    i++;
                }
            }
            base.Process(context, output);
        }
    }
    /// <summary>
    /// 复选框项
    /// </summary>
    public class CheckBoxItem
    {   
        /// <summary>
        /// 复选框值
        /// </summary>
        public string Value { set; get; }
        /// <summary>
        /// 复选框文本内容
        /// </summary>
        public string Text { set; get; }
        /// <summary>
        /// 是否选中
        /// </summary>
        public bool Checked { set; get; }
        /// <summary>
        /// 是否可用
        /// </summary>
        public bool Disabled { set; get; }
    }
}
