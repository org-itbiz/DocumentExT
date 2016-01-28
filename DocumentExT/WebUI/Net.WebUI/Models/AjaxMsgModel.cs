using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Net.WebUI.Models
{
    /// <summary>
    /// http://www.cnblogs.com/jiekzou/p/5137852.html
    /// </summary>
    public class AjaxMsgModel
    {
        public string Msg { get; set; }
        /// <summary>
        /// OK,ERROR
        /// </summary>
        public string Status { get; set; }
        public string BackUrl { get; set; }
        /// <summary>
        /// object
        /// </summary>
        public object Data { get; set; }
    }
}