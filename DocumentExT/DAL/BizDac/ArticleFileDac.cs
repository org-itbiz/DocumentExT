using Makersn.Models;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makersn.BizDac
{
    public class ArticleFileDac
    {
        #region 파일 리스트
        /// <summary>
        /// 파일 리스트
        /// </summary>
        /// <param name="no"></param>
        /// <returns></returns>
        public IList<ArticleFileT> GetFileList(int articleNo)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                IList<ArticleFileT> files = session.QueryOver<ArticleFileT>().Where(w => w.ArticleNo == articleNo && w.FileGubun == "article").OrderBy(o => o.Seq).Asc.List<ArticleFileT>();
                return files;
            }
        }

        /// <summary>
        /// STL 파일 리스트
        /// </summary>
        /// <param name="no"></param>
        /// <returns></returns>
        public IList<ArticleFileT> GetSTLFileList(int articleNo)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                IList<ArticleFileT> files = session.QueryOver<ArticleFileT>().Where(w => w.ArticleNo == articleNo && w.FileType == "stl" && w.FileGubun == "article").OrderBy(o => o.Seq).Asc.List<ArticleFileT>();
                return files;
            }
        }
        #endregion

        #region get article files by temp
        /// <summary>
        /// get article files by temp
        /// </summary>
        /// <param name="temp"></param>
        /// <returns></returns>
        public IList<ArticleFileT> GetArticleFilesByTemp(string temp)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                IList<ArticleFileT> files = session.QueryOver<ArticleFileT>().Where(w => w.FileGubun != "DELETE" && w.Temp == temp).OrderBy(o => o.Seq).Asc.ThenBy(o => o.RegDt).Asc.List<ArticleFileT>();
                return files;
            }
        }
        #endregion

        #region get article file by no
        /// <summary>
        /// get article file by no
        /// </summary>
        /// <param name="fileNo"></param>
        /// <returns></returns>
        public ArticleFileT GetArticleFileByNo(int fileNo)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                ArticleFileT file = session.QueryOver<ArticleFileT>().Where(w => w.No == fileNo).Take(1).SingleOrDefault<ArticleFileT>();
                return file;
            }
        }
        #endregion

        #region insert article file
        /// <summary>
        /// insert article file
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public int InsertArticleFile(ArticleFileT data)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                int articleFileNo = (Int32)session.Save(data);
                session.Flush();

                return articleFileNo;
            }
        }
        #endregion

        #region update article file
        /// <summary>
        /// update article file
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool UpdateArticleFile(ArticleFileT data)
        {
            if (data == null) new ArgumentException("객체가 Null임");

            using (ISession session = NHibernateHelper.OpenSession())
            {
                try
                {
                    session.Update(data);
                    session.Flush();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
        #endregion


        /// <summary>
        /// 
        /// </summary>
        /// <param name="seqArray"></param>
        public void UpdateArticleFileSeq(string[] seqArray)
        {
            string query = "";
            for (int i = 0; i < seqArray.Length; i++)
            {
                query += " UPDATE ARTICLE_FILE SET SEQ=" + i + " WHERE NO = ?";
            }
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    IQuery queryObj = session.CreateSQLQuery(query);
                    for (int i = 0; i < seqArray.Length; i++)
                    {
                        queryObj.SetParameter(i, seqArray[i]);
                    }

                    queryObj.ExecuteUpdate();
                    transaction.Commit();
                    session.Flush();
                }
            }
        }
    }
}
