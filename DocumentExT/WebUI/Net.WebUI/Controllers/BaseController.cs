using Makersn.BizDac;
using Net.WebUI.Configurations;
using Net.WebUI.Models;
using Newtonsoft.Json;
using System.Web.Mvc;

namespace Net.WebUI.Controllers
{
    public class BaseController : Controller
    {
        public ApplicationConfiguration instance = ApplicationConfiguration.Instance;

        public ProfileModel profileModel;


        public BaseController()
        {
            profileModel = Profile;

            ViewBag.LogOnMemner = profileModel;
            ViewBag.LogOnChk = profileModel.UserNo == 0 ? 0 : 1;

            ViewBag.ProfileImgUrl = instance.ProfileImgUrl;
            ViewBag.ArticleImgUrl = instance.ArticleImgUrl;
            ViewBag.Article3DUrl = instance.Article3DUrl;
            ViewBag.ArticleJsUrl = instance.Article3DJsUrl;
            ViewBag.AdminImgUrl = instance.AdminImgUrl;
            ViewBag.PrintImgUrl = instance.PrinterImgUrl;

            ViewBag.CurrentDomain = instance.CurrentDomain;
            ViewBag.TargetDomain = instance.TargetDomain;
            ViewBag.LangFlag = System.Configuration.ConfigurationManager.AppSettings["LangFlag"];
            ViewBag.LangFlagName = ViewBag.LangFlag == "KR" ? "한국어" : ViewBag.LangFlag == "EN" ? "English" : "";

            ViewBag.IsMain = "N";

        }

        private ProfileModel Profile
        {
            get
            {
                var user = System.Web.HttpContext.Current.User;

                profileModel = new ProfileModel();

                if (user.Identity.IsAuthenticated)
                {
                    profileModel = JsonConvert.DeserializeObject<ProfileModel>(user.Identity.Name);
                }
                else
                {
                    profileModel.UserNo = 0;
                    profileModel.UserNm = "손님";
                    profileModel.UserId = "anonymous";
                    profileModel.UserProfilePic = "";
                    profileModel.UserLevel = 0;
                }

                return profileModel;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterContext"></param>
        //protected override void OnActionExecuting(ActionExecutingContext filterContext)
        //{
        //    //todo
        //    base.OnActionExecuting(filterContext);
        //}
    }
}
