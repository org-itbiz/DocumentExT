using Makersn.BizDac;
using Makersn.Models;
using Net.Common.Func;
using Net.Common.Helper;
using Net.Common.Model;
using Net.WebUI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Net.WebUI.Controllers
{
    public class DesignController : BaseController
    {
        private ArticleDac _articleDac = new ArticleDac();
        private ArticleFileDac _articleFileDac = new ArticleFileDac();

        public ActionResult Index()
        {
            return View();
        }

        #region 디자인 업로드
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Upload()
        {
            //ViewBag.Temp = profileModel.UserNo + "_" + new DateTimeHelper().ConvertToUnixTime(DateTime.Now);
            ViewBag.Temp = "1_" + new DateTimeHelper().ConvertToUnixTime(DateTime.Now);

            return View();
        }

        [HttpPost]
        public JsonResult Upload(FormCollection collection)
        {
            AjaxResponseModel response = new AjaxResponseModel();
            response.Success = false;

            int articleNo = 0;

            string paramNo = collection["No"];
            string temp = collection["temp"];
            string mode = collection["mode"];
            int mainImg = Convert.ToInt32(collection["main_img"]);
            string articleTitle = collection["article_title"];
            string articleContents = collection["article_contents"];
            int codeNo = Convert.ToInt32(collection["lv1"]);
            int copyright = Convert.ToInt32(collection["copyright"]);
            string delNo = collection["del_no"];
            string VideoSource = collection["insertVideoSource"];

            string tags = collection["article_tags"];

            var mulltiSeq = collection["multi[]"];
            string[] seqArray = mulltiSeq.Split(',');

            if (tags.Length > 1000)
            {
                response.Message = "태그는 1000자 이하로 가능합니다.";
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            ArticleT articleT = null;

            if (!Int32.TryParse(paramNo, out articleNo))
            {
                response.Success = false;
                response.Message = "pk error";
            }

            if (articleNo > 0)
            {
                //update
                articleT = _articleDac.GetArticleByNo(articleNo);

                if (articleT != null)
                {
                    if (articleT.MemberNo == profileModel.UserNo && articleT.Temp == temp)
                    {
                        articleT.UpdDt = DateTime.Now;
                        articleT.UpdId = profileModel.UserId;
                        articleT.RegIp = IPAddressHelper.GetIP(this);
                    }
                }
            }
            else
            {
                //save
                articleT = new ArticleT();
                articleT.MemberNo = profileModel.UserNo;
                //태그 #**
                articleT.Tag = tags;
                articleT.Temp = temp;
                articleT.ViewCnt = 0;
                articleT.RegDt = DateTime.Now;
                articleT.RegId = profileModel.UserId;
                articleT.RegIp = IPAddressHelper.GetIP(this);
                articleT.RecommendYn = "N";
                articleT.RecommendDt = null;
            }

            if (articleT != null)
            {
                articleT.Title = articleTitle;
                articleT.Contents = articleContents;
                articleT.Visibility = mode == "upload" ? "Y" : "N";
                articleT.Copyright = copyright;
                articleT.CodeNo = codeNo;
                articleT.MainImage = mainImg;

                articleT.VideoUrl = VideoSource;

                articleT.Tag = tags;



                //articleNo = _articleDac.SaveOrUpdate(articleT, delNo);

                response.Success = true;
                response.Result = articleNo.ToString(); ;
            }

            //_articleFileDac.UpdateArticleFileSeq(seqArray);


            return Json(response, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 업로드된 파일 리스트
        /// <summary>
        /// 업로드된 파일 리스트
        /// </summary>
        /// <param name="temp"></param>
        /// <returns></returns>
        [HttpPost]
        public PartialViewResult UploadFilesView(FormCollection collection)
        {
            string paramNo = collection["No"];
            string uploadCnt = collection["uploadCnt"];
            string temp = collection["temp"];
            string mode = collection["mode"];
            string delNo = collection["del_no"];

            ViewBag.UploadCnt = uploadCnt;

            IList<ArticleFileT> fileLst = new List<ArticleFileT>();

            if (mode == "edit")
            {
                fileLst = _articleFileDac.GetFileList(int.Parse(paramNo));
            }
            else
            {
                fileLst = _articleFileDac.GetArticleFilesByTemp(temp);
            }

            if (!string.IsNullOrEmpty(delNo))
            {
                string[] delNos = delNo.Split(',');

                IList<ArticleFileT> files = fileLst.Where(n => delNos.Contains(n.No.ToString())).ToList();

                fileLst = fileLst.Except(files).ToList();
            }

            return PartialView(fileLst);
        }
        #endregion

        #region AppendFile
        /// <summary>
        /// 
        /// </summary>
        /// <param name="no"></param>
        /// <param name="idx"></param>
        /// <returns></returns>
        public PartialViewResult AppendFile(int no, int idx)
        {
            ArticleFileT file = _articleFileDac.GetArticleFileByNo(no);
            ViewBag.Index = idx;
            return PartialView(file);
        }
        #endregion

        #region stl upload
        /// <summary>
        /// stl upload
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult StlUpload(HttpPostedFileBase file, string temp, string fileIdx)
        {
            AjaxResponseModel response = new AjaxResponseModel();
            response.Success = false;
            string fileName = string.Empty;

            //HttpPostedFileBase stlupload = Request.Files["stlupload"];

            if (file != null)
            {
                if (file.ContentLength > 0)
                {
                    if (file.ContentLength < 200 * 1024 * 1024)
                    {
                        string[] extType = { ".stl", ".obj" };

                        string extension = Path.GetExtension(file.FileName).ToLower();

                        if (extType.Contains(extension))
                        {
                            string save3DFolder = "Article/article_3d";
                            string saveJSFolder = "Article/article_js";
                            fileName = new UploadFunc().FileUpload(file, null, save3DFolder, null);

                            string file3Dpath = string.Format("\\\\localhost\\fileupload\\{0}\\", save3DFolder);
                            string fileJSpath = string.Format("\\\\localhost\\fileupload\\{0}\\", saveJSFolder);

                            Object3DModel _3dModel = new Modeling3DHelper().Get3DModel(file3Dpath + fileName, extension);

                            //ArticleFileT sizeResult = GetSizeFor3DFile(file3Dpath + fileName, extension);

                            var jsonBuff = JsonConvert.SerializeObject(_3dModel);

                            string jsFileNm = fileJSpath + fileName + ".json";

                            new FileHelper().FileWriteAllText(jsFileNm, jsonBuff);

                            ArticleFileT articleFileT = new ArticleFileT();

                            articleFileT.FileGubun = "temp";
                            articleFileT.FileType = "stl";
                            //articleFileT.MemberNo = profileModel.UserNo;
                            articleFileT.MemberNo = 1;
                            articleFileT.Seq = 5000;
                            articleFileT.ImgUseYn = "N";
                            articleFileT.Ext = extension;
                            articleFileT.ThumbYn = "N";
                            articleFileT.MimeType = file.ContentType;
                            articleFileT.Name = file.FileName;
                            articleFileT.Size = file.ContentLength.ToString();
                            articleFileT.Rename = fileName;
                            articleFileT.Path = string.Format("/{0}/", save3DFolder);

                            articleFileT.X = 0;
                            articleFileT.Y = 0;
                            articleFileT.Z = 0;
                            articleFileT.Volume = 0;

                            articleFileT.UseYn = "Y";
                            articleFileT.Temp = temp;
                            articleFileT.RegIp = IPAddressHelper.GetIP(this);
                            articleFileT.RegId = profileModel.UserId;
                            articleFileT.RegDt = DateTime.Now;

                            int articleFileNo = _articleFileDac.InsertArticleFile(articleFileT);

                            response.Success = true;
                            response.Result = articleFileNo.ToString();
                        }
                        else
                        {
                            response.Message = "stl, obj 형식 파일만 가능합니다.";
                        }
                    }
                    else
                    {
                        response.Message = "최대 사이즈 200MB 파일만 가능합니다.";
                    }
                }
            }

            if (!string.IsNullOrEmpty(fileIdx))
            {
                string[] idxArr = fileIdx.Split(',');
                if (idxArr.Length > 1)
                {
                    _articleFileDac.UpdateArticleFileSeq(idxArr);
                }
            }


            return Json(response, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region img upload
        /// <summary>
        /// img upload
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        //public JsonResult ImgUpload(FormCollection collection)
        public JsonResult ImgUpload(HttpPostedFileBase file, string temp, string fileIdx)
        {
            AjaxResponseModel response = new AjaxResponseModel();
            response.Success = false;
            string fileName = string.Empty;

            //HttpPostedFileBase imgupload = Request.Files["file"];

            if (file != null)
            {
                if (file.ContentLength > 0)
                {
                    string[] extType = { ".jpg", ".png", ".gif" };

                    string extension = Path.GetExtension(file.FileName).ToLower();

                    if (extType.Contains(extension))
                    {
                        fileName = new UploadFunc().FileUpload(file, ImageSize.GetArticleResize(), "Article", null);

                        ArticleFileT articleFileT = new ArticleFileT();

                        articleFileT.FileGubun = "temp";
                        articleFileT.FileType = "img";
                        //articleFileT.MemberNo = profileModel.UserNo;
                        articleFileT.MemberNo = 1;
                        articleFileT.Seq = 5000;
                        articleFileT.ImgUseYn = "U";
                        articleFileT.Ext = extension;
                        articleFileT.ThumbYn = "Y";
                        articleFileT.MimeType = file.ContentType;
                        articleFileT.Name = file.FileName;
                        articleFileT.Size = file.ContentLength.ToString();
                        articleFileT.Rename = fileName;
                        articleFileT.Path = "/Article/article_img/";

                        articleFileT.Width = "630";
                        articleFileT.Height = "470";

                        articleFileT.UseYn = "Y";
                        articleFileT.Temp = temp;
                        articleFileT.RegIp = IPAddressHelper.GetIP(this);
                        articleFileT.RegId = profileModel.UserId;
                        articleFileT.RegDt = DateTime.Now;

                        int articleFileNo = _articleFileDac.InsertArticleFile(articleFileT);

                        response.Success = true;
                        response.Result = articleFileNo.ToString();
                    }
                    else
                    {
                        response.Message = "gif, jpg, png 형식 파일만 가능합니다.";
                    }
                }
            }

            if (!string.IsNullOrEmpty(fileIdx))
            {
                string[] idxArr = fileIdx.Split(',');
                if (idxArr.Length > 1)
                {
                    _articleFileDac.UpdateArticleFileSeq(idxArr);
                }
            }

            return Json(response, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 이미지 캡쳐
        /// <summary>
        /// capture
        /// </summary>
        /// <param name="stl_val"></param>
        /// <param name="stl_img_no"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult ImgCapture(string stl_val, int stl_img_no)
        {
            AjaxResponseModel response = new AjaxResponseModel();
            response.Success = false;

            string saveImgFolder = "Article/article_img";

            string fileImgpath = string.Format("\\\\localhost\\fileupload\\{0}\\", saveImgFolder);

            if (new FileHelper().FilePathCreater(fileImgpath))
            {

                if (!string.IsNullOrEmpty(stl_val) && stl_img_no != 0)
                {
                    ArticleFileT file = _articleFileDac.GetArticleFileByNo(stl_img_no);

                    string fileNm = file.Rename + ".jpg";

                    using (FileStream fs = new FileStream(fileImgpath + fileNm, FileMode.Create, FileAccess.Write))
                    {
                        using (BinaryWriter bw = new BinaryWriter(fs))
                        {
                            byte[] data = Convert.FromBase64String(stl_val.Replace("data:image/png;base64,", ""));
                            bw.Write(data);
                            bw.Close();
                        }
                        fs.Close();
                    }

                    file.ImgName = fileNm;

                    response.Success = _articleFileDac.UpdateArticleFile(file);
                    response.Result = file.No.ToString();
                }
            }

            response.Success = true;

            return Json(response, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
