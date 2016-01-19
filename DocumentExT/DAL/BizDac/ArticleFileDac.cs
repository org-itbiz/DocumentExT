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
    }
}
