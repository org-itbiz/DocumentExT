using log4net;
using Makersn.BizDac;
using Makersn.Models;
using Net.Common.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Net.WebUI.Controllers
{
    public class HomeController : BaseController
    {
        ArticleDac _articleDac = new ArticleDac();
        ArticleFileDac _articleFileDac = new ArticleFileDac();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Viewer(string no = "", string goReply = "N")
        {
            int articleNo = 0;
            var visitorNo = profileModel.UserNo;
            ViewBag.GoReply = goReply;
            ArticleDetailT detail = new ArticleDetailT();
            if (Int32.TryParse(no, out articleNo))
            {
                //조회수 증가 방지
                if (Request.Cookies[no] == null)
                {
                    Response.Cookies[no].Value = no;
                    Response.Cookies[no].Expires = DateTime.Now.AddDays(1);

                    //뷰 업데이트
                }

                detail = _articleDac.GetArticleDetailByArticleNo(articleNo, visitorNo);


                ViewBag.MetaDescription = detail.Contents;

                detail.Contents = new HtmlFilter().PunctuationEncode(detail.Contents);

                detail.Contents = new HtmlFilter().ConvertContent(detail.Contents);
                if ((detail.MemberNo != visitorNo && profileModel.UserLevel < 50) && detail.Visibility.ToUpper() == "N")
                {
                    return Content("<script>alert('비공개 처리된 게시물 입니다.'); location.href='/';</script>");
                }

                ViewBag.chkStlCnt = _articleFileDac.GetSTLFileList(articleNo).Count;
                ViewBag.Files = _articleFileDac.GetFileList(articleNo);
                ViewBag.MainImg = detail.MainImgName;
                ViewBag.ListCnt = 5;
                ViewBag.ListList = null; //리스트 이름 목록
            }
            else
            {

            }

            ViewBag.VisitorNo = visitorNo;

            ViewBag.No = no;
            ViewBag.CodeNo = detail.CodeNo;

            ViewBag.Class = "bdB mgB15";
            ViewBag.WrapClass = "bgW";

            return View(detail);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="no"></param>
        /// <param name="goReply"></param>
        /// <returns></returns>
        public ActionResult Viewer2(string no = "", string goReply = "N")
        {
            //ILog log = LogManager.GetLogger("APP");
            //log.Info("Viewer2_inof");
            //log.Warn("Viewer2_err");

            int articleNo = 0;
            var visitorNo = profileModel.UserNo;
            ViewBag.GoReply = goReply;
            ArticleDetailT detail = new ArticleDetailT();
            if (Int32.TryParse(no, out articleNo))
            {
                //조회수 증가 방지
                if (Request.Cookies[no] == null)
                {
                    Response.Cookies[no].Value = no;
                    Response.Cookies[no].Expires = DateTime.Now.AddDays(1);

                    //뷰 업데이트
                }

                detail = _articleDac.GetArticleDetailByArticleNo(articleNo, visitorNo);


                ViewBag.MetaDescription = detail.Contents;

                detail.Contents = new HtmlFilter().PunctuationEncode(detail.Contents);

                detail.Contents = new HtmlFilter().ConvertContent(detail.Contents);
                if ((detail.MemberNo != visitorNo && profileModel.UserLevel < 50) && detail.Visibility.ToUpper() == "N")
                {
                    return Content("<script>alert('비공개 처리된 게시물 입니다.'); location.href='/';</script>");
                }

                ViewBag.chkStlCnt = _articleFileDac.GetSTLFileList(articleNo).Count;
                ViewBag.Files = _articleFileDac.GetFileList(articleNo);
                ViewBag.MainImg = detail.MainImgName;
                ViewBag.ListCnt = 5;
                ViewBag.ListList = null; //리스트 이름 목록
            }
            else
            {

            }

            ViewBag.VisitorNo = visitorNo;

            ViewBag.No = no;
            ViewBag.CodeNo = detail.CodeNo;

            ViewBag.Class = "bdB mgB15";
            ViewBag.WrapClass = "bgW";

            return View(detail);
        }
    }
}
